using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTravel.Domain.Abstract;
using CityTravel.Domain.Entities;
using Microsoft.SqlServer.Types;

namespace CityTravel.Domain.Services.Segment
{
    public class TrackFinder
    {

        /// <summary>
        /// Gets the appropriate routes.
        /// </summary>
        public List<Route> AppropriateRoutes { get; private set; }

        public List<Route> Routes { get; private set; }
        public List<int> TransportTypes { get; private set; }

        public TrackFinder(List<Route> routes, List<int> transportTypes)
        {
            this.Routes = routes;
            this.TransportTypes = transportTypes;
        }

        /// <summary>
        /// Gets the appropriate routes.
        /// </summary>
        /// <param name="transportTypes">The transport types.</param>
        /// <param name="startMarker">The start marker.</param>
        /// <param name="endMarker">The end marker.</param>
        /// <returns>Appropriate routes</returns>
        public List<Route> GetAppropriateRoutes(SqlGeography startMarker, SqlGeography endMarker)
        {
            Routes = GetRoutes(TransportTypes, Routes);
            AppropriateRoutes = new List<Route>();
            foreach (Route route in Routes)
            {
                var nearestStop = FindNearestStop(route, endMarker);
                var furtherStop = FindNearestStop(route, startMarker);
                if (nearestStop.STEquals(furtherStop))
                {
                    continue;
                }
                if (nearestStop.STDistance(route.RouteGeography.STStartPoint()) <
                    furtherStop.STDistance(route.RouteGeography.STStartPoint()))
                {
                    Route appropriateRoute = route;
                    int startIndex = FindIndex(appropriateRoute, nearestStop)+1;
                    int endIndex = FindIndex(appropriateRoute, furtherStop)-1;
                    appropriateRoute = BuildRouteGeography(startIndex, endIndex, nearestStop, furtherStop,
                                                           appropriateRoute);
                    appropriateRoute.Time = GetTime(appropriateRoute);
                    appropriateRoute = GetCrossStops(appropriateRoute);
                    AppropriateRoutes.Add(appropriateRoute);
                }
            }
            SortCrossStopsByStart();

            return AppropriateRoutes;
        }

        /// <summary>
        /// Gets the define routes.
        /// </summary>
        /// <param name="transportTypes">The transport types.</param>
        /// <returns>List of define routes</returns>
        private static List<Route> GetRoutes(List<int> transportTypes, List<Route> routes)
        {
            List<Route> result = new List<Route>();
            if (!transportTypes.Contains(0))
            {
                result.AddRange(from route in routes
                                from type in transportTypes
                                where route.RouteType == type
                                select route);

                //    Parallel.For(0, transportTypes.Count(), i => result.AddRange(from item in routes.AsParallel().Where(route => route.RouteType == transportTypes[i])select item)); ;
            }
            else
            {
                result = routes;
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
        private static SqlGeography FindNearestStop(Route route, SqlGeography marker)
        {
            SqlGeography result = route.Stops[0].StopGeography;

            for (int i = 0; i < route.Stops.Count - 1; i++)
            {
                if (route.Stops[i].StopGeography.STDistance(marker) < result.STDistance(marker))
                {
                    result = route.Stops[i].StopGeography;
                }

            }
            return result;
        }


        /// <summary>
        /// Finds the start index.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="stop">The stop.</param>
        /// <returns>Start index</returns>
        private static int FindIndex(Route route, SqlGeography stop)
        {
            int result = 0;

            for (int j = 1; j <= route.RouteGeography.STNumPoints(); j++)
            {
                if (route.RouteGeography.STStartPoint().STDistance(stop)
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
        private static Route BuildRouteGeography(int startIndex, int endIndex, SqlGeography nearestStop, SqlGeography furtherStop, Route route)
        {

            var geographyBuilder = new SqlGeographyBuilder();
            geographyBuilder.SetSrid(4326);
            geographyBuilder.BeginGeography(OpenGisGeographyType.LineString);
            SqlGeography beginGb = nearestStop;
            geographyBuilder.BeginFigure((double)beginGb.Lat, (double)beginGb.Long);
            for (var j = startIndex; j <= endIndex; j++)
            {

                var point = route.RouteGeography.STPointN(j);
                geographyBuilder.AddLine((double)point.Lat, (double)point.Long);

            }
            SqlGeography endFigure = furtherStop;
            geographyBuilder.AddLine((double)endFigure.Lat, (double)endFigure.Long);
            geographyBuilder.EndFigure();
            geographyBuilder.EndGeography();
            route.RouteGeography = geographyBuilder.ConstructedGeography;

            return route;
        }


        /// <summary>
        /// Gets the route in points.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        public static List<MapPoint> GetRouteInPoints(Route route)
        {
            var result = new List<MapPoint>();

            for (int i = 1; i <= route.RouteGeography.STNumPoints(); i++)
            {
                var point = route.RouteGeography.STPointN(i);
                result.Add(new MapPoint((double)point.Long, (double)point.Lat));
            }

            return result;
        }

        public static List<MapPoint> GetPointsFromStops(Route route)
        {
            var result = new List<MapPoint>();
            foreach (var stop in route.Stops)
            {
                result.Add(new MapPoint((double)stop.StopGeography.Long, (double)stop.StopGeography.Lat));
            }
            return result;
        }


        public static Route GetCrossStops(Route route)
        {
            var crossStops = new List<Stop>();
            var result = route;
            foreach (var stop in route.Stops)
            {
                if (stop.StopGeography.STBuffer(1).STIntersects(route.RouteGeography))
                {
                    crossStops.Add(stop);
                }
            }
            result.Stops = crossStops;
            return result;
        }


        private static TimeSpan GetTime(Route route)
        {
            double lenght = 0;
            for (int i = 1; i < route.RouteGeography.STNumPoints(); i++)
            {
                lenght += (double) route.RouteGeography.STPointN(i).STDistance(route.RouteGeography.STPointN(i + 1));
            }
            return TimeSpan.FromSeconds((lenght/(route.Speed*3600)/1000) + route.WaitingTime.Seconds);
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

