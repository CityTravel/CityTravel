using System.ComponentModel.DataAnnotations;

namespace CityTravel.Domain.Entities
{
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