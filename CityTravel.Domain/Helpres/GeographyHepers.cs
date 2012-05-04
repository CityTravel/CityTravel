using Microsoft.SqlServer.Types;

namespace CityTravel.Domain.Helpres
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class GeographyHepers
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <param name="geo">
        /// The geography. 
        /// </param>
        /// <returns>
        /// Apropriate latitude for SQLGeography. 
        /// </returns>
        public static string GetLatitude(this SqlGeography geo)
        {
            return geo.Lat.ToString().Replace(',', '.');
        }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <param name="geo">
        /// The geography. 
        /// </param>
        /// <returns>
        /// Apropriate latitude for SQLGeography. 
        /// </returns>
        public static string GetLongitude(this SqlGeography geo)
        {
            return geo.Long.ToString().Replace(',', '.');
        }

        #endregion
    }
}