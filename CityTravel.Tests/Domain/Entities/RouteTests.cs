using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using CityTravel.Domain.Entities;
using CityTravel.Tests.Domain.DomainModel;
using CityTravel.Tests.Domain.Repository;
using Microsoft.SqlServer.Types;
using NUnit.Framework;

namespace CityTravel.Tests.Domain.Entities
{
    using CityTravel.Domain.Entities.Route;
    using CityTravel.Domain.Services.DimensionConverter;
    using CityTravel.Domain.Services.ModelConverter;

    /// <summary>
    /// The route tests.
    /// </summary>
    [TestFixture]
    public class RouteTests
    {
        #region Public Methods and Operators
        private FakeDbContext fakeDbContext;

        [SetUp]
        public void SetUpTests()
        {
            this.fakeDbContext = new FakeDbContext();
        }

        /// <summary>
        /// The can_ get_ route_ geography.
        /// </summary>
        [Test]
        public void Can_Get_Route_Geography()
        {
            // Arrange
            var route = new Route();
            route.RouteGeography =
                SqlGeography.STGeomFromText(
                    new SqlChars("LINESTRING(35.01064 48.43012, 35.01436 48.42924, 35.01436 48.42924)"), 4326);

            var anotherRoute = new Route();
            anotherRoute.RouteBin =
                SqlGeography.STLineFromText(route.RouteGeography.STAsText(), 4326).STAsBinary().Buffer;

            // Assert
            Assert.AreNotEqual(null, route.RouteGeography);
            Assert.AreNotEqual(null, anotherRoute.RouteGeography);
        }

		/// <summary>
		/// The can_ get_ price_ transport.
		/// </summary>
		[Test]
		public void CanGetPriceTransport()
		{
		    int price = 3;
            var result = DimensionConverter.GetTransportPrice(price);

		    Assert.AreEqual("3", result);
        }

        /// <summary>
        /// Determines whether this instance [can make valid].
        /// </summary>
        [Test]
        public void CanMakeValidRoute()
        {
            var context = new FakeDbContext();
            var routes = new List<Route>();
            var route = FakeRepository<Route>.Mock(fakeDbContext.Routes).All();
            routes.Add(route.First());
            var result = ModelConverter.Convert(routes);

            Assert.NotNull(result);
        }

        #endregion
    }
}