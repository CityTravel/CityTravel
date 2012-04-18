using System.Collections.Generic;
using CityTravel.Domain.Services.Autocomplete;

namespace CityTravel.Tests.Domain.Services.Autocomplete
{
    /// <summary>
    /// Fake Autocomplete
    /// </summary>
    public class FakeAutocomplete : IAutocomplete
    {
        #region Constants and Fields

        /// <summary>
        /// The data.
        /// </summary>
        private readonly List<string> data;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeAutocomplete"/> class.
        /// </summary>
        public FakeAutocomplete()
        {
            this.data = new List<string>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add suggestions to database.
        /// </summary>
        /// <param name="suggestions">
        /// The suggestions.
        /// </param>
        /// <param name="inputAdress">
        /// The input Adress.
        /// </param>
        public void AddSuggestionsToDatabase(List<string> suggestions, string inputAdress = null)
        {
            this.data.AddRange(suggestions);
        }

        /// <summary>
        /// The get adress from database.
        /// </summary>
        /// <param name="inputAdress">
        /// The input adress.
        /// </param>
        /// <returns>
        /// adress from database.
        /// </returns>
        public object GetAdressFromDatabase(string inputAdress)
        {
            return this.data.Find(x => x == inputAdress);
        }

        #endregion
    }
}