using System.ComponentModel.DataAnnotations;
using CityTravel.Web.UI.Controllers;
using CityTravel.Web.UI.Models;
using NUnit.Framework;

namespace CityTravel.Tests.Domain
{
    /// <summary>
    /// The Server Validation Tests.
    /// </summary>
	[TestFixture]
	public class ServerValidationTests : ValidationAttribute
	{
		/// <summary>
		/// Can Validate Coordinates of the City.
		/// </summary>
		[Test]
		public void CanValidateCityCoords()
		{
            // Pointer to MakeRouteViewModel
            MakeRouteViewModel makeRoute = new MakeRouteViewModel();

            makeRoute.EndPointLatitude = "35,0394344329834";
            makeRoute.EndPointLongitude = "48,46062862564409";
            makeRoute.StartPointLatitude = "35,03368377685547";
            makeRoute.StartPointLongitude = "48,45146411408362";

            // Create the instance of the ValidateServerHelper and compare the expected result with real
            ValidateServerHelper validateServer = new ValidateServerHelper();
			ValidationResult vr = validateServer.IsValidCoords(makeRoute);
            Assert.IsTrue( vr == ValidationResult.Success );
		}

		/// <summary>
		/// Can Validate the entered Address to the Autocomplete.
		/// </summary>
		[Test]
		public void CanValidateAutocompleteAddress()
		{
            // Create the instance of the ValidateServerHelper and compare the expected result with real
            ValidateServerHelper validateServer = new ValidateServerHelper();
            ValidationResult vr = validateServer.AddressValidation( "Кирова 59" );
            Assert.IsTrue(vr == ValidationResult.Success);
        }

		/// <summary>
		/// Can Validate the entered Data to the Feedback Form.
		/// </summary>
		[Test]
		public void CanValidateDataToFeedmackForm()
		{
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel();

            feedbackViewModel.Email = "qwerty@ua.fm";
            feedbackViewModel.Name = "Вася Пупкин";
            feedbackViewModel.Text = "Какой то текст";
            feedbackViewModel.SelectedValue = "1";

			// Create the instance of the ValidateServerHelper and compare the expected result with real one
			ValidateServerHelper validateServer = new ValidateServerHelper();
			// Verify that the entered data is valid
			ValidationResult vr = validateServer.FeedBackDataIsValid(feedbackViewModel);
			Assert.IsTrue(vr == ValidationResult.Success);
		}
	}
}


