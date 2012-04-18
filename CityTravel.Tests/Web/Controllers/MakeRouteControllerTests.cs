namespace CityTravel.Tests.Web.Controllers
{
	////TODO: ВОСТАНОВИТЬ ТЕСТЫ!!!

    /*  [TestFixture]
    public class MakeRouteControllerTests
    {
        private IUnitOfWork unitOfWork;
        private MakeRouteController makeRouteController;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = new FakeUnitOfWork(new FakeDbContext());
            makeRouteController = new MakeRouteController(unitOfWork);
        }

        [Test]
        public void Default_Action_Returns_Index_View()
        {
            // Arrange
            const string expectedViewName = "Index";

            // Act
            var result = makeRouteController.Index();

            // Assert
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);
        }

        [Test]
        public void Index_Action_Returns_Valid_Json()
        {
            // Arrange
            var makeRouteViewModel = new MakeRouteViewModel();
            makeRouteViewModel.StartPointLatitude = "35,01064";
            makeRouteViewModel.StartPointLongitude = "48,43012";
            makeRouteViewModel.EndPointLatitude = "35,01436";
            makeRouteViewModel.EndPointLongitude = "48,42924";

            // Act
            //var result = makeRouteController.Index(makeRouteViewModel) as JsonResult;

            // Assert
            //Assert.IsNotNull(result, "Should have returned a route data");
        }

        [Test]
        public void AboutUs_Action_Returns_AboutUs_View()
        {
            // Arrange
            const string expectedViewName = "AboutUs";

            // Act
            var result = makeRouteController.AboutUs();

            // Assert
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);
        }
    }*/
}