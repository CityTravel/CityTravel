namespace CityTravel.Domain.Entities.Route
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Walking route
    /// </summary>
    public class WalkingRoute
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the map points.
        /// </summary>
        /// <value>
        /// The map points.
        /// </value>
        public IEnumerable<MapPoint> MapPoints { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Transport Type { get; set; }
        
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public int Speed { get; set; }

        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        /// <value>
        /// The steps.
        /// </value>
        public List<Step> Steps { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public double Length { get; set; }
    }
}
