using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CityTravel.Domain.Settings;
using CityTravel.Web.UI.Models;

namespace CityTravel.Web.UI.Controllers
{
    /// <summary>
    /// The Server Validation class.
    /// </summary>
    public class ValidateServerHelper : ValidationAttribute
    {
       /// <summary>
       /// Pattern for Regex expressions
       /// </summary>
       private string pattern = "^[0-9]+$";  
       /// <summary>
       /// The overrided Server Validation method.
       /// </summary>
	   public ValidationResult IsValidCoords(MakeRouteViewModel makerouteVieModel)
       {
		   var jsonObj = makerouteVieModel; // cast the object to type MakeRouteViewModel

            /// Coordinates of the boundary of Dnepropetrovsk
            var polyCoords = new
            {
                SWLong = GeneralSettings.GetSouthWestLongitude,
                SWLat = GeneralSettings.GetSouthWestLatitude,
                NELong = GeneralSettings.GetNorthEastLongitude,
                NELat = GeneralSettings.GetNorthEastLatitude
            };

            double startPointLon;
            double startPointLat;
            double endPointLon;
            double endPointLat;

			// Convert the the Latitudes and Longitudes to type of double
			bool convertFlg = (double.TryParse(jsonObj.StartPointLongitude, out startPointLon)
            & double.TryParse(jsonObj.StartPointLatitude, out startPointLat)
            & double.TryParse(jsonObj.EndPointLongitude, out endPointLon)
            & double.TryParse(jsonObj.EndPointLatitude, out endPointLat));

            /// Verify that the Point A and Point B are in the boundries of Dnepropetrovsk
            if (convertFlg == true 
			&& startPointLon >= polyCoords.SWLong
            && startPointLon <= polyCoords.NELong
            && startPointLat <= polyCoords.NELat
            && startPointLat >= polyCoords.SWLat
            && endPointLon >= polyCoords.SWLong
            && endPointLon <= polyCoords.NELong
            && endPointLat <= polyCoords.NELat
            && endPointLat >= polyCoords.SWLat)
                return ValidationResult.Success;
            else
            {
                return new ValidationResult(Resources.Resources.ValidationCoords);
            }
       }

       /// <summary>
       /// Verify that address has no digits and not empty string
       /// </summary>
       /// <param name="address"></param>
       /// <returns></returns>
       public ValidationResult AddressValidation( string address )
       {
           // Verify that no digits were entered
		   if (!this.OnlyDigitsExist(address) && address != string.Empty)
               return ValidationResult.Success;
           return new ValidationResult(Resources.Resources.ValidationAddress);
       }

       /// <summary>
       /// Verify the entered values for feedback
       /// </summary>
       /// <param name="feedbackViewModel"></param>
       /// <returns></returns>
       public ValidationResult FeedBackDataIsValid( FeedbackViewModel feedbackViewModel)
       {

           var fbm = (FeedbackViewModel)feedbackViewModel;

           /// Verify that the entered data is valid
           if(fbm.Text != string.Empty
		        && (OnlyDigitsExist(fbm.SelectedValue.ToString()) && fbm.SelectedValue != null))
               return ValidationResult.Success;
           return new ValidationResult(Resources.Resources.ValidationFeedback);
       }

       /// <summary>
       /// Verify if any digits exist in the string
       /// </summary>
       /// <param name="s"></param>
       /// <returns></returns>
       private bool OnlyDigitsExist( string s )
       {
           bool val = false;

           Regex anydigit = new Regex(pattern);
           val = anydigit.IsMatch(s);

           return val;
       }
    }
}
