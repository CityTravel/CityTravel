using CityTravel.Domain.DomainModel;
using CityTravel.Domain.Entities;

namespace CityTravel.Domain.Repository
{
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