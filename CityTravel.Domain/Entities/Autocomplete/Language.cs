namespace CityTravel.Domain.Entities.Autocomplete
{
    using System.ComponentModel.DataAnnotations.Schema;

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
