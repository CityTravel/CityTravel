using System.Data.SqlTypes;
using CityTravel.Domain.Entities;
using Microsoft.SqlServer.Types;
using NUnit.Framework;

namespace CityTravel.Tests.Domain.Entities
{
    /// <summary>
    /// The stop tests.
    /// </summary>
    [TestFixture]
    public class BuildingTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The can_ get_ building_ geography.
        /// </summary>
        [Test]
        public void Can_Get_Building_Geography()
        {
            // Arrange
            var building = new Building
                {
                    BuildingGeography =
                        SqlGeography.STGeomFromText(new SqlChars("POLYGON((10 10, 20 10, 30 20, 10 10))"), 4326)
                };

            var anotherBuilding = new Building
                {
                    BuildingBin =
                        SqlGeography.STPolyFromText(building.BuildingGeography.STAsText(), 4326).STAsBinary().Buffer
                };

            // Assert
            Assert.AreNotEqual(null, building.BuildingGeography);
            Assert.AreNotEqual(null, anotherBuilding.BuildingGeography);
        }

        /// <summary>
        /// The can_ set_ building_ geography.
        /// </summary>
        [Test]
        public void Can_Set_Building_Geography()
        {
            // Arrange
            var building = new Building
                {
                    BuildingGeography =
                        SqlGeography.STGeomFromText(new SqlChars("POLYGON((10 10, 20 10, 30 20, 10 10))"), 4326)
                };

            // Assert
            Assert.AreNotEqual(null, building.BuildingGeography);
            Assert.AreNotEqual(null, building.BuildingBin);
        }

        #endregion
    }
}