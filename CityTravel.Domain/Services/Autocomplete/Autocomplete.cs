using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CityTravel.Domain.Abstract;
using CityTravel.Domain.Entities;

namespace CityTravel.Domain.Services.Autocomplete
{
    /// <summary>
    /// The autocomplete.
    /// </summary>
    public class Autocomplete : IAutocomplete
    {
        #region ReposFields

        private IProvider<Place> placeRepository;
        private IProvider<Building> buildingRepository;

        #endregion

        #region Constants and Fields

        /// <summary>
        ///   Lock object
        /// </summary>
        private static readonly object obj;

        /// <summary>
        ///   The patterns.
        /// </summary>
        private readonly string[] patterns = { @"\d{1,3}[a-zа-яА-ЯA-Z]", @"\d{1,3}", @"\d{1,3}" };

        /// <summary>
        ///   The stop words.
        /// </summary>
        private readonly string[] stopWords = { "просп.", "проспект", "просп", "ул.", "улица", "у.", "переулок", "тупик", "пер.", "туп." };

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="Autocomplete"/> class.
        /// </summary>
        static Autocomplete()
        {
            obj = new object();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Autocomplete"/> class.
        /// </summary>
        public Autocomplete(IProvider<Place> placeRepository, IProvider<Building> buildingRepository)
        {
            this.placeRepository = placeRepository;
            this.buildingRepository = buildingRepository;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets Obj.
        /// </summary>
        private static object Obj
        {
            get
            {
                return obj;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds the suggestions to database.
        /// </summary>
        /// <param name="suggestions">
        /// The suggestions. 
        /// </param>
       virtual public void AddSuggestionsToDatabase(List<string> suggestions, string inputAdress = null)
        {
            foreach (var suggestion in suggestions)
            {
                lock (Obj)
                {
                    var sug = suggestion.Trim();
                    var sugWords = sug.Split().ToList();
                    sugWords.ForEach(i => i = i.Trim());
                    var house = string.Empty;
                    foreach (var word in sugWords.Where(word => this.patterns.Any(pattern => Regex.IsMatch(word, pattern))))
                    {
                        house = word;
                    }

                    if (house != string.Empty)
                    {
                        sug = sug.Replace(house, string.Empty).Trim();
                    }

                    if (this.placeRepository.Contains(place => place.Name == sug) == false)
                    {
                        this.placeRepository.Add(new Place { Name = sug, LangId = 1, });
                        this.placeRepository.Save();
                    }

                    if (house != string.Empty)
                    {
                        var place = this.placeRepository.Find(pl => pl.Name == sug);
                        this.buildingRepository.Add(new Building { Number = house, Place = place });
                        this.buildingRepository.Save();
                    }
                }
            }
        }


        /// <summary>
        /// Gets the adress from database.
        /// </summary>
        /// <param name="inputAdress">
        /// The input_adress. 
        /// </param>
        /// <returns>
        /// The get adress from database. 
        /// </returns>
        virtual public object GetAdressFromDatabase(string inputAdress)
        {
            var autoViewModel = new AutocompleteViewModel();
            var adress = inputAdress;
            var house = string.Empty;
            var places = new List<Place>();

            if (string.IsNullOrEmpty(adress.Trim()))
            {
                return 0;
            }

            var acceptebleWords = (from w in adress.Split().ToList() where !(from c in this.stopWords select c).Contains(w) select w).ToList<string>();
            foreach (var word in acceptebleWords.Where(word => this.patterns.Any(pattern => Regex.IsMatch(word, pattern))))
            {
                house = word;
            }

            acceptebleWords.RemoveAll(str => str == house);

            var placesToFindStreetFromBeginingOfTheWord = new List<Place>();
            foreach (var w in acceptebleWords)
            {
                var filtered = this.placeRepository.Filter(e => e.Name.Contains(w)).ToList();
                if (filtered != null && filtered.Count > 0)
                {
                    placesToFindStreetFromBeginingOfTheWord.AddRange(filtered);
                }
            }

            foreach (var place in placesToFindStreetFromBeginingOfTheWord)
            {
                var placesFromDb = place.Name.Split().ToList();
                foreach (var word in acceptebleWords)
                {
                    placesFromDb.ForEach(
                        str =>
                            {
                                if (str.IndexOf(word, StringComparison.CurrentCultureIgnoreCase) == 0)
                                {
                                    places.Add(place);
                                }
                            });
                }
            }

            places.AddRange(this.placeRepository.Filter(e => e.Name.Contains(adress)).ToList());

            if (house != string.Empty)
            {
                foreach (var name in places)
                {
                    Building build = null;
                    build = this.buildingRepository.Find(b => b.PlaceId == name.Id && b.Number == house);
                    if (build == null)
                    {
                        this.buildingRepository.Add(new Building { Number = house, Place = name });
                        this.buildingRepository.Save();
                    }

                    autoViewModel.Predictions.Add(new { description = name.Name + " " + house });
                }

                this.buildingRepository.Filter(building => building.Number == house).ToList().ForEach(
                    b =>
                        {
                            if (b.PlaceId != null)
                            {
                                places.Add(this.placeRepository.GetById((int)b.PlaceId));
                            }
                        });
                places.ForEach(place => autoViewModel.Predictions.Add(new { description = place.Name + " " + house }));
                autoViewModel.Predictions = autoViewModel.Predictions.Count >= 4
                                                ? autoViewModel.Predictions.GetRange(0, 4)
                                                : autoViewModel.Predictions;
                autoViewModel.Predictions = autoViewModel.Predictions.Distinct().ToList();
                return autoViewModel;
            }

            if (places.Count != 0)
            {
                places = places.Distinct().ToList();
                places.ForEach(place => autoViewModel.Predictions.Add(new { description = place.Name }));
            }
            else
            {
                autoViewModel = null;
            }

            if (autoViewModel != null)
            {
                autoViewModel.Predictions = autoViewModel.Predictions.Count >= 4
                                                ? autoViewModel.Predictions.GetRange(0, 4)
                                                : autoViewModel.Predictions;
            }

            return autoViewModel;
        }

        #endregion
    }
}