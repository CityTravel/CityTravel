using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;
using NUnit.Framework;

namespace CityTravel.Tests.Domain.Entities
{
    using CityTravel.Domain.Entities.Autocomplete;

    /// <summary>
    /// The stop tests.
    /// </summary>
    [TestFixture]
    public class PlaceTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The can_ get_ place_ geography.
        /// </summary>
        [Test]
        public void Can_Get_Place_Geography()
        {
            // Arrange
            var place = new Place
                {
                    PlaceGeography =
                        SqlGeography.STGeomFromText(new SqlChars("POINT(10 10)"), 4326)
                };

            var anotherPlace = new Place
                { PlaceBin = SqlGeography.STPointFromText(place.PlaceGeography.STAsText(), 4326).STAsBinary().Buffer };

            // Assert
            Assert.AreNotEqual(null, place.PlaceGeography);
            Assert.AreNotEqual(null, anotherPlace.PlaceGeography);
        }

        /// <summary>
        /// The can_ set_ place_ geography.
        /// </summary>
        [Test]
        public void Can_Set_Place_Geography()
        {
            // Arrange
            var place = new Place
            {
                PlaceGeography =
                    SqlGeography.STGeomFromText(new SqlChars("POINT(10 10)"), 4326)
            };

            // Assert
            Assert.AreNotEqual(null, place.PlaceGeography);
            Assert.AreNotEqual(null, place.PlaceBin);
        }

        #endregion
    }
}