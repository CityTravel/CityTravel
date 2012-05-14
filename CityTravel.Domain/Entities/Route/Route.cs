namespace CityTravel.Domain.Entities.Route
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using CityTravel.Domain.Helpres;
    using Microsoft.SqlServer.Types;

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
        /// Gets or sets the address A.
        /// </summary>
        /// <value>
        /// The address A.
        /// </value>
        [NotMapped]
        public string AddressA { get; set; }

        /// <summary>
        /// Gets or sets the address B.
        /// </summary>
        /// <value>
        /// The address B.
        /// </value>
        [NotMapped]
        public string AddressB { get; set; }

    }
}
