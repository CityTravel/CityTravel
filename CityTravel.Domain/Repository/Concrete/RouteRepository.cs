namespace CityTravel.Domain.Repository.Concrete
{
    using CityTravel.Domain.DomainModel.Abstract;
    using CityTravel.Domain.Entities.Route;

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