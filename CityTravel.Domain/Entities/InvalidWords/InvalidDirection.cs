using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityTravel.Domain.Entities.InvalidWords
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Invalid directions in text instruction.
    /// </summary>
    [Table("InvalidDirection")]
    public class InvalidDirection : BaseEntity
    {
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public string Direction { get; set; }
    }
}
