using CityTravel.Domain.DomainModel;
using CityTravel.Domain.Entities;

namespace CityTravel.Domain.Repository
{
    /// <summary>
    /// The place repository.
    /// </summary>
    public class PlaceRepository : GenericRepository<Place>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public PlaceRepository(IDataBaseContext context)
            : base(context)
        {
        }

        #endregion
    }
}