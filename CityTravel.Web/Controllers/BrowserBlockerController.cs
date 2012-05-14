using System.Web.Mvc;

namespace CityTravel.Web.Controllers
{
    /// <summary>
    /// Controller which implementations browser bloker behavior.
    /// </summary>
    public class BrowserBlockerController : Controller
    {
        /// <summary>
        /// IEs the blocker.
        /// </summary>
        /// <returns>
        /// IEBlocker view.
        /// </returns>
        public ActionResult IEBlocker()
        {
            return View();
        }

    }
}
