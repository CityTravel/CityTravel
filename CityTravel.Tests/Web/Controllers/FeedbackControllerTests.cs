using System.Linq;
using CityTravel.Domain.Entities;
using CityTravel.Tests.Domain.DomainModel;
using CityTravel.Tests.Domain.Repository;
using CityTravel.Web.UI.Controllers;
using CityTravel.Web.UI.Models;
using NUnit.Framework;

namespace CityTravel.Tests.Web.Controllers
{
    /// <summary>
    /// The feedback controller tests.
    /// </summary>
    [TestFixture]
    public class FeedbackControllerTests
    {
        #region Constants and Fields

        /// <summary>
        /// The feedback controller.
        /// </summary>
        private FeedbackController feedbackController;
        private FakeDbContext fakeDbContext;
        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The feedback action returns feedback view.
        /// </summary>
        [Test]
        public void Should_Return_Feedback_List()
        {
            // Arrange
            const string ExpectedViewName = "Feedback";

            // Act
            var result = this.feedbackController.Feedback();

            // Assert
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(ExpectedViewName, result.ViewName, "View name should have been {0}", ExpectedViewName);
        }

        /// <summary>
        /// The feedback form_ action_ returns_ feedback form_ partial view.
        /// </summary>
        [Test]
        public void FeedbackForm_Action_Returns_FeedbackForm_PartialView()
        {
            // Arrange
            const string ExpectedViewName = "FeedbackForm";

            // Act
            var result = this.feedbackController.FeedbackForm();

            // Assert
            Assert.IsNotNull(result, "Should have returned a PartialViewResult");
            Assert.AreEqual(
                ExpectedViewName, result.ViewName, "PartialView name should have been {0}", ExpectedViewName);
        }

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.fakeDbContext = new FakeDbContext();
            this.feedbackController = new FeedbackController(FakeRepository<Feedback>.Mock(fakeDbContext.Feedbacks));
        }

        /// <summary>
        /// The should_ return_ content_ when_ feedback_ submitted.
        /// </summary>
        [Test]
        public void Should_Return_ContentFirst_When_Feedback_Submitted()
        {
            // Arrange
            var vm = new FeedbackViewModel
                { Email = "Shtpavel@gmail.com", SelectedValue = "1", Name = "PO", Text = "All good" };
            var zero = new FeedbackViewModel
                { Email = "Shtpavel@gmail.com", SelectedValue = "0", Name = "PO", Text = "All good" };
            var second = new FeedbackViewModel
                { Email = "Shtpavel@gmail.com", SelectedValue = "2", Name = "PO", Text = "All good" };
            var third = new FeedbackViewModel
                { Email = "Shtpavel@gmail.com", SelectedValue = "3", Name = "PO", Text = "All good" };
            var fouth = new FeedbackViewModel { Email = "Shtpavel@gmail.com", SelectedValue = "4", Name = "PO", Text = "All good" };
           
            // Act
            var result = this.feedbackController.FeedbackForm(vm);
            var resultZero = this.feedbackController.FeedbackForm(zero);
            var resultSecond = this.feedbackController.FeedbackForm(second);
            var resultThird = this.feedbackController.FeedbackForm(third);
            var resultFouth = this.feedbackController.FeedbackForm(fouth);

            int count = FakeRepository<Feedback>.Mock(fakeDbContext.Feedbacks).All().Count();

            // Assert
            Assert.IsNotNull(result, "Should have returned a ContentResult");
            Assert.AreEqual(8, count);
        }
        #endregion
    }
}