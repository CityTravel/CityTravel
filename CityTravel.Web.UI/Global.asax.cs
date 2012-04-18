using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using CityTravel.Domain.Abstract;
using CityTravel.Domain.DomainModel;
using CityTravel.Domain.Repository;
using CityTravel.Domain.Services.Autocomplete;
using CityTravel.Domain.Services.Segment;
using CityTravel.Web.UI.Helpers;

using Ninject;
using Ninject.Web.Mvc;
using entities = CityTravel.Domain.Entities;

namespace CityTravel.Web.UI
{
    /// <summary>
    /// The mvc application.
    /// </summary>
    public class MvcApplication : NinjectHttpApplication
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register global filters.
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// The register routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "MakeRoute", action = "Index", id = UrlParameter.Optional });
        }

        /// <summary>
        /// The get vary by custom string.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="arg">
        /// The arg.
        /// </param>
        /// <returns>
        /// vary by custom string.
        /// </returns>
        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            // It seems this executes multiple times and early, so we need to extract language again from cookie.
            if (arg == "culture")
            {
                // culture name (e.g. "en-US") is what should vary caching
                string cultureName = null;

                // Attempt to read the culture cookie from Request
                HttpCookie cultureCookie = this.Request.Cookies["_culture"];
                if (cultureCookie != null)
                {
                    cultureName = cultureCookie.Value;
                }
                else
                {
                    var userLanguages = this.Request.UserLanguages;
                    if (userLanguages != null)
                    {
                        cultureName = userLanguages[0]; // obtain it from HTTP header AcceptLanguages
                    }
                }

                // Validate culture name
                cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

                return cultureName.ToLower(); // use culture name as cache key, "es", "en-us", "es-cl", etc.
            }

            return base.GetVaryByCustomString(context, arg);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create kernel.
        /// </summary>
        /// <returns>
        /// ninject kernel
        /// </returns>
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Bind(typeof(IProvider<>)).To(typeof(GenericRepository<>));
           // kernel.Bind<IDataBaseContext>().ToConstant(DbContextFactory.GetInstance.GetContext());
            kernel.Bind<IAutocomplete>().To<CacheAutoComplete>().InThreadScope();
            kernel.Bind<IRouteSeach>().To<RouteSeach>();
            kernel.Bind<IDataBaseContext>().To<DataBaseContext>().InThreadScope();

            return kernel;
        }

        /// <summary>
        /// The on application started.
        /// </summary>
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        #endregion
    }
}