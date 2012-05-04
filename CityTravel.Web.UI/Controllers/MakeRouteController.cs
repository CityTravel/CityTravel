using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CityTravel.Web.UI.Models;

namespace CityTravel.Web.UI.Controllers
{
    using System.Linq;
    using CityTravel.Domain.Entities.InvalidWords;
    using CityTravel.Domain.Entities.Route;
    using CityTravel.Domain.Helpres;
    using CityTravel.Domain.Repository.Abstract;
    using CityTravel.Domain.Services.Segment.Abstract;

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

        /// <summary>
        /// Interface of repository of invalid directions.
        /// </summary>
        private readonly IProvider<InvalidDirection> directionProvider;

        /// <summary>
        /// Interface of repository of invalid characters.
        /// </summary>
        private readonly IProvider<InvalidCharacter> invalidCharacterProvider; 
        #endregion


        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MakeRouteController"/> class.
        /// </summary>
        /// <param name="routeSeach">The route seach.</param>
        /// <param name="routeProvider">The route provider.</param>
        /// <param name="directionProvider">The direction provider.</param>
        /// <param name="invalidCharacterProvider">The invalid character provider.</param>
        public MakeRouteController(
            IRouteSeach routeSeach,
            IProvider<Route> routeProvider,
            IProvider<InvalidDirection> directionProvider,
            IProvider<InvalidCharacter> invalidCharacterProvider)
        {
            this.routeSeach = routeSeach;
            this.routeProvider = routeProvider;
            this.directionProvider = directionProvider;
            this.invalidCharacterProvider = invalidCharacterProvider;
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
            if (validateCoords.IsValidCoords(makeRouteViewModel) == ValidationResult.Success)
            {
                var startPoint = new MapPoint(
                    Convert.ToDouble(makeRouteViewModel.StartPointLatitude),
                    Convert.ToDouble(makeRouteViewModel.StartPointLongitude));
                var endPoint = new MapPoint(
                    Convert.ToDouble(makeRouteViewModel.EndPointLatitude),
                    Convert.ToDouble(makeRouteViewModel.EndPointLongitude));
                var args = new List<Transport> { Transport.All };
                var invalidDirections =
                    this.directionProvider.All().ToList().Select(direction => direction.Direction).ToList();
                var invalidWords =
                    this.invalidCharacterProvider.All().ToList().Select(invalidWord => invalidWord.InvalidWord).ToList();
                var validWords =
                    this.invalidCharacterProvider.All().ToList().Select(validWord => validWord.ValidWord).ToList();
                var allRoutes = this.routeSeach.GetAppropriateRoutes(
                    this.routeProvider.All(),
                    invalidDirections,
                    validWords,
                    invalidWords,
                    endPoint.ToSqlGeography(),
                    startPoint.ToSqlGeography(),
                    args);
                var routes = ModelConverter.Convert(allRoutes);

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