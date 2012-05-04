namespace CityTravel.Domain.Repository.Concrete
{
    using CityTravel.Domain.DomainModel.Abstract;
    using CityTravel.Domain.Entities.Autocomplete;

    /// <summary>
    /// The language repository.
    /// </summary>
    public class LanguageRepository : GenericRepository<Language>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public LanguageRepository(IDataBaseContext context)
            : base(context)
        {
        }

        #endregion
    }
}