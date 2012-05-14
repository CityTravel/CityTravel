using System.Collections.Generic;
using System.Web.Mvc;

namespace CityTravel.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The add route view model.
    /// </summary>
    public class AddRouteViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        [Required]
        [Display(Name = "Имя\\Номер маршрута: ")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets RouteGeography.
        /// </summary>
        [Required]
        [Display(Name = "Координаты: ")]
        public string RouteGeography { get; set; }

        /// <summary>
        /// Gets or sets Selected.
        /// </summary>
        [Display(Name = "Тип маршрута: ")]
        public string Selected { get; set; }

        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        [Display(Name = "Тип маршрута: ")]
        public IEnumerable<SelectListItem> Type { get; set; }

        /// <summary>
        /// Gets or sets the routes.
        /// </summary>
        /// <value>
        /// The routes.
        /// </value>
        [Required]
        public IEnumerable<SimpleRouteViewModel> Routes { get; set; }

        
        #endregion
    }
}