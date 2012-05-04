namespace CityTravel.Domain.Services.Autocomplete.Concrete
{
    using System.Collections.Generic;

    /// <summary>
    /// Class to pass it throw JsonResult via NewTon library
    /// </summary>
    public class AutocompleteViewModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutocompleteViewModel"/> class.
        /// </summary>
        public AutocompleteViewModel()
        {
            this.Predictions = new List<object>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets list to make our json-interface be the same with google json.
        /// </summary>
        public List<object> Predictions { get; set; }

        #endregion
    }
}