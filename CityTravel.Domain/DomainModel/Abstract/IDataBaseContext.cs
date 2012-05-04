namespace CityTravel.Domain.DomainModel.Abstract
{
    using System.Data.Entity;
    using CityTravel.Domain.Entities;
    using CityTravel.Domain.Entities.Autocomplete;
    using CityTravel.Domain.Entities.Feedback;
    using CityTravel.Domain.Entities.Route;

    /// <summary>
    /// The i data base context.
    /// </summary>
    public interface IDataBaseContext
    {
        #region Public Properties

        /// <summary>
        /// Gets Buildings.
        /// </summary>
        IDbSet<Building> Buildings { get; }

        /// <summary>
        /// Gets Feedbacks.
        /// </summary>
        IDbSet<Feedback> Feedbacks { get; }

        /// <summary>
        /// Gets Languages.
        /// </summary>
        IDbSet<Language> Languages { get; }

        /// <summary>
        /// Gets Places.
        /// </summary>
        IDbSet<Place> Places { get; }

        /// <summary>
        /// Gets Routes.
        /// </summary>
        IDbSet<Route> Routes { get; }

        /// <summary>
        /// Gets Stops.
        /// </summary>
        IDbSet<Stop> Stops { get; }

        /// <summary>
        /// Gets TransportTypes.
        /// </summary>
        IDbSet<TransportType> TransportTypes { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <returns>
        /// The affected rows count
        /// </returns>
        int SaveChanges();

        /// <summary>
        /// The set.
        /// </summary>
        /// <typeparam name="T">
        /// type of set objects
        /// </typeparam>
        /// <returns>
        /// IDbSet of T type
        /// </returns>
        IDbSet<T> Set<T>() where T : BaseEntity;

        #endregion
    }
}