using CityTravel.Domain.DomainModel;
using CityTravel.Domain.Entities;

namespace CityTravel.Domain.Repository
{
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