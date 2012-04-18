using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CityTravel.Web.UI.Models
{
    /// <summary>
    /// The feedback view model.
    /// </summary>
    public class FeedbackViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Email.
        /// </summary>
        [StringLength(60, ErrorMessage = "Слишком длинный e-mail адрес.")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", 
            ErrorMessageResourceName = "YouEnterWrongAdress", ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(Name = "ModelEmail", ResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        [StringLength(59, ErrorMessage = "Введите имя короче 60 символов")]
        [Display(Name = "ModelName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets SelectedValue.
        /// </summary>
        [Required(ErrorMessageResourceName = "ChooseCategoryError", 
            ErrorMessageResourceType = typeof(Resources.Resources))]
        public string SelectedValue { get; set; }

        /// <summary>
        /// Gets or sets Text.
        /// </summary>
        [Required(ErrorMessageResourceName = "YouMustEnterMessage", 
            ErrorMessageResourceType = typeof(Resources.Resources))]
        [StringLength(7999, ErrorMessage = "Введите сообщение меньше 8000 символов.")]
        [Display(Name = "ModelMessage", ResourceType = typeof(Resources.Resources))]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        [Display(Name = "ChooseCategory", ResourceType = typeof(Resources.Resources))]
        public IEnumerable<SelectListItem> Type { get; set; }

        #endregion
    }
}