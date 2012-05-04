namespace CityTravel.Domain.Entities.Route
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Types of transport
    /// </summary>
    [Table("TransportType")]
    public class TransportType : BaseEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Type
        /// </summary>
        [Required]
        public string Type { get; set; }

        #endregion
    }
}