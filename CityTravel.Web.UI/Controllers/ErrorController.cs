using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityTravel.Web.UI.Controllers
{
    public class ErrorController : Controller
    {

        public ActionResult DefaultError()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }


        public ActionResult Errors500()
        {
            return View();
        }

    }
}
