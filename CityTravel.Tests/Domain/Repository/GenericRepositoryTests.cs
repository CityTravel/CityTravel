using System.Linq;
using CityTravel.Domain.DomainModel;
using CityTravel.Domain.Entities;
using CityTravel.Domain.Repository;
using CityTravel.Tests.Domain.DomainModel;
using NUnit.Framework;

namespace CityTravel.Tests.Domain.Repository
{
    using CityTravel.Domain.Entities.Route;

    /// <summary>
    /// The generic repository tests.
    /// </summary>
    [TestFixture]
    public class GenericRepositoryTests
    {
        #region Constants and Fields

        /// <summary>
        /// The data base context.
        /// </summary>
        private IDataBaseContext dataBaseContext;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The can_ add_ new_ entity.
        /// </summary>
        [Test]
        public void Can_Add_New_Entity()
        {
            // Arrange
            var routesRepository = new GenericRepository<Route>(this.dataBaseContext);

            // Act
            routesRepository.Add(new Route { Id = 4, Name = "newRoute" });

            // Assert
            Assert.AreEqual(2, routesRepository.Count);
            Assert.AreNotEqual(null, routesRepository.GetById(1));
        }

        /// <summary>
        /// The can_ check_ entity_ exists_ in_ repository.
        /// </summary>
        [Test]
        public void Can_Check_Entity_Exists_In_Repository()
        {
            // Arrange
            var routesRepository = new GenericRepository<Route>(this.dataBaseContext);

            // Act
            var resultNormal = routesRepository.Contains(x => x.Id == 3);
            var resultFailure = routesRepository.Contains(x => x.Id == 100);

            // Assert
            Assert.AreEqual(false, resultNormal);
            Assert.AreNotEqual(true, resultFailure);
        }

        /// <summary>
        /// The can_ filter_ entities.
        /// </summary>
        [Test]
        public void Can_Filter_Entities()
        {
            // Arrange
            var routesRepository = new GenericRepository<Route>(this.dataBaseContext);

            // Act
            var results = routesRepository.Filter(x => x.Id != 3);

            // Assert
            Assert.AreEqual(1, results.Count());
        }

        /// <summary>
        /// The can_ find_ entity_ by_ specified_ predicate.
        /// </summary>
        [Test]
        public void Can_Find_Entity_By_Specified_Predicate()
        {
            // Arrange
            var routesRepository = new GenericRepository<Route>(this.dataBaseContext);

            // Act
            var resultNormal = routesRepository.Find(x => x.Id == 1);
            var resultFailure = routesRepository.Find(x => x.Id == 100);

            // Assert
            Assert.AreNotEqual(null, resultNormal);
            Assert.AreEqual(null, resultFailure);
        }

        /// <summary>
        /// The can_ get_ entity_ by_ specified_ id.
        /// </summary>
        [Test]
        public void Can_Get_Entity_By_Specified_Id()
        {
            // Arrange
            var routesRepository = new GenericRepository<Route>(this.dataBaseContext);

            // Act
            var resultNormal = routesRepository.GetById(1);
            var resultFailure = routesRepository.GetById(100);

            // Assert
            Assert.AreNotEqual(null, resultNormal);
            Assert.AreEqual(null, resultFailure);
        }

        /// <summary>
        /// The can_ remove_ entity_ by_ it_ specifying.
        /// </summary>
        [Test]
        public void Can_Remove_Entity_By_It_Specifying()
        {
            // Arrange
            var routesRepository = new GenericRepository<Route>(this.dataBaseContext);
            var entityToDelete = routesRepository.GetById(2);

            // Act
            routesRepository.Delete(entityToDelete);

            // Assert
            Assert.AreEqual(1, routesRepository.Count);
            Assert.AreEqual(null, routesRepository.GetById(2));
        }

        /// <summary>
        /// The can_ remove_ entity_ by_ predicate.
        /// </summary>
        [Test]
        public void Can_Remove_Entity_By_Predicate()
        {
            // Arrange
            var routesRepository = new GenericRepository<Route>(this.dataBaseContext);

            // Act
            routesRepository.Delete(x => x.Id == 2);

            // Assert
            Assert.AreEqual(1, routesRepository.Count);
            Assert.AreEqual(null, routesRepository.GetById(2));
        }

        /// <summary>
        /// The can_ update_ entity.
        /// </summary>
        [Test]
        public void Can_Update_Entity()
        {
            // Arrange
            var routesRepository = new GenericRepository<Route>(this.dataBaseContext);
            var entityToUpdate = routesRepository.GetById(1);
            entityToUpdate.Name = "Updated";

            // Act
            routesRepository.Update(entityToUpdate);

            // Assert
            Assert.AreEqual(1, routesRepository.Count);
            Assert.AreEqual("Updated", routesRepository.GetById(1).Name);
        }

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.dataBaseContext = new FakeDbContext();
        }

        #endregion
    }
}