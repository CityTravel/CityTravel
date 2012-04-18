namespace CityTravel.Domain.Entities
{
    /// <summary>
    /// Route type
    /// </summary>
    public enum Transport
    {
        /// <summary>
        /// The all.
        /// </summary>
        All = 0, 

        /// <summary>
        ///   Walking route
        /// </summary>
        Walking = 1, 

        /// <summary>
        ///   Ride on bus
        /// </summary>
        Bus = 2, 

        /// <summary>
        ///   Ride on subway
        /// </summary>
        Subway = 3, 

        /// <summary>
        ///   Ride on trolleybus
        /// </summary>
        Trolleybus = 4, 

        /// <summary>
        ///   Ride on tramway
        /// </summary>
        Tramway = 5
    }
}