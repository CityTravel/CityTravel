namespace CityTravel.Domain.Repository.Concrete
{
    using CityTravel.Domain.DomainModel.Abstract;
    using CityTravel.Domain.Entities.Autocomplete;

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