using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;

namespace CityTravel.Domain.Entities
{
    /// <summary>
    /// Point on the map
    /// </summary>
    public class MapPoint
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MapPoint"/> class. 
        ///   Constructs new map point
        /// </summary>
        public MapPoint()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapPoint"/> class. 
        /// Constructs new map point
        /// </summary>
        /// <param name="latitude">
        /// point latitude 
        /// </param>
        /// <param name="longitude">
        /// point longitude 
        /// </param>
        public MapPoint(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Point latitude
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets Point longitude
        /// </summary>
        public double Longitude { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Converts MapPoint to SqlGeography value
        /// </summary>
        /// <returns>
        /// SqlGeography point representation 
        /// </returns>
        public SqlGeography ToSqlGeography()
        {
            return SqlGeography.STPointFromText(new SqlChars("POINT(" + this + ")"), 4326);
        }

        /// <summary>
        /// Converts MapPoint to string value
        /// </summary>
        /// <returns>
        /// String representation 
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", this.Latitude, this.Longitude).Replace(',', '.');
        }

        #endregion
    }
}