using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityTravel.Domain.Abstract;
using Microsoft.SqlServer.Types;
using CityTravel.Domain.Entities;

namespace CityTravel.Domain.Services.Segment
{
    /// <summary>
    /// The service to finds appropriate routes
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Unit of work for work with database
        /// </summary>
        private readonly IUnitOfWork unitOfWork;
       
        /// <summary>
        /// Gets a value indicating whether [found route].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [found route]; otherwise, <c>false</c>.
        /// </value>
        public bool FoundRoute { get;  private set; }

        /// <summary>
        /// Gets the appropriate routes.
        /// </summary>
        public List<Route> AppropriateRoutes { get;  private set; }


        /// <summary>
        /// buffer for Stops
        /// </summary>
        private const int StopBuffer = 200;
       
        /// <summary>
        /// buffer for Rotues
        /// </summary>     
        private const int RouteBuffer = 1500;

        /// <summary>
        /// Max buffer for finding
        /// </summary>
        private const int MaxBuffer = 1500;
      
        /// <summary>
        /// Initializes a new instance of the <see cref="Track"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public Track(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            FoundRoute = false;
            
        }

        /// <summary>
        /// Gets the appropriate routes.
        /// </summary>
        /// <param name="transportTypes">The transport types.</param>
        /// <param name="startMarker">The start marker.</param>
        /// <param name="endMarker">The end marker.</param>
        /// <returns>Appropriate routes</returns>
        public List<Route> GetAppropriateRoutes(List<int> transportTypes, SqlGeography startMarker, SqlGeography endMarker)
        {

            var routes = GetDefineRoutes(transportTypes);
            var stops = GetDefineStops(transportTypes);
            int startBuffer = RouteBuffer;
            AppropriateRoutes = new List<Route>();
            var findRoutes = FindRoutes(routes, startBuffer, startMarker, endMarker);
            int i = 1;
            while ((FoundRoute == false) && (i<=findRoutes.Count()))
            {
                FoundRoute = false;
                foreach (Route findRoute in findRoutes)
                {
                    var nearestStop = FindNearestStop(stops, findRoute, startMarker, endMarker);
                    var furtherStop = FindFurtherStop(stops, findRoute, startMarker, endMarker);
                    if (nearestStop.StopGeography.STEquals(furtherStop.StopGeography))
                    {
                        break;
                    }
                    if (nearestStop.StopGeography.STDistance(findRoute.RouteGeography.STStartPoint()) <=
                        furtherStop.StopGeography.STDistance(findRoute.RouteGeography.STStartPoint()))
                    {
                        FoundRoute = true;
                        Route appropriateRoute = findRoute;

                        int stIndex = FindStartIndex(appropriateRoute, nearestStop);
                        int endIndex = FindEndIndex(appropriateRoute, furtherStop);
                        appropriateRoute = BuildRouteGeography(stIndex, endIndex, nearestStop, furtherStop,
                                                               appropriateRoute);
                        AppropriateRoutes.Add(new Route
                        {
                            Id = appropriateRoute.Id,
                            Name = appropriateRoute.Name,
                            MapPoints = GetRouteInPoints(appropriateRoute),
                            RouteGeography = appropriateRoute.RouteGeography,
                            RouteType = appropriateRoute.RouteType
                        });
                    }

                }
                i++;

            }
            GetCrossStops(transportTypes);
            SortCrossStopsByStart();

            return AppropriateRoutes;
        }

        /// <summary>
        /// Gets the define routes.
        /// </summary>
        /// <param name="transportTypes">The transport types.</param>
        /// <returns>List of define routes</returns>
        private List<Route> GetDefineRoutes(List<int> transportTypes)
        {
            List<Route> allRoutes = unitOfWork.RouteRepository.All().ToList();
            var result = new List<Route>();
            if (!transportTypes.Contains(0))
            {
                Parallel.For(0, transportTypes.Count(), i => result.AddRange(from item in allRoutes.AsParallel().Where(route => route.RouteType == transportTypes[i])select item)); ;
               /* foreach (var type in transportTypes)
                {
                    result.AddRange(allRoutes.Where(route => route.RouteType == type));
                }*/
            }
            else
            {
                result = allRoutes;
            }
            return result;
        }

        /// <summary>
        /// Gets the define stops.
        /// </summary>
        /// <param name="transportTypes">The transport types.</param>
        /// <returns>List of define stops</returns>
        private List<Stop> GetDefineStops(List<int> transportTypes)
        {
            List<Stop> allStops = unitOfWork.StopRepository.All().ToList();
            var result = new List<Stop>();
            if (!transportTypes.Contains(0))
            {
                Parallel.For(0, transportTypes.Count(), i => result.AddRange(from item in allStops.AsParallel().Where(stop => stop.StopType == transportTypes[i]) select item)); ;
               /* foreach (var type in transportTypes)
                {
                    result.AddRange(allStops.Where(stop => stop.StopType == type));
                }*/
            }
            else
            {
                result = allStops;
            }
            return result;
        }



        /// <summary>
        /// Finds the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <param name="startBuffer">The start buffer.</param>
        /// <param name="startMarker">The start marker.</param>
        /// <param name="endMarker">The end marker.</param>
        /// <returns>Routes with buffer range</returns>
        private static List<Route> FindRoutes(List<Route> routes,int startBuffer,SqlGeography startMarker,SqlGeography endMarker)
        {
            var result = new List<Route>();
            SqlGeography radiusStart = endMarker.STBuffer(startBuffer);
            SqlGeography radiusEnd = startMarker.STBuffer(startBuffer);
          /* Parallel.For(0, routes.Count(), i =>
                                                {
                                                    if (radiusStart.STIntersects(routes[i].RouteGeography)
                      && radiusEnd.STIntersects(routes[i].RouteGeography))
                                                    {
                                                        result.Add(routes[i]);
                                                    }  
                                                });*/
            foreach (var route in routes)
            {
                if (radiusStart.STIntersects(route.RouteGeography)
                    && radiusEnd.STIntersects(route.RouteGeography))
                {
                    result.Add(route);
                }

            }
            return result;
        }



        /// <summary>
        /// Finds the nearest stop.
        /// </summary>
        /// <param name="stops">The stops.</param>
        /// <param name="findRoute">The find route.</param>
        /// <param name="startMarker">The start marker.</param>
        /// <param name="endMarker">The end marker.</param>
        /// <returns>Neareres stop for startMarker</returns>
        private static Stop FindNearestStop(List<Stop> stops, Route findRoute, SqlGeography startMarker, SqlGeography endMarker)
        {
            var buffer = StopBuffer;
            var result = new Stop();
            var foundNearestStop = false;
           
            do
            {
                Parallel.For(0, stops.Count(), i =>
                                                   {
                                                       if (
                       stops[i].StopGeography.STIntersects(
                           endMarker.STBuffer(buffer)) &&
                       stops[i].StopGeography.STIntersects(findRoute.RouteGeography))
                                                       {
                                                           result = new Stop
                                                           {
                                                               Name = stops[i].Name,
                                                               StopGeography = stops[i].StopGeography.STIntersection(findRoute.RouteGeography).EnvelopeCenter()
                                                           };
                                                           foundNearestStop = true;
                                                       }
                                                   });
              /*  foreach (Stop stop in stops)
                {
                    if (
                        stop.StopGeography.STIntersects(
                            endMarker.STBuffer(buffer)) &&
                        stop.StopGeography.STIntersects(findRoute.RouteGeography))
                    {
                        result = new Stop
                        {
                            Name = stop.Name,
                            StopGeography = stop.StopGeography.STIntersection(findRoute.RouteGeography).EnvelopeCenter()
                        };
                        foundNearestStop = true;
                    }
                 }*/
                buffer += StopBuffer;
            } while (foundNearestStop == false && buffer < MaxBuffer);
            return result;
        }



        /// <summary>
        /// Finds the further stop.
        /// </summary>
        /// <param name="stops">The stops.</param>
        /// <param name="findRoute">The find route.</param>
        /// <param name="startMarker">The start marker.</param>
        /// <param name="endMarker">The end marker.</param>
        /// <returns>Further stop for endMarker</returns>
        private static Stop FindFurtherStop(List<Stop> stops, Route findRoute, SqlGeography startMarker, SqlGeography endMarker)
        {
            var buffer = StopBuffer;
            var result = new Stop();
            var foundFurtherStop = false;
            do
            {
               Parallel.For(0, stops.Count(), i =>
                                                   {
                                                       if (
                         stops[i].StopGeography.STIntersects(
                             startMarker.STBuffer(buffer)) &&
                         stops[i].StopGeography.STIntersects(findRoute.RouteGeography))
                                                       {
                                                           result = new Stop
                                                           {
                                                               Name = stops[i].Name,
                                                               StopGeography = stops[i].StopGeography.STIntersection(findRoute.RouteGeography).EnvelopeCenter()

                                                           };
                                                           foundFurtherStop = true;
                                                       }
                     
                                                   });
             /*   foreach (Stop stop in stops)
                {
                    if (
                        stop.StopGeography.STIntersects(
                            startMarker.STBuffer(buffer)) &&
                        stop.StopGeography.STIntersects(findRoute.RouteGeography))
                    {
                        result = new Stop
                        {
                            Name = stop.Name,
                            StopGeography = stop.StopGeography.STIntersection(findRoute.RouteGeography).EnvelopeCenter()

                        };
                        foundFurtherStop = true;
                    }
                   
                }*/
                buffer += StopBuffer;
            } while (foundFurtherStop == false && buffer < MaxBuffer);
            return result;
        }

        /// <summary>
        /// Finds the start index.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="stop">The stop.</param>
        /// <returns>Start index</returns>
        private static int FindStartIndex(Route route, Stop stop)
        {
            var result = 0;
          /* Parallel.For(1, (int) route.RouteGeography.STNumPoints() + 1, i =>
                                                                              {
                                                                                  if (route.RouteGeography.STStartPoint().STDistance(stop.StopGeography)
                      > route.RouteGeography.STStartPoint().STDistance(route.RouteGeography.STPointN(i)))
                                                                                  {
                                                                                      result = i;
                                                                                  }   
                                                                              });*/
            for (int j = 1; j < route.RouteGeography.STNumPoints() ; j++)
            {
                if (route.RouteGeography.STStartPoint().STDistance(stop.StopGeography)
                    > route.RouteGeography.STStartPoint().STDistance(route.RouteGeography.STPointN(j)))
                {
                    result = j ;
                }
            }
            return result;
        }

        /// <summary>
        /// Finds the end index.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="stop">The stop.</param>
        /// <returns>end index</returns>
        private static int FindEndIndex (Route route,Stop stop)
        {
            var result = 0;
           /* Parallel.For(1,(int)route.RouteGeography.STNumPoints()+1,i=>
                                                                         {
                                                                            if (route.RouteGeography.STStartPoint().STDistance(stop.StopGeography)
                    > route.RouteGeography.STStartPoint().STDistance(route.RouteGeography.STPointN(i)))
                {
                    result = i;
                }  
                                                                         });*/
            for (int j = 1; j < route.RouteGeography.STNumPoints() ; j++)
            {

                if (route.RouteGeography.STStartPoint().STDistance(stop.StopGeography)
                    > route.RouteGeography.STStartPoint().STDistance(route.RouteGeography.STPointN(j)))
                {
                    result = j;
                }
            }
            return result;
        }

        /// <summary>
        /// Builds the route geography.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndex">The end index.</param>
        /// <param name="nearestStop">The nearest stop.</param>
        /// <param name="furtherStop">The further stop.</param>
        /// <param name="route">The route.</param>
        /// <returns>Geography of appropriate route</returns>
        private static Route BuildRouteGeography(int startIndex,int endIndex,Stop nearestStop, Stop furtherStop,Route route)
        {
            
                var geographyBuilder = new SqlGeographyBuilder();
                geographyBuilder.SetSrid(4326);
                geographyBuilder.BeginGeography(OpenGisGeographyType.LineString);
                SqlGeography beginGb = nearestStop.StopGeography;
                geographyBuilder.BeginFigure((double) beginGb.Lat, (double) beginGb.Long);
                for (var j = startIndex; j <= endIndex; j++)
                {

                    var point = route.RouteGeography.STPointN(j);
                    geographyBuilder.AddLine((double) point.Lat, (double) point.Long);

                }
                SqlGeography endFigure = furtherStop.StopGeography;
                geographyBuilder.AddLine((double) endFigure.Lat, (double) endFigure.Long);
                geographyBuilder.EndFigure();
                geographyBuilder.EndGeography();
                route.RouteGeography = geographyBuilder.ConstructedGeography;
            
            return route;
        }

        /// <summary>
        /// Gets the cross stops.
        /// </summary>
        /// <param name="transportTypes">The transport types.</param>
        private void GetCrossStops(List<int> transportTypes)
        {
            var defineStops = GetDefineStops(transportTypes);
            if (AppropriateRoutes.Count < 1) return;
            foreach (var route in AppropriateRoutes)
            {
                route.Stops=new List<Stop>();  
                foreach (Stop stop in defineStops)
                {
                    if (route.RouteGeography.STIntersects(stop.StopGeography))
                    {
                        route.Stops.Add(new Stop
                                            {
                                                Id = stop.Id,
                                                Name = stop.Name,
                                                 
                                                StopGeography =
                                                    (route.RouteGeography.STIntersection(
                                                        stop.StopGeography)).EnvelopeCenter(),
                                                StopType = stop.StopType,
                                                Points = new MapPoint((double)(route.RouteGeography.STIntersection(
                                                    stop.StopGeography).EnvelopeCenter()).Long, (double)(route.RouteGeography.STIntersection(
                                                        stop.StopGeography).EnvelopeCenter()).Lat)
                                            });
                    }
                }
            }
        }

        /// <summary>
        /// Gets the route in points.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        private static List<MapPoint> GetRouteInPoints(Route route)
        {
            var result = new List<MapPoint>();

            for (int i = 1; i < route.RouteGeography.STNumPoints()+1 ; i++)
            {
                var point = route.RouteGeography.STPointN(i);
                result.Add(new MapPoint((double)point.Long, (double)point.Lat));
            }

            return result;
        }

        /// <summary>
        /// Sorts the cross stops by start.
        /// </summary>
        private void SortCrossStopsByStart()
        {
            foreach (var appropriateRoute in AppropriateRoutes.Where(appropriateRoute => appropriateRoute.Stops != null))
            {
                for (int i = appropriateRoute.Stops.Count - 1; i > 0; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (appropriateRoute.RouteGeography.STStartPoint().STDistance(
                            appropriateRoute.Stops[j].StopGeography) >=
                            appropriateRoute.RouteGeography.STStartPoint().STDistance(
                                appropriateRoute.Stops[j + 1].StopGeography))
                        {

                            Stop temp = appropriateRoute.Stops[j];
                            appropriateRoute.Stops[j] = appropriateRoute.Stops[j + 1];
                            appropriateRoute.Stops[j + 1] = temp;
                        }
                    }
                }
            }
        }
    }
}
