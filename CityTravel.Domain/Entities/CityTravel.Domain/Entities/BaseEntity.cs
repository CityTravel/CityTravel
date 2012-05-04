using System.ComponentModel.DataAnnotations;

namespace CityTravel.Domain.Entities
{
    /// <summary>
    /// Base abstract class for all entities
    /// </summary>
    public abstract class BaseEntity
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets the entity identifier
        /// </summary>
        [Key]
        [Column("Id")]
        public virtual int Id { get; set; }

        #endregion
    }
}