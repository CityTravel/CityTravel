using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using CityTravel.Web.UI.Helpers;

namespace CityTravel.Web.UI.Controllers
{
    /// <summary>
    /// The base controller.
    /// </summary>
    public class BaseController : Controller
    {
        #region Methods

        /// <summary>
        /// The execute core.
        /// </summary>
        protected override void ExecuteCore()
        {
            string cultureName = null;

            var cultureCookie = this.Request.Cookies["_culture"];
            var userLanguages = this.Request.UserLanguages;
            if (userLanguages != null)
            {
                cultureName = cultureCookie != null ? cultureCookie.Value : userLanguages[0];
            }

            cultureName = CultureHelper.GetImplementedCulture(cultureName);

            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            base.ExecuteCore();
        }

        #endregion
    }
}