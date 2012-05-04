using System.Data.SqlTypes;
using CityTravel.Domain.Entities;
using Microsoft.SqlServer.Types;
using NUnit.Framework;

namespace CityTravel.Tests.Domain.Entities
{
    using CityTravel.Domain.Entities.Route;

    /// <summary>
    /// The stop tests.
    /// </summary>
    [TestFixture]
    public class StopTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The can_ get_ stop_ geography.
        /// </summary>
        [Test]
        public void Can_Get_Stop_Geography()
        {
            // Arrange
            var stop = new Stop();
            stop.StopGeography = SqlGeography.STGeomFromText(
                new SqlChars("POLYGON((10 10, 20 10, 30 20, 10 10))"), 4326);

            var anotherStop = new Stop();
            anotherStop.StopBin = SqlGeography.STPolyFromText(stop.StopGeography.STAsText(), 4326).STAsBinary().Buffer;

            // Assert
            Assert.AreNotEqual(null, stop.StopGeography);
            Assert.AreNotEqual(null, anotherStop.StopGeography);
        }

        /// <summary>
        /// The can_ set_ stop_ geography.
        /// </summary>
        [Test]
        public void Can_Set_Stop_Geography()
        {
            // Arrange
            var stop = new Stop();
            stop.StopGeography = SqlGeography.STGeomFromText(
                new SqlChars("POLYGON((10 10, 20 10, 30 20, 10 10))"), 4326);

            // Assert
            Assert.AreNotEqual(null, stop.StopGeography);
            Assert.AreNotEqual(null, stop.StopBin);
        }

        #endregion
    }
}