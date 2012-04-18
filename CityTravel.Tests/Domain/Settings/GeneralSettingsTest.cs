using CityTravel.Domain.Settings;
using NUnit.Framework;

namespace CityTravel.Tests.Domain.Settings
{
    /// <summary>
    /// General settings test
    /// </summary>
    [TestFixture]
    public class GeneralSettingsTest
    {
        #region Public Methods and Operators

        /// <summary>
        /// Determines whether this instance [can get cach time].
        /// </summary>
        [Test]
        public void CanGetCachTime()
        {
            var cacheTime = GeneralSettings.CacheTime;
            Assert.NotNull(cacheTime);
            Assert.AreEqual(typeof(int), cacheTime.GetType());
        }

        /// <summary>
        /// Determines whether this instance [can get google API key].
        /// </summary>
        [Test]
        public void CanGetGoogleApiKey()
        {
            var googleApiKey = GeneralSettings.GoogleApiKey;
            Assert.NotNull(googleApiKey);
            Assert.AreEqual(typeof(string), googleApiKey.GetType());
        }

        /// <summary>
        /// Determines whether this instance [can get route radius seach].
        /// </summary>
        [Test]
        public void CanGetRouteRadiusSeach()
        {
            var routeRadius = GeneralSettings.RouteRadiusSeach;
            Assert.NotNull(routeRadius);
            Assert.AreEqual(typeof(int), routeRadius.GetType());
        }

        /// <summary>
        /// Determines whether this instance [can get max time constraint].
        /// </summary>
        [Test]
        public void CanGetMaxTimeConstraint()
        {
            var maxTimeConstraint = GeneralSettings.MaxTimeConstraint;
            Assert.NotNull(maxTimeConstraint);
            Assert.AreEqual(typeof(int), maxTimeConstraint.GetType());
        }

        /// <summary>
        /// Determines whether this instance [can get walking speed].
        /// </summary>
        [Test]
        public void CanGetWalkingSpeed()
        {
            var walkingSpeed = GeneralSettings.WalkingSpeed;
            Assert.NotNull(walkingSpeed);
            Assert.AreEqual(typeof(int), walkingSpeed.GetType());
        }

        #endregion
    }
}