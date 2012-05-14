using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CityTravel.Domain.Helpres;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CityTravel.Web.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using CityTravel.Domain.Services.Autocomplete.Abstract;

    /// <summary>
    /// The autocomplete controller.
    /// </summary>
    public class AutocompleteController : BaseController
    {
        #region Constants and Fields

        /// <summary>
        /// The autocomplete.
        /// </summary>
        private readonly IAutocomplete autocomplete;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutocompleteController"/> class.
        /// </summary>
        /// <param name="autocomplete">
        /// The autocomplete. 
        /// </param>
        public AutocompleteController(IAutocomplete autocomplete)
        {
            this.autocomplete = autocomplete;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the predictions.
        /// </summary>
        /// <param name="address">
        /// The address. 
        /// </param>
        /// <returns>
        /// predictions for address in json format
        /// </returns>
        [OutputCache(Duration = 3000)]
        public JsonResult GetPredictions(string address)
        {
            var retVal = string.Empty;

            // Verify that address has no digits and not empty string
            ValidateServerHelper vsh = new ValidateServerHelper();
            if( vsh.AddressValidation(address) == ValidationResult.Success )
            {

                // used to build entire input
                var sb = new StringBuilder();
                var fromDatabase = this.autocomplete.GetAdressFromDatabase(address);

                // used on each read operation
                if (fromDatabase == null)
                {  
                    var valueBeforeParsing = GoogleMapHelper.GetGoogleAnswerForAutocomplete(address);

                    var resp = JObject.Parse(valueBeforeParsing);
                    var status = (string)resp["status"];
                    if (status == "OK")
                    {
                        var descriptions = resp["predictions"];
                        var suggestions = descriptions.Select(item => ((string)item["description"]).Replace(",", string.Empty).Replace("\n", string.Empty)).ToList();

                        this.autocomplete.AddSuggestionsToDatabase(suggestions);
                    }           

                    retVal = valueBeforeParsing;
                }
                else
                {
                    var jsonAnswer = JsonConvert.SerializeObject(fromDatabase);
                    return this.Json(fromDatabase, JsonRequestBehavior.AllowGet);
                }

                return this.Json(retVal, JsonRequestBehavior.AllowGet);
            }
            else
                return null;
        }

        /// <summary>
        /// Gets the predictions for end.
        /// </summary>
        /// <param name="endPointAddress">
        /// The end point address. 
        /// </param>
        /// <returns>
        /// predictions for end address
        /// </returns>
        public JsonResult GetPredictionsForEnd(string endPointAddress)
        {
            return this.GetPredictions(endPointAddress);
        }

        /// <summary>
        /// Gets the predictions for start.
        /// </summary>
        /// <param name="startPointAddress">
        /// The start point address. 
        /// </param>
        /// <returns>
        /// predictions for start address
        /// </returns>
        public JsonResult GetPredictionsForStart(string startPointAddress)
        {
            return this.GetPredictions(startPointAddress);
        }

        #endregion
    }
}