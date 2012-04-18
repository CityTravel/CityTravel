using System.Threading;
using CityTravel.Web.UI.Helpers;
using NUnit.Framework;

namespace CityTravel.Tests.Web.Helpers
{
    /// <summary>
    /// The culture helper tests.
    /// </summary>
    [TestFixture]
    public class CultureHelperTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The can convert to default culture test.
        /// </summary>
        [Test]
        public void CanConvertToDefaultCultureTest()
        {
            string culture = CultureHelper.GetImplementedCulture("hhhhhh");
            Assert.AreEqual(culture, "ru-RU");
        }

        /// <summary>
        /// The can find close culture.
        /// </summary>
        [Test]
        public void CanFindCloseCulture()
        {
            string culture = CultureHelper.GetImplementedCulture("ru-UA");
            Assert.AreEqual(culture, "ru-RU");
        }

        /// <summary>
        /// The can get culture null test.
        /// </summary>
        [Test]
        public void CanGetCultureNullTest()
        {
            string culture = CultureHelper.GetImplementedCulture(null);
            Assert.AreEqual(culture, "ru-RU");
        }

        /// <summary>
        /// The can get current culture test.
        /// </summary>
        [Test]
        public void CanGetCurrentCultureTest()
        {
            string curCulture = Thread.CurrentThread.CurrentCulture.Name;
            string culture = CultureHelper.CurrentCulture;
            Assert.AreEqual(culture, curCulture);
        }

        /// <summary>
        /// The can get implemented culture.
        /// </summary>
        [Test]
        public void CanGetImplementedCulture()
        {
            string culture = CultureHelper.GetImplementedCulture("ru-RU");
            Assert.AreEqual(culture, "ru-RU");
        }

        #endregion
    }
}