using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Web.Mvc;
using CityTravel.Domain.Settings;
using CityTravel.Web.UI.Models;
using Microsoft.SqlServer.Types;
using SQLSpatialTools;

namespace CityTravel.Web.UI.Controllers
{
    using System.Linq;

    using CityTravel.Domain.Entities.Route;
    using CityTravel.Domain.Repository.Abstract;

    /// <summary>
    /// Controller to add new routes and stops.
    /// </summary>
    [Authorize]
    public class AddRouteController : BaseController
    {
        #region Constants and Fields
        private IProvider<Route> routeRepository;
        private IProvider<Stop> stopRepository;

        /// <summary>
        ///   Create drop-down list elements.
        /// </summary>
        private readonly List<SelectListItem> itemsForDropDown = new List<SelectListItem>
        {
                new SelectListItem
                    {
                       Text = Resources.Resources.ShuttleTransport, Value = ((int)Transport.Bus).ToString() 
                    }, 
                new SelectListItem
                    {
                       Text = Resources.Resources.TroleyBus, Value = ((int)Transport.Trolleybus).ToString() 
                    }, 
                new SelectListItem { Text = Resources.Resources.Tramvay, Value = ((int)Transport.Tramway).ToString() }, 
                new SelectListItem { Text = Resources.Resources.Metro, Value = ((int)Transport.Subway).ToString() }
        };

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRouteController"/> class.
        /// </summary>
        public AddRouteController(IProvider<Route> routeRepository, IProvider<Stop> stopRepository)
        {
            this.routeRepository = routeRepository;
            this.stopRepository = stopRepository;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds the route.
        /// </summary>
        /// <returns>
        /// View result. 
        /// </returns>
        public ViewResult AddRoute()
        {
            var addRouteViewModel = new AddRouteViewModel
                { Type = this.itemsForDropDown, Selected = ((int)Transport.Bus).ToString() };
            var simpleRoutes = this.GetSimpleRoutes(this.routeRepository.All().ToList());
            addRouteViewModel.Routes = simpleRoutes;

            return View("AddRoute", addRouteViewModel);
            
        }

        /// <summary>
        /// Method invokes when AddRoute-form is submitted. Tryin' to add new route to DB.
        /// </summary>
        /// <param name="addModel">
        /// Filled view-model form. 
        /// </param>
        /// <returns>
        /// View result. 
        /// </returns>
        [HttpPost]
        public ActionResult AddRoute(AddRouteViewModel addModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View("AddRoute", addModel);
            }

            var routeText = string.Format("LINESTRING({0})", addModel.RouteGeography);
            this.routeRepository.Add(
                new Route
                    {
                        Name = addModel.Name,
                        RouteGeography = SqlGeography.STLineFromText(new SqlChars(routeText), 4326),
                        RouteType = int.Parse(addModel.Selected)
                    });
            this.routeRepository.Save();
            return this.RedirectToAction("AddRoute");
        }

        /// <summary>
        /// Adds the stop.
        /// </summary>
        /// <returns>
        /// View result. 
        /// </returns>
        public ViewResult AddStop()
        {
            var addStopViewModel = new AddStopViewModel
                { Type = this.itemsForDropDown, Selected = ((int)Transport.Bus).ToString() };
            return View("AddStop", addStopViewModel);
        }


        /// <summary>
        /// Adds the stop.
        /// </summary>
        /// <param name="addStop">
        /// The add stop. 
        /// </param>
        /// <returns>
        /// View result. 
        /// </returns>
        [HttpPost]
        public ActionResult AddStop(AddStopViewModel addStop)
        {
            if (!this.ModelState.IsValid)
            {
                return View("AddStop", addStop);
            }

            string name = addStop.Name;
            IEnumerable<TmpAddRouteStopViewModel> routesList = this.ConnectStopsWithRoutes(addStop);
            foreach (TmpAddRouteStopViewModel intersectObect in routesList)
            {
                var intermediateToFineIntersects = new List<Route> { intersectObect.Route };
                var intermidStop = new Stop
                    {
                        Name = name,
                        StopGeography = intersectObect.StopPoint,
                        StopType = int.Parse(addStop.Selected),
                        Routes = intermediateToFineIntersects
                    };
                this.stopRepository.Add(intermidStop);
            }

            this.stopRepository.Save();
            return this.RedirectToAction("AddStop");
        }

