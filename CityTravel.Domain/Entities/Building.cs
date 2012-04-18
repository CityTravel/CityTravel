using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;

namespace CityTravel.Domain.Entities
{
    /// <summary>
    /// Entity for Building
    /// </summary>
    [Table("Building")]
    public class Building : BaseEntity
    {
        #region Constants and Fields

        /// <summary>
        /// The building geography.
        /// </summary>
        private SqlGeography buildingGeography;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Building Address in binary view
        /// </summary>
        public byte[] BuildingBin { get; set; }

        /// <summary>
        /// Gets or sets SqlGeography type for Building
        /// </summary>
        [NotMapped]
        public SqlGeography BuildingGeography
        {
            get
            {
                return this.buildingGeography
                       ?? (this.buildingGeography = SqlGeography.STGeomFromWKB(new SqlBytes(this.BuildingBin), 4326));
            }

            set
            {
                this.buildingGeography = value;
                this.BuildingBin = this.buildingGeography.STNumPoints() > 1
                                       ? SqlGeography.STPolyFromText(this.buildingGeography.STAsText(), 4326).STAsBinary().Buffer
                                       : SqlGeography.STPointFromText(this.buildingGeography.STAsText(), 4326).STAsBinary().Buffer;
            }
        }

        /// <summary>
        ///   Gets or sets the Index number of building.
        /// </summary>
        /// <value> The index. </value>
        public string BuildingIndexNumber { get; set; }

        /// <summary>
        ///   Gets or sets the Count
        /// </summary>
        /// <value> The Id of Count. </value>
        public int Count { get; set; }

        /// <summary>
        ///   Gets or sets the Number of building.
        /// </summary>
        /// <value> The number. </value>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets Place.
        /// </summary>
        [ForeignKey("PlaceId")]
        public Place Place { get; set; }

        /// <summary>
        ///   Gets or sets the Id of Place in Russian.
        /// </summary>
        /// <value> The Id of Place in Russian. </value>
        public int? PlaceId { get; set; }

        #endregion
    }
}