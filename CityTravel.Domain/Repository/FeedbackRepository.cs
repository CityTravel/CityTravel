using CityTravel.Domain.DomainModel;
using CityTravel.Domain.Entities;

namespace CityTravel.Domain.Repository
{
    /// <summary>
    /// The feedback repository.
    /// </summary>
    public class FeedbackRepository : GenericRepository<Feedback>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public FeedbackRepository(IDataBaseContext context)
            : base(context)
        {
        }

        #endregion
    }
}