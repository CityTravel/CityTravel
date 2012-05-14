using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CityTravel.Web.Models
{
    /// <summary>
    /// The add stop view model.
    /// </summary>
    public class AddStopViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Selected.
        /// </summary>
        [Display(Name = "Тип маршрута: ")]
        public string Selected { get; set; }

        /// <summary>
        /// Gets or sets StopGeography.
        /// </summary>
        [Required]
        public string StopGeography { get; set; }

        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        [Display(Name = "Тип маршрута: ")]
        public IEnumerable<SelectListItem> Type { get; set; }

        #endregion
    }
}