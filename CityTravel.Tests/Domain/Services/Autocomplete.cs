using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CityTravel.Domain.Services.Autocomplete;
using CityTravel.Tests.Domain.DomainModel;
using CityTravel.Tests.Domain.UnitOfWork;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CityTravel.Domain.Abstract;

namespace CityTravel.Tests.Domain.Services
{
  /*  [TestFixture]
    public class AutocompleteTest
    {
        private Autocomplete autocomplete;
        private IUnitOfWork unitOfWork;
        [SetUp]
        public void SetUp()
        {
            autocomplete = new Autocomplete(new FakeUnitOfWork(new FakeDbContext()));
            unitOfWork = new FakeUnitOfWork(new FakeDbContext());
        }

        [Test]
        public void AutocompleteServiceTest()
        {
            object answer = autocomplete.GetAdressFromDatabase("проспект ");
            string jsonString = JsonConvert.SerializeObject(answer);
            JObject resp = JObject.Parse(jsonString);
            JToken descriptions = resp["predictions"];
            foreach (var item in descriptions)
            {
                Assert.AreEqual((string)item["description"], "проспект Гагарина");
            }
        }

        [Test]
        public void AutocompleteServiceTestBuildingNumber()
        {
            object answer = autocomplete.GetAdressFromDatabase("55");
            string jsonString = JsonConvert.SerializeObject(answer);
            JObject resp = JObject.Parse(jsonString);
            JToken descriptions = resp["predictions"];
            foreach (var item in descriptions)
            {
                Assert.AreEqual((string)item["description"], "улица Барикадная 55");
            }
        }

        [Test]
        public void AutocompleteServiceTestNotInDb()
        {
            object answer = autocomplete.GetAdressFromDatabase("DoNotIntDB");
            Assert.IsNull(answer);
        }

        [Test]
        public void AutocompleteServiceTestBuildingNumberAndStreet()
        {
            object answer = autocomplete.GetAdressFromDatabase("Барикадная 55");
            string jsonString = JsonConvert.SerializeObject(answer);
            JObject resp = JObject.Parse(jsonString);
            JToken descriptions = resp["predictions"];
            foreach (var item in descriptions)
            {
                Assert.AreEqual((string)item["description"], "улица Барикадная 55");
            }
        }

        [Test]
        public void AutocompleteServiceTestAddPlace()
        {
            autocomplete.AddSuggestionsToDatabase(new List<string> { "улица Дзержинского, 33б" });
            Assert.AreEqual(3, unitOfWork.BuildingRepository.Count);
            Assert.AreEqual(3, unitOfWork.PlaceRepository.Count);
        }
    }*/
}
