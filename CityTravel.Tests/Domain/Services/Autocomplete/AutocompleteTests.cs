using System.Linq;
using CityTravel.Domain.Entities;
using CityTravel.Domain.Services.Autocomplete;
using CityTravel.Tests.Domain.DomainModel;
using CityTravel.Tests.Domain.Repository;
using NUnit.Framework;
using DomainAutocomplete = CityTravel.Domain.Services.Autocomplete;

namespace CityTravel.Tests.Domain.Services.Autocomplete
{
    /// <summary>
    /// Autocomplete tests
    /// </summary>
    [TestFixture]
    public class AutocompleteTests
    {
        /// <summary>
        /// The unit of work.
        /// </summary>
        private FakeDbContext fakeDbContext;
        /// <summary>
        /// The autocomplete
        /// </summary>
        private IAutocomplete autocomplete;

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.fakeDbContext = new FakeDbContext();
			this.autocomplete = new DomainAutocomplete.Autocomplete(FakeRepository<Place>.Mock(fakeDbContext.Places), FakeRepository<Building>.Mock(fakeDbContext.Buildings));
        }

        /// <summary>
        /// Test can create Autocomplete instance
        /// </summary>
        [Test]
        public void Can_Create_Autocomplete_Instance()
        {
            // Arrange
			var auto = new DomainAutocomplete.Autocomplete(FakeRepository<Place>.Mock(fakeDbContext.Places), FakeRepository<Building>.Mock(fakeDbContext.Buildings));

            // Assert
            Assert.AreNotEqual(null, auto);
        }

        /// <summary>
        /// Test can add suggestions to database
        /// </summary>
        [Test]
        public void Can_Add_Suggestions_To_Database()
        {
            // Arrange
            var placesCount = FakeRepository<Place>.Mock(fakeDbContext.Places).Count;
            var suggestions = new[] { "Кирова", "Карла Маркса", "Короленко" }.ToList();

            // Act
            this.autocomplete.AddSuggestionsToDatabase(suggestions);

            // Assert
            Assert.AreEqual(placesCount + 3, FakeRepository<Place>.Mock(fakeDbContext.Places).Count);

            // Arrange
            var buildingsCount = FakeRepository<Building>.Mock(fakeDbContext.Buildings).Count;
            var buildings = new[] { "Кирова 54", "Карла Маркса 12", "Короленко 12" }.ToList();

            // Act
            this.autocomplete.AddSuggestionsToDatabase(buildings);

            // Assert
            Assert.AreEqual(buildingsCount + 3, FakeRepository<Building>.Mock(fakeDbContext.Buildings).Count);
        }

        /// <summary>
        /// Test can get address from database
        /// </summary>
        [Test]
        public void Can_Get_Address_From_Database()
        {
            // Arrange
            const string ExistAddress = "klari";

            // Act
            var result = this.autocomplete.GetAdressFromDatabase(ExistAddress) as AutocompleteViewModel;

            // Assert
            Assert.AreNotEqual(null, result);
            Assert.AreEqual(result.Predictions.Count, 1);

            // Arrange
            const string ExistBuilding = "artema 60b";

            // Act
            var resultBuilding = this.autocomplete.GetAdressFromDatabase(ExistBuilding) as AutocompleteViewModel;

            // Assert
            Assert.AreNotEqual(null, resultBuilding);
            Assert.AreEqual(resultBuilding.Predictions.Count, 1);
        }
    }
}
