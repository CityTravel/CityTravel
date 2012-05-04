using System.Collections.Generic;
using System.Linq;
using CityTravel.Domain.Entities;
using CityTravel.Domain.Services.Segment;
using CityTravel.Tests.Domain.DomainModel;
using CityTravel.Tests.Domain.Repository;
using Microsoft.SqlServer.Types;
using NUnit.Framework;

namespace CityTravel.Tests.Domain.Services
{
    using CityTravel.Domain.Entities.Route;

    /// <summary>
    /// Route seach test
    /// </summary>
    [TestFixture]
    public class RouteSeachTest
    {
        /// <summary>
        /// Route seach class
        /// </summary>
        private RouteSeach route;
        
        /// <summary>
        /// Start Marker
        /// </summary>
        private SqlGeography startPoint;

        /// <summary>
        /// End Marker
        /// </summary>
        private SqlGeography endPoint;

        private FakeDbContext fakeDbContext;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.fakeDbContext = new FakeDbContext();
            this.startPoint = new MapPoint(35.048072199999979, 48.437927).ToSqlGeography();
            this.endPoint = new MapPoint(35.045630999999958, 48.46442).ToSqlGeography();
            this.route = new RouteSeach();
        }

        /// <summary>
        /// Determines whether this instance [can get appropriate routes].
        /// </summary>
        [Test]
        public void CanGetAppropriateRoutes()
        {
            var trasnportType = new List<Transport> { Transport.All };
            var appropriariateRoutes = this.route.GetAppropriateRoutes(FakeRepository<Route>.Mock(new FakeDbContext().Routes).All(),
                this.startPoint, this.endPoint, trasnportType);
            var routes = FakeRepository<Route>.Mock(fakeDbContext.Routes).All();

            Assert.True((bool)routes.First().RouteGeography.STEquals(appropriariateRoutes[1].RouteGeography));
        }

        /// <summary>
        /// Determines whether this instance [can get concrate type of transport].
        /// </summary>
        [Test]
        public void CanGetConcrateTypeOfTransport()
        {
            var transportType = new List<Transport> { Transport.Bus };
            var appropriariateRoutes =
                this.route.GetAppropriateRoutes(
                    FakeRepository<Route>.Mock(new FakeDbContext().Routes).All(),
                    this.startPoint,
                    this.endPoint,
                    transportType);
            var routes = FakeRepository<Route>.Mock(this.fakeDbContext.Routes).All();

            Assert.AreEqual(1, appropriariateRoutes.First().RouteType.Value);
        }
    }
}
