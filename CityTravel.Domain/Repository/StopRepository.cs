using CityTravel.Domain.DomainModel;
using CityTravel.Domain.Entities;

namespace CityTravel.Domain.Repository
{
    /// <summary>
    /// The stop repository.
    /// </summary>
    public class StopRepository : GenericRepository<Stop>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StopRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public StopRepository(IDataBaseContext context)
            : base(context)
        {
        }

        #endregion
    }
}