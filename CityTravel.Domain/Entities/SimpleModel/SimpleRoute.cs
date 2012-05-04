namespace CityTravel.Domain.Entities.SimpleModel
{
    using System.Collections.Generic;

    using CityTravel.Domain.Entities.Route;

    /// <summary>
    /// Route view model
    /// </summary>
    public class SimpleRoute
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the stops.
        /// </summary>
        /// <value>
        /// The stops.
        /// </value>
        public ICollection<SimpleStop> Stops { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TransportType Type { get; set; }

        /// <summary>
        /// Gets or sets the map points.
        /// </summary>
        /// <value>
        /// The map points.
        /// </value>
        public ICollection<MapPoint> MapPoints { get; set; }

        /// <summary>
        /// Gets or sets the waiting time.
        /// </summary>
        /// <value>
        /// The waiting time.
        /// </value>
        public string WaitingTime { get; set; }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public int Speed { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        /// <value>
        /// The steps.
        /// </value>
        public IList<Step> Steps { get; set; }

        /// <summary>
        /// Gets or sets the walking routes.
        /// </summary>
        /// <value>
        /// The walking routes.
        /// </value>
        public ICollection<SimpleWalkingRoute> WalkingRoutes { get; set; }

        /// <summary>
        /// Gets or sets the route time.
        /// </summary>
        /// <value>
        /// The route time.
        /// </value>
        public string RouteTime { get; set; }

        /// <summary>
        /// Gets or sets the total minutes.
        /// </summary>
        /// <value>
        /// The total minutes.
        /// </value>
        public int TotalMinutes { get; set; }

        /// <summary>
        /// Gets or sets the bus length.
        /// </summary>
        /// <value>
        /// The bus length.
        /// </value>
        public string BusLength { get; set; }

        /// <summary>
        /// Gets or sets the all route length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public string AllLength { get; set; }

        /// <summary>
        /// Gets or sets the length of the summary walking.
        /// </summary>
        /// <value>
        /// The length of the summary walking.
        /// </value>
        public string SummaryWalkingLength { get; set; }

        /// <summary>
		/// Gets or sets the route Price.
		/// </summary>
		/// <value>
		/// The route Price.
		/// </value>
		public string Price { get; set; }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        public string Currency { get; private set; }

        /// <summary>
        /// Gets or sets the route Cost.
        /// </summary>
        /// <value>
        /// The route Cost.
        /// </value>
        public float Cost { get; set; }
	}
}
