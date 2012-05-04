using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityTravel.Domain.Entities.InvalidWords
{
    /// <summary>
    /// Invalid characters in text instruction.
    /// </summary>
    public class InvalidCharacter : BaseEntity
    {
        /// <summary>
        /// Gets or sets the valid word.
        /// </summary>
        /// <value>
        /// The valid word.
        /// </value>
        public string ValidWord { get; set; }

        /// <summary>
        /// Gets or sets the invalid word.
        /// </summary>
        /// <value>
        /// The invalid word.
        /// </value>
        public string InvalidWord { get; set; }
    }
}
