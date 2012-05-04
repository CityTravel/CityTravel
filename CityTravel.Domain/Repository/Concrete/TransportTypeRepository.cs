namespace CityTravel.Domain.Repository.Concrete
{
    using CityTravel.Domain.DomainModel.Abstract;
    using CityTravel.Domain.Entities.Route;

    /// <summary>
    /// The transport type repository.
    /// </summary>
    public class TransportTypeRepository : GenericRepository<TransportType>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransportTypeRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public TransportTypeRepository(IDataBaseContext context)
            : base(context)
        {
        }

        #endregion
    }
}