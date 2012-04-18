using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;

namespace CityTravel.Domain.Entities
{
    /// <summary>
    /// Entity for Place or Street
    /// </summary>
    [Table("Place")]
    public class Place : BaseEntity
    {
        #region Constants and Fields

        /// <summary>
        /// The place geography.
        /// </summary>
        private SqlGeography placeGeography;

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the Count
        /// </summary>
        /// <value> The Id of Count. </value>
        public int? Count { get; set; }

        /// <summary>
        ///   Gets or sets the Id of Language.
        /// </summary>
        /// <value> The Id of Language. </value>
        public int? LangId { get; set; }

        /// <summary>
        ///   Gets or sets the name.
        /// </summary>
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Building Address in binary view
        /// </summary>
        public byte[] PlaceBin { get; set; }

        /// <summary>
        /// Gets or sets SqlGeography type for Building
        /// </summary>
        [NotMapped]
        public SqlGeography PlaceGeography
        {
            get
            {
                return this.placeGeography
                       ?? (this.placeGeography = SqlGeography.STGeomFromWKB(new SqlBytes(this.PlaceBin), 4326));
            }

            set
            {
                this.placeGeography = value;
                this.PlaceBin = SqlGeography.STPointFromText(this.placeGeography.STAsText(), 4326).STAsBinary().Buffer;
            }
        }

        /// <summary>
        ///   Gets or sets the Id of Place in Russian.
        /// </summary>
        /// <value> The Id of Place in Russian. </value>
        public int? PlaceInRussainId { get; set; }

        /// <summary>
        ///   Gets or sets the Type.
        /// </summary>
        /// <value> The Type. </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets place.
        /// </summary>
        [ForeignKey("LangId")]
        public Place place { get; set; }

        #endregion
    }
}