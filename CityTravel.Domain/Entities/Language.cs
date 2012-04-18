using System.ComponentModel.DataAnnotations;

namespace CityTravel.Domain.Entities
{
    /// <summary>
    /// Entity for Language 
    /// </summary>
    [Table("Language")]
    public class Language : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}
