namespace CityTravel.Domain.Entities.Route
{
    /// <summary>
    /// Steps of route
    /// </summary>
    public class Step
    {
        /// <summary>
        /// Gets or sets the instruction.
        /// </summary>
        /// <value>
        /// The instruction.
        /// </value>
        public string Instruction { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets the lenght.
        /// </summary>
        /// <value>
        /// The lenght.
        /// </value>
        public string Length { get; set; }
    }
}