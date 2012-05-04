namespace CityTravel.Domain.Entities.SimpleModel
{
    using CityTravel.Domain.Entities.Route;

    /// <summary>
    /// View model for stop
    /// </summary>
    public class SimpleStop
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
       
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public MapPoint Points { get; set; }
    }
}