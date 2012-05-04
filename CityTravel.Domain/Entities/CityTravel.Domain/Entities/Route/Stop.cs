namespace CityTravel.Domain.Entities.Route
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CityTravel.Domain.Helpres;

    using Microsoft.SqlServer.Types;

    /// <summary>
    /// Entity for Route 
    /// </summary>
    [Table("Stop")]
    public class Stop : BaseEntity
    {
        /// <summary>
        /// Geography of stop
        /// </summary>
        private SqlGeography stopGeography;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the stop bin.
        /// </summary>
        /// <value>
        /// The stop bin.
        /// </value>
        public byte[] StopBin { get; set; }

        /// <summary>
        /// Gets or sets the stop geography.
        /// </summary>
        /// <value>
        /// The stop geography.
        /// </value>
        [NotMapped]
        public SqlGeography StopGeography
        {
            get
            {
                return GeographyHelpers.GetGeography(this.stopGeography, this.StopBin);
            }

            set
            {
                this.stopGeography = value;
                this.StopBin = GeographyHelpers.SetBinFromGeography(value, "stop");
            }
        }

        /// <summary>
        /// Gets or sets StopType.
        /// </summary>
        public int? StopType { get; set; }

        /// <summary>
        /// Gets or sets the routes.
        /// </summary>
        /// <value>
        /// The routes.
        /// </value>
        public virtual List<Route> Routes { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [ForeignKey("StopType")]
        public virtual TransportType Type { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        [NotMapped]
        public MapPoint Points { get; set; }
    }
}
