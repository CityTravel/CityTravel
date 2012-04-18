using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CityTravel.Domain.Entities;
using CityTravel.Tests.Domain.DomainModel;
using CityTravel.Tests.Domain.Repository;
using CityTravel.Web.UI.Controllers;
using CityTravel.Web.UI.Models;
using NUnit.Framework;

namespace CityTravel.Tests.Web.Controllers
{
    /// <summary>
    /// The add route controller tests.
    /// </summary>
    [TestFixture]
    public class AddRouteControllerTests
    {
        #region Constants and Fields

        /// <summary>
        /// The add route controller.
        /// </summary>
        private AddRouteController addRouteController;

        private FakeDbContext fakeDbContext;

        #endregion

        #region Public Methods and Operators


        /// <summary>
        /// The add route_ action_ adds_ new_ route_ when_ the_ add route view model_ model_ is_ valid.
        /// </summary>
        [Test]
        public void AddRoute_Action_Adds_New_Route_When_The_AddRouteViewModel_Model_Is_Valid()
        {
            // Arrange
            var routeViewModel = new AddRouteViewModel();
            routeViewModel.Name = "Test";
            routeViewModel.RouteGeography =
                "35.01064 48.43012, 35.01436 48.42924, 35.01436 48.42924, 35.01455 48.42919, 35.01455 48.42919, 35.0166 48.43273, 35.0166 48.43273, 35.01681 48.43277, 35.01716 48.43272, 35.02054 48.42825, 35.02341 48.4253, 35.02352 48.42512, 35.02359 48.42477, 35.02359 48.42477, 35.02364 48.42458, 35.02373 48.42451, 35.02386 48.42446, 35.02413 48.42446, 35.02456 48.42474, 35.02493 48.42488, 35.02847 48.42539, 35.0296 48.42566, 35.03847 48.42882, 35.0395 48.4292, 35.04009 48.42949, 35.04166 48.43074, 35.0424 48.43139, 35.04785 48.43735, 35.04864 48.4383, 35.04864 48.4383, 35.05028 48.4401, 35.05052 48.44046, 35.05062 48.44076, 35.05081 48.4425, 35.05101 48.44283, 35.05349 48.44474, 35.05714 48.44769, 35.05891 48.44907, 35.05946 48.44959, 35.06174 48.45147, 35.06544 48.45471, 35.06544 48.45471, 35.06223 48.45632, 35.0615 48.45664, 35.05841 48.45822, 35.05537 48.45988, 35.05286 48.46117, 35.0465 48.46457, 35.04394 48.46588, 35.03825 48.46892, 35.03825 48.46892, 35.03502 48.46616, 35.03283 48.46441, 35.03283 48.46441, 35.029 48.46538, 35.029 48.46538, 35.02885 48.46529, 35.02881 48.46522, 35.02682 48.46143, 35.02566 48.45937, 35.02078 48.45009, 35.01776 48.44452, 35.01253 48.43466, 35.01012 48.43025, 35.01012 48.43025, 35.01064 48.43012";
            routeViewModel.Selected = "2";

            // Act
            this.addRouteController.AddRoute(routeViewModel);

            // Assert
            Assert.AreEqual(2, FakeRepository<Route>.Mock(fakeDbContext.Routes).All().Count());
        }

        /// <summary>
        /// The add route_ action_ returns_ add route_ view.
        /// </summary>
        [Test]
        public void AddRoute_Action_Returns_AddRoute_View()
        {
            // Arrange
            const string ExpectedViewName = "AddRoute";

            // Act
            var result = this.addRouteController.AddRoute();

            // Assert
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(ExpectedViewName, result.ViewName, "View name should have been {0}", ExpectedViewName);
        }

        /// <summary>
        /// The add route_ action_ returns_ view result__ when_ the_ add route view model_ model_ is_ invalid.
        /// </summary>
        [Test]
        public void AddRoute_Action_Returns_ViewResult__When_The_AddRouteViewModel_Model_Is_Invalid()
        {
            // Arrange
            const string ExpectedViewName = "AddRoute";
            var routeViewModel = new AddRouteViewModel();
            routeViewModel.Name = string.Empty;
            routeViewModel.RouteGeography = string.Empty;
            this.addRouteController.ModelState.AddModelError("ModelError", "Error!");

            // Act
            var result = this.addRouteController.AddRoute(routeViewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(ExpectedViewName, result.ViewName, "View name should have been {0}", ExpectedViewName);
        }

        /// <summary>
        /// The add stop_ action_ adds_ new_ stop_ when_ the_ add stop view model_ model_ is_ valid.
        /// </summary>
        [Test]
        public void AddStop_Action_Adds_New_Stop_When_The_AddStopViewModel_Model_Is_Valid()
        {
            // Arrange
            var stopViewModel = new AddStopViewModel
                { Name = "Test4", StopGeography = "POLYGON((10 10, 40 10, 40 40, 10 40, 10 10))", Selected = "2" };

            // Act
            this.addRouteController.AddStop(stopViewModel);

            // Assert
            Assert.AreEqual(14, FakeRepository<Stop>.Mock(fakeDbContext.Stops).All().Count());
        }

        /// <summary>
        /// The add stop_ action_ returns_ add stop_ view.
        /// </summary>
        [Test]
        public void AddStop_Action_Returns_AddStop_View()
        {
            // Arrange
            const string ExpectedViewName = "AddStop";

            // Act
            var result = this.addRouteController.AddStop();

            // Assert
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(ExpectedViewName, result.ViewName, "View name should have been {0}", ExpectedViewName);
        }

        /// <summary>
        /// The add stop_ action_ returns_ view result__ when_ the_ add stop view model_ model_ is_ invalid.
        /// </summary>
        [Test]
        public void AddStop_Action_Returns_ViewResult__When_The_AddStopViewModel_Model_Is_Invalid()
        {
            // Arrange
            const string ExpectedViewName = "AddStop";
            var stopViewModel = new AddStopViewModel { Name = string.Empty, StopGeography = string.Empty };
            this.addRouteController.ModelState.AddModelError("ModelError", "Error!");

            // Act
            var result = this.addRouteController.AddStop(stopViewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(ExpectedViewName, result.ViewName, "View name should have been {0}", ExpectedViewName);
        }

        /// <summary>
        /// The can_ get_ all_ stops_ as_ json.
        /// </summary>
        [Test]
        public void Can_Get_All_Stops_As_Json()
        {
            // Act
            var jsonResult = this.addRouteController.GetAllStops();
            var jsonList = jsonResult.Data as List<object>;

            // Assert
            Assert.AreEqual(14, jsonList.Count);
        }

        /// <summary>
        /// Can_s the get_ all_ routes_ as_ json.
        /// </summary>
        [Test]
        public void Can_Get_All_Routes_As_Json()
        {
            // Act
            var jsonResult = this.addRouteController.GetAllRoutes();
            var jsonList = jsonResult.Data as List<object>;

            // Assert
            Assert.AreEqual(1, jsonList.Count);
        }

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public void SetUpTest()
        {
            this.fakeDbContext = new FakeDbContext();
            this.addRouteController = new AddRouteController(FakeRepository<Route>.Mock(fakeDbContext.Routes), FakeRepository<Stop>.Mock(fakeDbContext.Stops));
        }

        #endregion
    }
}