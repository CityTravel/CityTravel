namespace CityTravel.Domain.Entities
{
    /// <summary>
    /// View model for stop
    /// </summary>
    public class StopViewModel
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