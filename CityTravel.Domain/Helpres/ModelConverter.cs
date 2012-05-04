namespace CityTravel.Domain.Helpres
{
    using System.Collections.Generic;
    using System.Linq;

    using CityTravel.Domain.Entities.Route;
    using CityTravel.Domain.Entities.SimpleModel;

    /// <summary>
    /// Convert entity framework model to the simple model without cicly relations.
    /// </summary>
    public static class ModelConverter
    {
        /// <summary>
        /// Makes the routes is valie.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <returns>Valid for JSON routes</returns>
        public static List<SimpleRoute> Convert(IList<Route> routes)
        {
            
            var routeModel = new List<SimpleRoute>();
            foreach (var route in routes)
            {
                
                if (!route.Type.Type.Equals(Transport.Walking.ToString()))
                {
                    var summaryWalkingDistance = GetSummaryWalkingDistance(route);
                    var allRouteLength = route.Length + summaryWalkingDistance;
                    routeModel.Add(
                        new SimpleRoute
                        {
                            Name = route.Name,
                            AllLength = DimensionConverter.GetRoundDistance(allRouteLength),
                            BusLength = DimensionConverter.GetRoundDistance(route.Length),
                            MapPoints = route.MapPoints,
                            RouteTime = DimensionConverter.GetRoundTime(route.RouteTime),
                            Speed = route.Speed,
                            Steps = route.Steps,
                            Stops = GetStopsInViewModel(route),
                            Price = DimensionConverter.GetTransportPrice(route.Price),
                            Cost = route.Price,
                            Time = DimensionConverter.GetRoundTime(route.Time),
                            TotalMinutes = (int)route.Time.TotalMinutes,
                            Type = route.Type,
                            WaitingTime = DimensionConverter.GetRoundTime(route.WaitingTime),
                            WalkingRoutes = GetWalkingRoutesInViewModel(route),
                            SummaryWalkingLength = DimensionConverter.GetRoundDistance(summaryWalkingDistance)
                        });
                }
                else
                {
                    var length = DimensionConverter.GetRoundDistance(route.Length);
                    routeModel.Add(new SimpleRoute()
                    {
                        Name = route.Name,
                        BusLength = DimensionConverter.GetRoundDistance(0),
                        AllLength = length,
                        MapPoints = route.MapPoints,
                        Speed = route.Speed,
                        Steps = route.Steps,
                        Time = DimensionConverter.GetRoundTime(route.Time),
                        TotalMinutes = (int)route.Time.TotalMinutes,
                        Type = route.Type,
                        Price = "0",
                        SummaryWalkingLength = length
                    });
                }
            }

            return routeModel;
        }

        /// <summary>
        /// Gets the stops in view model.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>Valid stops </returns>
        private static List<SimpleStop> GetStopsInViewModel(Route route)
        {
            var stops = route.Stops.Select(stop => new SimpleStop()
            {
                Name = stop.Name,
                Points = stop.Points
            }).ToList();

            return stops;
        }

        /// <summary>
        /// Gets the walking routes in view model.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>Valid walking routes</returns>
        private static List<SimpleWalkingRoute> GetWalkingRoutesInViewModel(Route route)
        {
            var walkingRoutes = route.WalkingRoutes.Select(walk => new SimpleWalkingRoute()
            {
                Name = walk.Name,
                Length = DimensionConverter.GetRoundDistance(walk.Length),
                MapPoints = walk.MapPoints,
                Speed = walk.Speed,
                Steps = walk.Steps,
                Time = DimensionConverter.GetRoundTime(walk.Time),
                Type = walk.Type,
            }).ToList();

            return walkingRoutes;
        }

        /// <summary>
        /// Gets the summary walking distance.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>Summary walking distance in route</returns>
        private static double GetSummaryWalkingDistance(Route route)
        {
            var summaryDistance = route.WalkingRoutes.Sum(walk => walk.Length);

            return summaryDistance;
        }
    }
}
