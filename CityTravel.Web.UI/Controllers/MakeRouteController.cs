using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CityTravel.Domain.Abstract;
using CityTravel.Domain.Entities;
using CityTravel.Web.UI.Models;

namespace CityTravel.Web.UI.Controllers
{
    /// <summary>
    /// Make route controller
    /// </summary>
    public class MakeRouteController : BaseController
    {
        #region Constants and Fields

        /// <summary>
        ///   Interface of route seach
        /// </summary>
        private readonly IRouteSeach routeSeach;

        /// <summary>
        /// Repository for routes.
        /// </summary>
        private readonly IProvider<Route> routeProvider;
        
        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MakeRouteController"/> class.
        /// </summary>
        /// <param name="routeSeach">The route seach.</param>
        /// <param name="routeProvider">The route provider.</param>
        public MakeRouteController(IRouteSeach routeSeach, IProvider<Route> routeProvider)
        {
            this.routeSeach = routeSeach;
            this.routeProvider = routeProvider;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Abouts the us.
        /// </summary>
        /// <returns>
        /// View about us 
        /// </returns>
        public ViewResult AboutUs()
        {
            return this.View("AboutUs");
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// View of page
        /// </returns>
        [OutputCache(Duration = 30, VaryByParam = "none")]
        public ViewResult Index()
        {
            return this.View("Index");
        }

        /// <summary>
        /// Indexes the specified make route view model.
        /// </summary>
        /// <param name="makeRouteViewModel">The make route view model.</param>
        /// <param name="validateCoords">The validate coords.</param>
        /// <returns>
        /// Json route result
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Index(MakeRouteViewModel makeRouteViewModel, ValidateServerHelper validateCoords)
        {
            // Verify the valid of entered data from client (coordinates)
            if (validateCoords.IsValidCoords(makeRouteViewModel) == ValidationResult.Success)
            {
                var startPoint = new MapPoint(
                    Convert.ToDouble(makeRouteViewModel.StartPointLatitude),
                    Convert.ToDouble(makeRouteViewModel.StartPointLongitude));
                var endPoint = new MapPoint(
                    Convert.ToDouble(makeRouteViewModel.EndPointLatitude),
                    Convert.ToDouble(makeRouteViewModel.EndPointLongitude));
                var args = new List<Transport> { Transport.Bus };
                var allRoutes = this.routeSeach.GetAppropriateRoutes(
                    this.routeProvider.All(), endPoint.ToSqlGeography(), startPoint.ToSqlGeography(), args);
                var routes = Route.MakeValid(allRoutes);

                return this.Json(routes);
            }
            else
            {
                return Json(makeRouteViewModel);
            }
        }
        #endregion
    }


}