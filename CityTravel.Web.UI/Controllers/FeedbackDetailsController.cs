using System.Web.Mvc;

namespace CityTravel.Web.UI.Controllers
{
    using CityTravel.Domain.Entities.Feedback;
    using CityTravel.Domain.Repository.Abstract;

    /// <summary>
    /// Feedback details controller.
    /// </summary>
    public class FeedbackDetailsController : Controller
    {
        /// <summary>
        /// Feedback provider.
        /// </summary>
        private readonly IProvider<Feedback> feedbackProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackDetailsController"/> class.
        /// </summary>
        /// <param name="feedbackProvider">The feedback provider.</param>
        public FeedbackDetailsController(IProvider<Feedback> feedbackProvider)
        {
            this.feedbackProvider = feedbackProvider;

        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// View of feedback details.
        /// </returns>
        public ViewResult Index()
        {
            var feedbackInformation = this.feedbackProvider.All();

            return View(feedbackInformation);
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// Feedback view.
        /// </returns>
        public ActionResult Delete(int id)
        {
            this.feedbackProvider.Delete(data => data.Id == id);
            this.feedbackProvider.Save();

            return RedirectToAction("Index");
        }
    }
}
