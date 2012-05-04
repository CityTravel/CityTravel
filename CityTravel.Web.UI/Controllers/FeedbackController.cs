using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace CityTravel.Web.UI.Controllers
{
    using CityTravel.Domain.Entities.Feedback;
    using CityTravel.Domain.Repository;
    using CityTravel.Domain.Repository.Abstract;
    using CityTravel.Web.UI.Models;

    /// <summary>
    /// The feedback controller.
    /// </summary>
    public class FeedbackController : BaseController
    {
        #region Fields
        private IProvider<Feedback> feedbackRepository;
        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackController"/> class.
        /// </summary>
        public FeedbackController(IProvider<Feedback> feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Feedbacks the form.
        /// </summary>
        /// <returns>
        /// feedback form
        /// </returns>
        public PartialViewResult FeedbackForm()
        {
            var feedBackModel = new FeedbackViewModel();
            var feedBackTypes = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Мнение", Value = "0" }, 
                    new SelectListItem { Text = "Предложение", Value = "1" }, 
                    new SelectListItem { Text = "Ошибка", Value = "2" }, 
                    new SelectListItem { Text = "Партнерство", Value = "3" }
                };

            feedBackModel.Type = feedBackTypes;

            return this.PartialView("FeedbackForm", feedBackModel);
        }

        /// <summary>
        /// Feedbacks the form.
        /// </summary>
        /// <param name="fvm">
        /// The FVM. 
        /// </param>
        /// <returns>
        /// content result
        /// </returns>
        [HttpPost]
        public ContentResult FeedbackForm(FeedbackViewModel fvm)
        {
            var vsh = new ValidateServerHelper();
            ValidationResult validationResult = vsh.FeedBackDataIsValid(fvm);
            
            if (validationResult == ValidationResult.Success)
            {
                var feedback = new Feedback
                {
                    Email = fvm.Email,
                    Name = fvm.Name ?? "Anonymous",
                    Text = fvm.Text,
                    Type = int.Parse(fvm.SelectedValue)
                };

                this.feedbackRepository.Add(feedback);
                this.feedbackRepository.Save();
                switch (feedback.Type)
                {
                    case 0:
                        this.RedirectToAction("Index", "MakeRoute");
                        return
                            this.Content(
                                "<div class=\"greetings\" oncreate style=\"text-align:center; padding:20px;\">Спасибо, ваше мнение очень важно для нас. <a class='close' data-dismiss='modal'>×</a></div>",
                            "text/html");
                        
                    case 1:
                        return
                            this.Content(
                                "<div class=\"greetings\" oncreate style=\"text-align:center; padding:20px;\">Спасибо, ваше предложение будет расмотрено. <a class='close' data-dismiss='modal'>×</a></div>",
                            "text/html");
                    case 2:
                        return
                            this.Content(
                                "<div class=\"greetings\" oncreate style=\"text-align:center; padding:20px;\">Спасибо, ошибка будет вскоре исправлена.<a class='close' data-dismiss='modal'>×</a></div>",
                            "text/html");
                    case 3:
                        return
                            this.Content(
                                "<div class=\"greetings\" oncreate style=\"text-align:center; padding:20px;\">Спасибо за проявленый интерес к нашему сайту. Мы вскоре свяжемся с вами. <a class='close' data-dismiss='modal'>×</a></div>",
                            "text/html");
                    default:
                        return
                            this.Content(
                                "<div class=\"greetings\" oncreate style=\"text-align:center; padding:20px;\" >Спасибо за ваш отзыв, он для нас очень важен. <a class='close' data-dismiss='modal'>×</a></div> ",
                            "text/html");
                }
            }
            else 
                return
                    this.Content(
                        "<div class=\"greetings\" oncreate style=\"text-align:center\">" + validationResult.ErrorMessage
                        + "</div>",
                        "text/html");
            }

        #endregion
    }
}