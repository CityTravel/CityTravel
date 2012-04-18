namespace CityTravel.Tests.Domain.Services
{
    // <summary>
    /// Test the tracke service
    /// </summary>
    /*  [TestFixture]
    public class TrackTest
    {
        private Track route;
        private FakeUnitOfWork fakeUnitOfWork;
        private SqlGeography startPoint;
        private SqlGeography endPoint;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            fakeUnitOfWork = new FakeUnitOfWork(new FakeDbContext());
            startPoint = new MapPoint(35.048072199999979, 48.437927).ToSqlGeography();
            endPoint = new MapPoint(35.045630999999958, 48.46442).ToSqlGeography();
            route = new Track(fakeUnitOfWork);

        }
        

        /// <summary>
        /// Gets the type of the appropriate routes by transport.
        /// </summary>
        [Test]
        public void GetAppropriateRoutesByTransportType()
        {
            var trasnportType = new List<int> {2};
            var appropriariateRoutes = route.GetAppropriateRoutes(trasnportType, startPoint, endPoint);
            Assert.AreEqual(trasnportType[0], appropriariateRoutes[0].RouteType);
            Assert.AreEqual(trasnportType[0], appropriariateRoutes[0].Stops[0].StopType);
        }




    }*/
}