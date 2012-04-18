using System.Collections.Generic;

namespace CityTravel.Domain.Services.Autocomplete
{
    /// <summary>
    /// The i autocomplete.
    /// </summary>
    public interface IAutocomplete
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add suggestions to database.
        /// </summary>
        /// <param name="suggestions">
        /// The suggestions.
        /// </param>
        void AddSuggestionsToDatabase(List<string> suggestions, string inputAdress = null);

        /// <summary>
        /// The get adress from database.
        /// </summary>
        /// <param name="inputAdress">
        /// The input_adress.
        /// </param>
        /// <returns>
        /// get adress from database.
        /// </returns>
        object GetAdressFromDatabase(string inputAdress);

        #endregion
    }
}