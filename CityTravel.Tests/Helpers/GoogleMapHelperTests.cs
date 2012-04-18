using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using CityTravel.Domain.Entities;
using CityTravel.Domain.Helpres;
using CityTravel.Domain.Settings;
using Microsoft.SqlServer.Types;
using NUnit.Framework;

namespace CityTravel.Tests.Helpers
{
    /// <summary>
    /// Google Map Helper Tests
    /// </summary>
    [TestFixture]
    public class GoogleMapHelperTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The can_ get_ direction.
        /// </summary>
        [Test]
        public void CanGetDirection()
        {
            // Arrange
            var startPoint = SqlGeography.STPointFromText(new SqlChars("POINT(48.36 34.58)"), 4326);
            var endPoint = SqlGeography.STPointFromText(new SqlChars("POINT(48.46 34.28)"), 4326);

            // Act
            var urlForDirection = GoogleMapHelper.CreateUrlForDirectionRequest(
                GeneralSettings.GoogleApiKey, startPoint, endPoint);
            var json = GoogleMapHelper.GetResponceFromGoogleApi(urlForDirection);

            // Assert
            Assert.AreNotEqual(null, json);
        }

        /// <summary>
        /// The can_ get_ distance_ of_ direction.
        /// </summary>
        [Test]
        public void CanGetDistanceOfDirection()
        {
			// Arrange
			var startPoint = SqlGeography.STPointFromText(new SqlChars("POINT(48.36 34.58)"), 4326);
			var endPoint = SqlGeography.STPointFromText(new SqlChars("POINT(48.46 34.28)"), 4326);

			// Act
            var urlForDirection = GoogleMapHelper.CreateUrlForDirectionRequest(
               GeneralSettings.GoogleApiKey, startPoint, endPoint);
			var json = GoogleMapHelper.GetResponceFromGoogleApi(urlForDirection);

            // Act
			var dist = GoogleMapHelper.GetDistanceOfDirection(json);

            // Assert
            Assert.Greater(dist, 0.0);
        }

        /// <summary>
        /// The can_ get_ steps_ of_ direction.
        /// </summary>
        [Test]
        public void CanGetStepsOfDirection()
        {
			// Arrange
			var startPoint = SqlGeography.STPointFromText(new SqlChars("POINT(48.36 34.58)"), 4326);
			var endPoint = SqlGeography.STPointFromText(new SqlChars("POINT(48.46 34.28)"), 4326);

			// Act
            var urlForDirection = GoogleMapHelper.CreateUrlForDirectionRequest(
               GeneralSettings.GoogleApiKey, startPoint, endPoint);
			var direction = GoogleMapHelper.GetResponceFromGoogleApi(urlForDirection);

            // Act
            var steps = GoogleMapHelper.GetStepsOfDirection(direction);

            // Assert
            Assert.AreNotEqual(null, steps);
            Assert.Greater(steps.Count(), 0);
        }

        /// <summary>
        /// The can_ get_ summary polyline.
        /// </summary>
        [Test]
        public void CanGetSummaryPolyline()
        {
			// Arrange
			var startPoint = SqlGeography.STPointFromText(new SqlChars("POINT(48.36 34.58)"), 4326);
			var endPoint = SqlGeography.STPointFromText(new SqlChars("POINT(48.46 34.28)"), 4326);

			// Act
            var urlForDirection = GoogleMapHelper.CreateUrlForDirectionRequest(
               GeneralSettings.GoogleApiKey, startPoint, endPoint);
			var direction = GoogleMapHelper.GetResponceFromGoogleApi(urlForDirection);

            // Act
            var line = GoogleMapHelper.GetSummaryPolyline(direction);

            // Assert
            Assert.AreNotEqual(null, line);
            Assert.Greater(line.Count(), 0);
        }

        /// <summary>
        /// The can_ get_ time_ of_ direction.
        /// </summary>
        [Test]
        public void CanGetTimeOfDirection()
        {
			// Arrange
			var startPoint = SqlGeography.STPointFromText(new SqlChars("POINT(48.36 34.58)"), 4326);
			var endPoint = SqlGeography.STPointFromText(new SqlChars("POINT(48.46 34.28)"), 4326);

			// Act
            var urlForDirection = GoogleMapHelper.CreateUrlForDirectionRequest(
               GeneralSettings.GoogleApiKey, startPoint, endPoint);
			var direction = GoogleMapHelper.GetResponceFromGoogleApi(urlForDirection);

            // Act
            var time = GoogleMapHelper.GetTimeOfDirection(direction);

            // Assert
            Assert.Greater(time, 0.0);
        }

        /// <summary>
        /// Determines whether this instance [can get decode polyline].
        /// </summary>
        [Test]
        public void CanGetDecodePolyline()
        {
            List<MapPoint> decodePolyline = GoogleMapHelper.DecodePolyline("qssfHsl|tEnBdB");
            var points = new List<MapPoint>()
                {
                    new MapPoint() { Latitude = 35.04858, Longitude = 48.43849 },
                    new MapPoint() { Latitude = 35.04807, Longitude = 48.43793 }
                };

            var result = decodePolyline != null && (decodePolyline.Except(points) == null ? false : true);

            Assert.True(result);
        }

        /// <summary>
        /// Determines whether this instance [can get steps].
        /// </summary>
        [Test]
        public void CanGetSteps()
        {
            var startPoint = new MapPoint(35.048072199999979, 48.437927).ToSqlGeography();
            var endPoint = new MapPoint(35.045630999999958, 48.46442).ToSqlGeography();
            var urlForDirection = GoogleMapHelper.CreateUrlForDirectionRequest(
               GeneralSettings.GoogleApiKey, startPoint, endPoint);
            var responce = GoogleMapHelper.GetResponceFromGoogleApi(urlForDirection);
			var stepsFromGoogle = GoogleMapHelper.GetStepsOfDirection(responce);
            
            var steps = new List<Step>
                {
                    new Step()
                        {
                            Instruction =
                                "Направляйтесь на <b>юго-восток</b> по <b>просп. Карла Маркса</b> в сторону <b>пл. В.И. Ленина</b>",
                            Length = "1,8 км",
                            Time = "21,8мин"
                        },
                    new Step()
                        {
                            Instruction = "Поверните <b>направо</b> на <b>просп. Гагарина</b>",
                            Length = "3 м",
                            Time = "0,0мин"
                        }
                };

            var result = stepsFromGoogle != null && (stepsFromGoogle.Except(steps) == null ? false : true);

            Assert.True(result);
        }

        /// <summary>
        /// Gets the summary polyline.
        /// </summary>
        [Test]
        public void GetSummaryPolyline()
        {
            var startPoint = new MapPoint(35.048072199999979, 48.437927).ToSqlGeography();
            var endPoint = new MapPoint(35.045630999999958, 48.46442).ToSqlGeography();
            var urlForDirection = GoogleMapHelper.CreateUrlForDirectionRequest(
               GeneralSettings.GoogleApiKey, startPoint, endPoint);
            var responce = GoogleMapHelper.GetResponceFromGoogleApi(urlForDirection);
            var decodePolyline = GoogleMapHelper.GetSummaryPolyline(responce);
            var points = new List<MapPoint>()
                {
                    new MapPoint() { Latitude = 35.04858, Longitude = 48.43849 },
                    new MapPoint() { Latitude = 35.04807, Longitude = 48.43793 }
                };
            var result = decodePolyline != null && (decodePolyline.Except(points) == null ? false : true);

            Assert.True(result);
        }

        #endregion
    }
}