using System.Data.SqlTypes;
using CityTravel.Domain.Entities;
using NUnit.Framework;

namespace CityTravel.Tests.Domain.Entities
{
    /// <summary>
    /// The map point tests.
    /// </summary>
    [TestFixture]
    public class MapPointTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The can_ convert_ to_ sql geography.
        /// </summary>
        [Test]
        public void Can_Convert_To_SqlGeography()
        {
            // Arrange
            var point = new MapPoint(35.04, 48.32);

            // Act
            var sqlGeoValue = point.ToSqlGeography();

            // Assert
            Assert.AreEqual((SqlDouble)48.32, sqlGeoValue.Lat);
        }

        /// <summary>
        /// The can_ convert_ to_ string.
        /// </summary>
        [Test]
        public void Can_Convert_To_String()
        {
            // Arrange
            var point = new MapPoint(35.04, 48.32);

            // Act
            var stringValue = point.ToString();

            // Assert
            Assert.AreEqual("35.04 48.32", stringValue);
        }

        /// <summary>
        /// The can_ create_ new_ point.
        /// </summary>
        [Test]
        public void Can_Create_New_Point()
        {
            // Arrange
            var point = new MapPoint();
            var anotherPoint = new MapPoint(35.04, 48.32);

            // Assert
            Assert.AreNotEqual(point.Latitude, 10.0);
            Assert.AreEqual(48.32, anotherPoint.Longitude);
        }

        #endregion
    }
}