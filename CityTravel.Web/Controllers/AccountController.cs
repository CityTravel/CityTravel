using System.Web.Mvc;

namespace CityTravel.Web.Controllers
{
    using CityTravel.Domain.Services.AuthenticationProvider.Abstract;
    using CityTravel.Translations;
    using CityTravel.Web.Models;

    /// <summary>
    /// Controller witch responsible forms authentication.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Interface of forms authentication provider.
        /// </summary>
        private readonly IAuthenticationProvider authenticationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="authenticationProvider">The authentication provider.</param>
        public AccountController(IAuthenticationProvider authenticationProvider)
        {
            this.authenticationProvider = authenticationProvider;
        }

        /// <summary>
        /// Logs the on.
        /// </summary>
        /// <returns>
        /// View of log on.
        /// </returns>
        [HttpGet]
        public ActionResult LogOn()
        {
            return this.View();
        }


        /// <summary>
        /// Logs the on.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// View or concrete action.
        /// </returns>
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (this.authenticationProvider.Authentication(model.UserName, model.Password))
                {
                    return this.Redirect(returnUrl ?? Url.Action("Index", "FeedbackList"));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Resources.AuthenticationError);
                    return this.View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
