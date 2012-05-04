namespace CityTravel.Domain.Services.Segment.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CityTravel.Domain.Entities.Route;
    using CityTravel.Domain.Helpres;
    using CityTravel.Domain.Services.Segment.Abstract;
    using CityTravel.Domain.Settings;

    using Microsoft.SqlServer.Types;

    /// <summary>
    /// Routes finder 
    /// </summary>
    public class RouteSeach : IRouteSeach
    {
        /// <summary>
        /// Kilometer in metres
        /// </summary>
        private const double Hour = 60;

        /// <summary>
        /// Kilometer in metres
        /// </summary>
        private const double Kilometer = 1000;

        /// <summary>
        /// Gets or sets AppropriateRoutes.
        /// </summary>
        public List<Route> AppropriateRoutes { get; set; }

        /// <summary>
        /// Gets the appropriate routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <param name="invalidDirections">The invalid directions.</param>
        /// <param name="validWords">The valid words.</param>
        /// <param name="invalidWords">The invalid words.</param>
        /// <param name="startMarker">The start marker.</param>
        /// <param name="endMarker">The end marker.</param>
        /// <param name="transportTypes">The transport types.</param>
        /// <returns>
        /// Appropriates routes
        /// </returns>
        public List<Route> GetAppropriateRoutes(
            IEnumerable<Route> routes, 
            IEnumerable<string> invalidDirections,
            IEnumerable<string> validWords,
            IEnumerable<string> invalidWords,
            SqlGeography startMarker,
            SqlGeography endMarker,
            IEnumerable<Transport> transportTypes)
        {
            if (startMarker.STEquals(endMarker))
            {
                throw new ArgumentException("Start & end markers are equals");
            }

            IEnumerable<Route> defineRoutes = this.GetRoutes(transportTypes, routes, startMarker, endMarker);
            this.AppropriateRoutes = new List<Route>();
            var possibleRoutes = new List<Route>();
            foreach (Route route in defineRoutes)
            {
                var possibleStartStop = this.GetPossibleStop(route, endMarker);
                var possiblEndStop = this.GetPossibleStop(route, startMarker);
                if (possibleStartStop.StopGeography.STEquals(possiblEndStop.StopGeography))
                {
                    continue;
                }

                if (possibleStartStop.StopGeography.STDistance(route.RouteGeography.STStartPoint())
                    < possiblEndStop.StopGeography.STDistance(route.RouteGeography.STStartPoint()))
                {
                    route.StartStop = this.GetStop(route, endMarker);
                    route.EndStop = this.GetStop(route, startMarker);
                    route.StartRouteIndex = this.GetPathIndex(route, route.StartStop.StopGeography) + GeneralSettings.MaxIndexDeflection;
                    route.EndRouteIndex = this.GetPathIndex(route, route.EndStop.StopGeography) - GeneralSettings.MaxIndexDeflection;
                    possibleRoutes.Add(route);
                }
            }

            this.AppropriateRoutes = this.DeleteIncorectRoutes(
                invalidDirections, validWords, invalidWords, startMarker, endMarker, possibleRoutes);

            for (int i = 0; i < this.AppropriateRoutes.Count; i++)
            {
                if (!this.AppropriateRoutes[i].Type.Type.Equals("Walking"))
                {
                    this.AppropriateRoutes[i] = this.BuildRouteGeography(this.AppropriateRoutes[i]);
                    this.AppropriateRoutes[i] = this.BuildRouteProperties(this.AppropriateRoutes[i], startMarker, endMarker, invalidDirections, validWords, invalidWords);
                }
            }

            this.AppropriateRoutes.RemoveAll(
                route =>
                !route.Type.Type.Equals("Walking") && route.Length <= (route.WalkingRoutes.First().Length + route.WalkingRoutes.Last().Length));

            return this.AppropriateRoutes;
        }

        /// <summary>
        /// Deletes the incorect routes.
        /// </summary>
        /// <param name="invalidDirections">The invalid directions.</param>
        /// <param name="validWords">The valid words.</param>
        /// <param name="invalidWords">The invalid words.</param>
        /// <param name="startMarker">The start marker.</param>
        /// <param name="endMarker">The end marker.</param>
        /// <param name="possibleRoutes">The possible routes.</param>
        /// <returns>
        /// Delete incorect routes in possible list of routes.
        /// </returns>
        public List<Route> DeleteIncorectRoutes(
            IEnumerable<string> invalidDirections,
            IEnumerable<string> validWords,
            IEnumerable<string> invalidWords,
            SqlGeography startMarker, 
            SqlGeography endMarker,
            List<Route> possibleRoutes)
        {
            List<double> startWalkLength =
                possibleRoutes.Select(route => (double)startMarker.STDistance(route.StartStop.StopGeography)).ToList();
            List<double> endWalkLength =
                possibleRoutes.Select(route => (double)endMarker.STDistance(route.EndStop.StopGeography)).ToList();
            var times = new List<double>();
            for (int i = 0; i < possibleRoutes.Count; i++)
            {
                possibleRoutes[i].Length = this.GetLengthOfPartRoute(possibleRoutes[i]);
                double routeTime = this.GetTimeInMinutes(
                    possibleRoutes[i].Speed, possibleRoutes[i].Length, (int)possibleRoutes[i].WaitingTime.TotalMinutes);
                double summaryWalkLength = startWalkLength[i] + endWalkLength[i];
                double walkTime = this.GetTimeInMinutes(GeneralSettings.WalkingSpeed, summaryWalkLength);
                double summaryTime = routeTime + walkTime;
                times.Add(summaryTime);
            }

            for (int i = 0; i < possibleRoutes.Count; i++)
            {
                possibleRoutes[i].PossibleTime = times[i];
            }

            possibleRoutes.Add(
                this.GetWalkingRoute(startMarker, endMarker, invalidDirections, validWords, invalidWords));
            possibleRoutes.Sort((a, b) => a.PossibleTime.CompareTo(b.PossibleTime));
            double minTime = possibleRoutes.First().Time.TotalMinutes;
            possibleRoutes.RemoveAll(
                route =>
                route.Time.TotalMinutes > minTime * GeneralSettings.MaxTimeConstraint);

            return possibleRoutes;
        }

        /// <summary>
        /// Gets the length of part route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>
        /// Length of part route.
        /// </returns>
        private double GetLengthOfPartRoute(Route route)
        {
            double length = 0;
            for (var i = route.StartRouteIndex; i < route.EndRouteIndex; i++)
            {
                length += (double)route.RouteGeography.STPointN(i).STDistance(route.RouteGeography.STPointN(i + 1));
            }

            return length;
        }

        /// <summary>
        /// Adds the walking steps to route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="startMarker">The start marker.</param>
        /// <param name="endMarker">The end marker.</param>
        /// <returns>
        /// Route with walking steps
        /// </returns>
        private Route BuildRouteProperties(
            Route route, 
            SqlGeography startMarker,
            SqlGeography endMarker,
            IEnumerable<string> invalidDirections,
            IEnumerable<string> validWords,
            IEnumerable<string> invalidWords 
            )
        {
            if (!route.Type.Type.Equals("Walking"))
            {
                route = this.GetCrossStops(route);
                route.Length = this.GetRouteLength(route);
                route.MapPoints = this.GetRouteInPoints(route);
                route.WalkingRoutes = new List<WalkingRoute>
                    {
                        this.GetWalkingStepsForRoute(
                            endMarker, route.StartStop.StopGeography, invalidDirections, validWords, invalidWords), 
                        this.GetWalkingStepsForRoute(
                            route.EndStop.StopGeography, startMarker, invalidDirections, validWords, invalidWords)
                    };
                route.Time = this.GetSumamryTime(route);
                route.RouteTime = TimeSpan.FromMinutes(this.GetTimeInMinutes(route.Speed, route.Length));
                route.Stops.Select(this.GetPointsFromStops).ToList();
            }

            return route;
        }

        /// <summary>
        /// Get walking route
        /// </summary>
        /// <param name="startPoint">The start point.</param>
        /// <param name="endPoint">The end point.</param>
        /// <returns>Walking route between points</returns>
        private WalkingRoute GetWalkingStepsForRoute(
            SqlGeography startPoint, 
            SqlGeography endPoint,
            IEnumerable<string> invalidDirections,
            IEnumerable<string> validWords,
            IEnumerable<string> invalidWords 
            )
        {
            var urlForDirection = GoogleMapHelper.CreateUrlForDirectionRequest(
                GeneralSettings.GoogleApiKey, startPoint, endPoint);
            var responce = GoogleMapHelper.GetResponceFromGoogleApi(urlForDirection);
            var token = responce.SelectToken(GoogleMapHelper.PolylinePointsToken);
            var points = GoogleMapHelper.DecodePolyline((string)token);
            var walkingRoute = new WalkingRoute
            {
                Name = string.Empty, 
                MapPoints = points, 
                Speed = GeneralSettings.WalkingSpeed, 
                Steps = GoogleMapHelper.GetStepsOfDirection(responce, invalidDirections, validWords, invalidWords), 
                Type = Transport.Walking, 
                Length = GoogleMapHelper.GetDistanceOfDirection(responce), 
                Time =
                    TimeSpan.FromMinutes(
                        this.GetTimeInMinutes(
                            GeneralSettings.WalkingSpeed, GoogleMapHelper.GetDistanceOfDirection(responce)))
            };

            return walkingRoute;
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <param name="transportTypes">The transport types.</param>
        /// <param name="routes">The routes.</param>
        /// <param name="startMarker">The start marker.</param>
        /// <param name="endMarker">The end marker.</param>
        /// <returns>Definde routes by transport types</returns>
        private IEnumerable<Route> GetRoutes(
            IEnumerable<Transport> transportTypes,
            IEnumerable<Route> routes,
            SqlGeography startMarker,
            SqlGeography endMarker)
        {
            var result = new List<Route>();
            if (transportTypes != null)
            {
                result.AddRange(
                    !transportTypes.Contains(Transport.All)
                        ? routes.Where(
                            route =>
                            this.Intersection(route, startMarker, endMarker)
                            && transportTypes.Any(type => route.RouteType == (int)type))
                        : routes.Where(route => this.Intersection(route, startMarker, endMarker)));
            }

            return result;
        }

        /// <summary>
        /// Intersections the specified route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="startMarker">The start marker.</param>
        /// <param name="endMarker">The end marker.</param>
        /// <returns>return bool value of intersections for routes</returns>
        private bool Intersection(Route route, SqlGeography startMarker, SqlGeography endMarker)
        {
            return (bool)route.RouteGeography.STIntersects(startMarker.STBuffer(GeneralSettings.RouteRadiusSeach))
                   && (bool)route.RouteGeography.STIntersects(endMarker.STBuffer(GeneralSettings.RouteRadiusSeach));
        }

        /// <summary>
        /// Finds the start stop.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="marker">The marker.</param>
        /// <returns>Geography of stop</returns>
        private Stop GetStop(Route route, SqlGeography marker)
        {
            Stop result = route.Stops[0];

            foreach (Stop stop in route.Stops)
            {
                if (stop.StopGeography.STDistance(marker) < result.StopGeography.STDistance(marker))
                {
                    result = stop;
                }
            }

            List<Stop> possibleStops =
                route.Stops.Where(stop => (bool)result.StopGeography.STBuffer(GeneralSettings.MaxStopRadiusSeach).STIntersects(stop.StopGeography)).ToList();
            var urlForDistanceMatrix = GoogleMapHelper.CreateUrlForDisntanceMatrixRequest(
                GeneralSettings.GoogleApiKey, marker, possibleStops);
            var responce = GoogleMapHelper.GetResponceFromGoogleApi(urlForDistanceMatrix);
            var distances = GoogleMapHelper.GetMatrixOfDistanceForOneStop(responce);
            if (distances.Count != 0)
            {
                var index = distances.IndexOf(distances.Min(stop => stop));
                return possibleStops[index];
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Gets the possible stop.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="marker">The marker.</param>
        /// <returns>Possible stop of route.</returns>
        private Stop GetPossibleStop(Route route, SqlGeography marker)
        {
            Stop result = route.Stops[0];

            foreach (Stop stop in route.Stops)
            {
                if (stop.StopGeography.STDistance(marker) < result.StopGeography.STDistance(marker))
                {
                    result = stop;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the index of the path.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="point">The point.</param>
        /// <returns>Index for rechenge route</returns>
        private int GetPathIndex(Route route, SqlGeography point)
        {
            int position = 1;

            for (int i = 1; i < route.RouteGeography.STNumPoints(); i++)
            {
                position++;
                if (route.RouteGeography.STPointN(i).STDistance(point) <= GeneralSettings.MaxFindIndexDeflection)
                {
                    break;
                }
            }

            return position;
        }

        /// <summary>
        /// Builds the route geography.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>
        /// New route geography
        /// </returns>
        private Route BuildRouteGeography(Route route)
        {
            var routeBuilder = new SqlGeographyBuilder();
            routeBuilder.SetSrid(4326);
            routeBuilder.BeginGeography(OpenGisGeographyType.LineString);
            SqlGeography beginPoint = route.StartStop.StopGeography;
            routeBuilder.BeginFigure((double)beginPoint.Lat, (double)beginPoint.Long);

            for (var j = route.StartRouteIndex; j <= route.EndRouteIndex; j++)
            {
                var point = route.RouteGeography.STPointN(j);
                routeBuilder.AddLine((double)point.Lat, (double)point.Long);
            }

            SqlGeography endPoint = route.EndStop.StopGeography;
            routeBuilder.AddLine((double)endPoint.Lat, (double)endPoint.Long);
            
            routeBuilder.EndFigure();
            routeBuilder.EndGeography();
            route.CurrentPath = routeBuilder.ConstructedGeography;

            return route;
        }

        /// <summary>
        /// Gets the route in points.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>Points for paint route on map</returns>
        private List<MapPoint> GetRouteInPoints(Route route)
        {
            var result = new List<MapPoint>();

            for (int i = 1; i <= route.CurrentPath.STNumPoints(); i++)
            {
                var point = route.CurrentPath.STPointN(i);
                result.Add(new MapPoint((double)point.Long, (double)point.Lat));
            }

            return result;
        }

        /// <summary>
        /// Gets the points from stops.
        /// </summary>
        /// <param name="stop">The route.</param>
        /// <returns>Points of stops to paint this stops on map</returns>
        private MapPoint GetPointsFromStops(Stop stop)
        {
            return stop.Points = new MapPoint((double)stop.StopGeography.Long, (double)stop.StopGeography.Lat);
        }

        /// <summary>
        /// Gets the cross stops.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>
        /// Route with new cross stops
        /// </returns>
        private Route GetCrossStops(Route route)
        {
            var crossStops = new List<Stop>();
            crossStops.AddRange(
                route.Stops.Where(stop => (bool)stop.StopGeography.STBuffer(1).STIntersects(route.CurrentPath)));
            route.Stops = crossStops;
            route.Stops = this.SortStops(route);

            return route;
        }

        /// <summary>
        /// Gets the walking route.
        /// </summary>
        /// <param name="startPoint">The start point.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="invalidDirections">The invalid directions.</param>
        /// <param name="validWords">The valid words.</param>
        /// <param name="invalidWords">The invalid words.</param>
        /// <returns>
        /// Walking route
        /// </returns>
        private Route GetWalkingRoute(
            SqlGeography startPoint, 
            SqlGeography endPoint,
            IEnumerable<string> invalidDirections,
            IEnumerable<string> validWords,
            IEnumerable<string> invalidWords 
            )
        {
            var urlForDirection = GoogleMapHelper.CreateUrlForDirectionRequest(
                GeneralSettings.GoogleApiKey, startPoint, endPoint);
            var responce = GoogleMapHelper.GetResponceFromGoogleApi(urlForDirection);
            var polyline = (List<MapPoint>)GoogleMapHelper.GetSummaryPolyline(responce);
            var summaryDistance = GoogleMapHelper.GetDistanceOfDirection(responce);
            var summaryTime = this.GetTimeInMinutes(GeneralSettings.WalkingSpeed, summaryDistance);
            var walkingType = new TransportType { Type = "Walking" };
            var walkingRoute = new Route
            {
                Name = string.Empty,
                MapPoints = polyline,
                RouteType = (int)Transport.Walking,
                Speed = GeneralSettings.WalkingSpeed,
                Time = TimeSpan.FromMinutes(summaryTime),
                Stops = new List<Stop>(),
                Type = walkingType,
                Steps = GoogleMapHelper.GetStepsOfDirection(responce,invalidDirections,validWords,invalidWords),
                Length = GoogleMapHelper.GetDistanceOfDirection(responce)
            };

            return walkingRoute;
        }

        /// <summary>
        /// Gets the time.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>
        /// Summary time of route
        /// </returns>
        private TimeSpan GetSumamryTime(Route route)
        {
            double walkLength = route.WalkingRoutes.Sum(walkingRoute => walkingRoute.Length);
            var walkTime = this.GetTimeInMinutes(GeneralSettings.WalkingSpeed, walkLength);

            return
                TimeSpan.FromMinutes(
                    this.GetTimeInMinutes(route.Speed, route.Length, (int)route.WaitingTime.TotalMinutes) + walkTime);
        }

        /// <summary>
        /// Gets the time in minutes.
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <param name="length">The length.</param>
        /// <param name="waitingTime">The waiting time.</param>
        /// <returns>
        /// Time in minutes
        /// </returns>
        private double GetTimeInMinutes(int speed, double length, int waitingTime = 0)
        {
            var result = speed != 0 ? (length / ((speed * Kilometer) / Hour)) + waitingTime : 0;
            
            return result;
        }

        /// <summary>
        /// Gets the length of the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>Length of route</returns>
        private double GetRouteLength(Route route)
        {
            double length = 0;
            for (var i = 1; i < route.CurrentPath.STNumPoints(); i++)
            {
                length += (double)route.CurrentPath.STPointN(i).STDistance(route.CurrentPath.STPointN(i + 1));
            }

            return length;
        }

        /// <summary>
        /// Sorts the stops.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>Sorting list of stops</returns>
        private List<Stop> SortStops(Route route)
        {
            for (var i = route.Stops.Count - 1; i > 0; i--)
            {
                for (var j = 0; j < i; j++)
                {
                    if (route.RouteGeography.STStartPoint().STDistance(route.Stops[j].StopGeography)
                        >= route.RouteGeography.STStartPoint().STDistance(route.Stops[j + 1].StopGeography))
                    {
                        var temp = route.Stops[j];
                        route.Stops[j] = route.Stops[j + 1];
                        route.Stops[j + 1] = temp;
                    }
                }
            }

            return route.Stops as List<Stop>;
        }
    }
}