using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CityTravel.Domain.Helpres;
using Microsoft.SqlServer.Types;

namespace CityTravel.Domain.Entities
{
    /// <summary>
    /// Entity for Route
    /// </summary>
    [Table("Route")]
    public class Route : BaseEntity
    {
        /// <summary>
        /// Sql geography of route
        /// </summary>
        private SqlGeography routeGeography;

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets RouteBin.
        /// </summary>
        public byte[] RouteBin { get; set; }

        /// <summary>
        /// Gets or sets RouteGeography.
        /// </summary>
        [NotMapped]
        public SqlGeography RouteGeography
        {
            get
            {
                return GeographyHelpers.GetGeography(this.routeGeography, this.RouteBin);
            }

            set
            {
                this.routeGeography = value;
                this.RouteBin = GeographyHelpers.SetBinFromGeography(value, "route");
            }
        }

        /// <summary>
        /// Gets or sets the current path.
        /// </summary>
        /// <value>
        /// The current path.
        /// </value>
        [NotMapped]
        public SqlGeography CurrentPath { get; set; }

        /// <summary>
        /// Gets or sets the type of the route.
        /// </summary>
        /// <value>
        /// The type of the route.
        /// </value>
        public int? RouteType { get; set; }

        /// <summary>
        /// Gets or sets the stops.
        /// </summary>
        /// <value>
        /// The stops.
        /// </value>
        public virtual IList<Stop> Stops { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [ForeignKey("RouteType")]
        public virtual TransportType Type { get; set; }

        /// <summary>
        /// Gets or sets the map points.
        /// </summary>
        /// <value>
        /// The map points.
        /// </value>
        [NotMapped]
        public List<MapPoint> MapPoints { get; set; }

        /// <summary>
        /// Gets or sets the waiting time.
        /// </summary>
        /// <value>
        /// The waiting time.
        /// </value>
        [Required]
        public TimeSpan WaitingTime { get; set; }

        /// <summary>
        /// Gets or sets the route Price.
        /// </summary>
        /// <value>
        /// The route Price.
        /// </value>
        [Required]
        public float Price { get; set; }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        [Required]
        public int Speed { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        [NotMapped]
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Gets or sets the route time.
        /// </summary>
        /// <value>
        /// The route time.
        /// </value>
        [NotMapped]
        public TimeSpan RouteTime { get; set; }

        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        /// <value>
        /// The steps.
        /// </value>
        [NotMapped]
        public IList<Step> Steps { get; set; }

        /// <summary>
        /// Gets or sets the routes.
        /// </summary>
        /// <value>
        /// The routes.
        /// </value>
        [NotMapped]
        public IList<WalkingRoute> WalkingRoutes { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        [NotMapped]
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the length of the summary.
        /// </summary>
        /// <value>
        /// The length of the summary.
        /// </value>
        [NotMapped]
        public double PossibleTime { get; set; }

        /// <summary>
        /// Gets or sets the start index of the route.
        /// </summary>
        /// <value>
        /// The start index of the route.
        /// </value>
        [NotMapped]
        public int StartRouteIndex { get; set; }

        /// <summary>
        /// Gets or sets the start stop.
        /// </summary>
        /// <value>
        /// The start stop.
        /// </value>
        [NotMapped]
        public Stop StartStop { get; set; }

        /// <summary>
        /// Gets or sets the end stop.
        /// </summary>
        /// <value>
        /// The end stop.
        /// </value>
        [NotMapped]
        public Stop EndStop { get; set; }

        /// <summary>
        /// Gets or sets the end index of the route.
        /// </summary>
        /// <value>
        /// The end index of the route.
        /// </value>
        [NotMapped]
        public int EndRouteIndex { get; set; }
		
		/// <summary>
		/// Gets the Address Point A.
		/// </summary>
		/// <value>
		/// The the Address Point A.
		/// </value>
		[NotMapped]
		public string AddressA { get; set; }

		/// <summary>
		/// Gets the Address Point B.
		/// </summary>
		/// <value>
		/// The the Address Point B.
		/// </value>
		[NotMapped]
		public string AddressB { get; set; }

        /// <summary>
        /// Makes the routes is valie.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <returns>Valid for JSON routes</returns>
        public static List<RouteViewModel> MakeValid(List<Route> routes)
        {
            var routeModel = new List<RouteViewModel>();
            foreach (var route in routes)
            {
                if (!route.Type.Type.Equals("Walking"))
                {
                    var summaryWalkingDistance = GetSummaryWalkingDistance(route);
                    var allRouteLength = route.Length + summaryWalkingDistance;
                    routeModel.Add(
                        new RouteViewModel
                            {
                                Name = route.Name,
                                AllLength = GetRoundDistance(allRouteLength),
                                BusLength = GetRoundDistance(route.Length),
                                MapPoints = route.MapPoints,
                                RouteTime = GetRoundTime(route.RouteTime),
                                Speed = route.Speed,
                                Steps = route.Steps,
                                Stops = GetStopsInViewModel(route),
								Price = GetPriceTransport(route.Price),
                                Cost = route.Price,
                                Time = GetRoundTime(route.Time),
                                TotalMinutes = (int)route.Time.TotalMinutes,
                                Type = route.Type,
                                WaitingTime = route.WaitingTime.TotalMinutes.ToString(),
                                WalkingRoutes = GetWalkingRoutesInViewModel(route),
                                SummaryWalkingLength = GetRoundDistance(summaryWalkingDistance)
                            });
                }
                else
                {
                    var length = GetRoundDistance(route.Length);
                    routeModel.Add(new RouteViewModel()
                        {
                            Name = route.Name,
                            BusLength = GetRoundDistance(0),
                            AllLength = length,
                            MapPoints = route.MapPoints,
                            Speed = route.Speed,
                            Steps = route.Steps,
                            Time = GetRoundTime(route.Time),
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
        /// Gets the round time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>Time in string </returns>
        public static string GetRoundTime(TimeSpan time)
        {
            var result = time.TotalMinutes >= 60
                             ? string.Format(Math.Round(time.TotalMinutes / 60, 1).ToString("F1") + "{0}", " ч")
                             : string.Format(time.TotalMinutes.ToString("F0") + "{0}", " мин");
            
            return result;
        }

        /// <summary>
        /// Gets the round distance.
        /// </summary>
        /// <param name="distance">The distance.</param>
        /// <returns>
        /// Disntace in string
        /// </returns>
        public static string GetRoundDistance(double distance)
        {
            var result = distance >= 1000
                             ? string.Format(Math.Round(distance / 1000, 1) + "{0}", " км") 
                             : string.Format(distance.ToString("F0") + "{0}", " м");

            return result;
        }

        /// <summary>
        /// Gets the price for Transport Type
        /// </summary>
        /// <param name="price">The price.</param>
        /// <returns>
        /// Price in string
        /// </returns>
		public static string GetPriceTransport( float price )
        {
            return string.Format("{0}", price);
        }

        /// <summary>
        /// Gets the stops in view model.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>Valid stops </returns>
        public static List<StopViewModel> GetStopsInViewModel(Route route)
        {
            var stops = route.Stops.Select(stop => new StopViewModel()
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
        public static List<WalkingRoutesViewModel> GetWalkingRoutesInViewModel(Route route)
        {
            var walkingRoutes = route.WalkingRoutes.Select(walk => new WalkingRoutesViewModel()
                {
                    Name = walk.Name,
                    Length = GetRoundDistance(walk.Length),
                    MapPoints = walk.MapPoints,
                    Speed = walk.Speed,
                    Steps = walk.Steps,
                    Time = GetRoundTime(walk.Time),
                    Type = walk.Type,  
                }).ToList();

            return walkingRoutes;
        }

        /// <summary>
        /// Gets the summary walking distance.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>Summary walking distance in route</returns>
        public static double GetSummaryWalkingDistance(Route route)
        {
            var summaryDistance = route.WalkingRoutes.Sum(walk => walk.Length);

            return summaryDistance;
        }
    }
}
