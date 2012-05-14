namespace CityTravel.Domain.Helpres
{
    using System;
    using CityTravel.Translations;

    /// <summary>
    /// Dimension converter.
    /// </summary>
    public static class DimensionConverter
    {
        /// <summary>
        /// Count minutes in hour.
        /// </summary>
        private const int Hour = 60;

        /// <summary>
        /// Count metres in kilometer.
        /// </summary>
        private const int Kilometr = 1000;

        /// <summary>
        /// Gets the round time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>Time in string </returns>
        public static string GetRoundTime(TimeSpan time)
        {
            
            var result = time.TotalMinutes >= Hour
                             ? string.Format(
                                 Math.Round(time.TotalMinutes / Hour, 1) + "{0}", Resources.Hour)
                             : string.Format(time.TotalMinutes.ToString("F0") + "{0}", Resources.Minute);

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
            var result = distance >= Kilometr
                             ? string.Format(Math.Round(distance / Kilometr, 1) + "{0}", Resources.Kilometr)
                             : string.Format(distance.ToString("F0") + "{0}", Resources.Metres);

            return result;
        }

        /// <summary>
        /// Gets the price for Transport Type
        /// </summary>
        /// <param name="price">The price.</param>
        /// <returns>
        /// Price in string
        /// </returns>
        public static string GetTransportPrice(float price)
        {
            return string.Format(price + "{0}", Resources.Price);
        }
    }
}
