using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;

namespace CityTravel.Domain.Helpres
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class GeographyHelpers
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

        /// <summary>
        /// Gets the geography.
        /// </summary>
        /// <param name="geography">The geography.</param>
        /// <param name="binGeography">The bin geography.</param>
        /// <returns> An sql Geougraphy retrieved from byte array. </returns>
        public static SqlGeography GetGeography(SqlGeography geography, byte[] binGeography)
        {
			return geography ?? SqlGeography.STGeomFromWKB(new SqlBytes(binGeography), Settings.GeneralSettings.GetSpatialReferenceSystem);
        }

        /// <summary>
        /// Sets the bin from geography.
        /// </summary>
        /// <param name="value">The value of sqlgeography.</param>
        /// <param name="type">The type of retrieved geography.</param>
        /// <returns> The byte array retrieved from SqlGeography. </returns>
        public static byte[] SetBinFromGeography(SqlGeography value, string type)
        {
            byte[] returnBinary = new byte[1];

            switch (type)
            {
                case "stop":
                    returnBinary = value.STNumPoints() > 1
					? SqlGeography.STPolyFromText(value.STAsText(), Settings.GeneralSettings.GetSpatialReferenceSystem).STAsBinary().Buffer
					: SqlGeography.STPointFromText(value.STAsText(), Settings.GeneralSettings.GetSpatialReferenceSystem).STAsBinary().Buffer;
                    break;
                case "route":
					returnBinary = SqlGeography.STLineFromText(value.STAsText(), Settings.GeneralSettings.GetSpatialReferenceSystem).STAsBinary().Buffer;
                    break;
            }

            return returnBinary;
        }
        #endregion
    }
}