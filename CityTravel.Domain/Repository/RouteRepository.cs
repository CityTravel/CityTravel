using CityTravel.Domain.DomainModel;
using CityTravel.Domain.Entities;

namespace CityTravel.Domain.Repository
{
    /// <summary>
    /// The route repository.
    /// </summary>
    public class RouteRepository : GenericRepository<Route>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public RouteRepository(IDataBaseContext context)
            : base(context)
        {
        }

        #endregion
    }
}