        /// <summary>
        /// Gets all routes.
        /// </summary>
        /// <returns>
        /// Json with all routes. 
        /// </returns>
        public JsonResult GetAllRoutes()
        {
            var routeList = new List<object>();
            var routes = this.routeRepository.All();

            foreach (var route in routes)
            {
                var coords = new List<object>();
                for (var i = 1; i <= route.RouteGeography.STNumPoints(); i++)
                {
                    coords.Add(
                        new
                            {
                                lat = route.RouteGeography.STPointN(i).Lat,
                                lng = route.RouteGeography.STPointN(i).Long,
                            });
                }

                routeList.Add(new { name = route.Name, coord = coords });
            }

            return this.Json(routeList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets all stops.
        /// </summary>
        /// <returns>
        /// Json with all stops. 
        /// </returns>
        public JsonResult GetAllStops()
        {
            var stopsList = new List<object>();
            var stops = this.stopRepository.All();
            foreach (var stop in stops)
            {
                stopsList.Add(
                    new
                        {
                            name = stop.Name,
                            lat = stop.StopGeography.EnvelopeCenter().Lat,
                            lng = stop.StopGeography.EnvelopeCenter().Long
                        });
            }

            return this.Json(stopsList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Refreshes the relations in database.
        /// </summary>
        /// <returns>
        /// Message about refresh relations action.
        /// </returns>
        public ActionResult RefreshRelationsInDatabase()
        {
            var allRoutes = this.routeRepository.All();
            var allStops = this.stopRepository.All();
            var counter = 0;

            if (allRoutes != null && allStops != null)
            {
                foreach (var route in allRoutes)
                {
                    foreach (var stop in allStops)
                    {
                        if (route.RouteGeography.STIntersects(stop.StopGeography.STBuffer(GeneralSettings.MaxRelationsBuffer)) && !route.Stops.Contains(stop))
                        {
                            route.Stops.Add(stop);
                            stop.Routes.Add(route);
                            counter++;
                        }
                    }
                }

                this.routeRepository.Save();
                this.stopRepository.Save();
            }

            return RedirectToAction("AddStop");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Connects the stops with routes.
        /// </summary>
        /// <param name="stopGeo">
        /// The stop's geo. 
        /// </param>
        /// <returns>
        /// List of viewModelsWithStops. 
        /// </returns>
        private IEnumerable<TmpAddRouteStopViewModel> ConnectStopsWithRoutes(AddStopViewModel stopGeo)
        {
            var connectList = new List<TmpAddRouteStopViewModel>();
            var routes = this.routeRepository.All();
            foreach (var route in routes)
            {
                var stop = stopGeo.StopGeography;
                var sqlGeography = Functions.MakeValidGeographyFromText(stop, 4326);
                if ((bool)route.RouteGeography.STIntersects(sqlGeography)
                    && route.RouteType == int.Parse(stopGeo.Selected))
                {
                    connectList.Add(
                        new TmpAddRouteStopViewModel
                            {
                                Route = route,
                                StopPoint = route.RouteGeography.STIntersection(sqlGeography).STPointN(1)

                                // первое пересечение с точкой. Не magick-number.
                            });
                }
            }

            return connectList;
        }

        /// <summary>
        /// Gets the simple routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <returns></returns>
        private List<SimpleRouteViewModel> GetSimpleRoutes(List<Route> routes )
        {
            return routes.Select(
                route => new SimpleRouteViewModel
                    {
                        Id = route.Id, 
                        Name = route.Name, 
                        Points = this.GetPoins(route.RouteGeography)
                    }).ToList();
        }

        /// <summary>
        /// Gets the poins.
        /// </summary>
        /// <param name="geography">The geography.</param>
        /// <returns></returns>
        private List<MapPoint> GetPoins(SqlGeography geography)
        {
            var points = new List<MapPoint>();
            for (int i = 1; i < geography.STNumPoints() + 1; i++)
            {
                points.Add(new MapPoint
                    {
                        Latitude = (double)geography.STPointN(i).EnvelopeCenter().Lat,
                        Longitude = (double)geography.STPointN(i).EnvelopeCenter().Long

                    });
            }

            return points;
        }

        #endregion
    }
}