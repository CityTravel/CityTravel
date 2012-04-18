using System.Data.Entity;
using CityTravel.Domain.Entities;

namespace CityTravel.Domain.DomainModel
{
    /// <summary>
    /// Data Base Context for our entities
    /// </summary>
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Buildings.
        /// </summary>
        public IDbSet<Building> Buildings { get; set; }

        /// <summary>
        /// Gets or sets Feedbacks.
        /// </summary>
        public IDbSet<Feedback> Feedbacks { get; set; }

        /// <summary>
        /// Gets or sets Languages.
        /// </summary>
        public IDbSet<Language> Languages { get; set; }

        /// <summary>
        /// Gets or sets Places.
        /// </summary>
        public IDbSet<Place> Places { get; set; }

        /// <summary>
        /// Gets or sets Routes.
        /// </summary>
        public IDbSet<Route> Routes { get; set; }

        /// <summary>
        /// Gets or sets Stops.
        /// </summary>
        public IDbSet<Stop> Stops { get; set; }

        /// <summary>
        /// Gets or sets TransportTypes.
        /// </summary>
        public IDbSet<TransportType> TransportTypes { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The set.
        /// </summary>
        /// <typeparam name="T">
        /// type of set objects
        /// </typeparam>
        /// <returns>
        /// IDbSet of T type
        /// </returns>
        public new IDbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

        #endregion
    }
}