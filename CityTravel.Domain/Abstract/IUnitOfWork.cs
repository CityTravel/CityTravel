using CityTravel.Domain.Entities;

namespace CityTravel.Domain.Abstract
{
    /// <summary>
    /// The unit of work interface
    /// </summary>
    public interface IUnitOfWork
    {
        #region Public Properties

        /// <summary>
        /// Gets BuildingRepository.
        /// </summary>
        IProvider<Building> BuildingRepository { get; }

        /// <summary>
        /// Gets FeedbackRepository.
        /// </summary>
        IProvider<Feedback> FeedbackRepository { get; }

        /// <summary>
        /// Gets LanguageRepository.
        /// </summary>
        IProvider<Language> LanguageRepository { get; }

        /// <summary>
        /// Gets PlaceRepository.
        /// </summary>
        IProvider<Place> PlaceRepository { get; }

        /// <summary>
        /// Gets RouteRepository.
        /// </summary>
        IProvider<Route> RouteRepository { get; }

        /// <summary>
        /// Gets StopRepository.
        /// </summary>
        IProvider<Stop> StopRepository { get; }

        /// <summary>
        /// Gets TransportTypeRepository.
        /// </summary>
        IProvider<TransportType> TransportTypeRepository { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The save.
        /// </summary>
        /// <returns>
        /// The affected rows count
        /// </returns>
        int Save();

        #endregion
    }
}