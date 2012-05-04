namespace CityTravel.Domain.Entities.Feedback
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The feedback.
    /// </summary>
    [Table("Feedback")]
    public class Feedback : BaseEntity
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets the email.
        /// </summary>
        /// <value> The email. </value>
        public string Email { get; set; }

        /// <summary>
        ///   Gets or sets the name.
        /// </summary>
        /// <value> The name. </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///   Gets or sets the text.
        /// </summary>
        /// <value> The text. </value>
        [Required]
        public string Text { get; set; }

        /// <summary>
        ///   Gets or sets the type.
        /// </summary>
        /// <value> The type. </value>
        [Required]
        public int Type { get; set; }

        #endregion
    }
}