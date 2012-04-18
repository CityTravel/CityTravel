using CityTravel.Domain.DomainModel;
using CityTravel.Domain.Entities;

namespace CityTravel.Domain.Repository
{
    /// <summary>
    /// The building repository.
    /// </summary>
    public class BuildingRepository : GenericRepository<Building>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public BuildingRepository(IDataBaseContext context)
            : base(context)
        {
        }

        #endregion
    }
}