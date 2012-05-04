using System;
using System.Linq;
using System.Web.Mvc;

namespace CityTravel.Web.UI.Controllers
{
    using CityTravel.Domain.Entities.Feedback;
    using CityTravel.Domain.Repository;
    using CityTravel.Domain.Repository.Abstract;
    using CityTravel.Web.UI.Models;

    using Trirand.Web.Mvc;

    /// <summary>
    /// Feedback controller.
    /// </summary>
    [Authorize]
    public class FeedbackListController : Controller
    {
        /// <summary>
        /// Feedback repository.
        /// </summary>
        private readonly IProvider<Feedback> feedBackProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackListController"/> class.
        /// </summary>
        /// <param name="feedBackProvider">The feed back provider.</param>
        public FeedbackListController(IProvider<Feedback> feedBackProvider)
        {
            this.feedBackProvider = feedBackProvider;
        }

        /// <summary>
        /// Performances the linq search.
        /// </summary>
        /// <returns>
        /// Grid view model.
        /// </returns>
        public ActionResult Index()
        {
            var gridModel = new FeedbackGridViewModel();
            var ordersGrid = gridModel.OrdersGrid;
            ordersGrid.DataUrl = Url.Action("DataRequest");
            this.SetUpGrid(ordersGrid);
            
            return View(gridModel);
        }

        /// <summary>
        /// Searches the grid data requested.
        /// </summary>
        /// <returns>
        /// Data from database.
        /// </returns>
        public JsonResult DataRequest()
        {
            var gridModel = new FeedbackGridViewModel();
            var feedBackEntites = this.feedBackProvider.All().AsQueryable();
            this.SetUpGrid(gridModel.OrdersGrid);
            JQGridState gridState = gridModel.OrdersGrid.GetState();
            Session["gridState"] = gridState;

            return gridModel.OrdersGrid.DataBind(feedBackEntites);
        }

        /// <summary>
        /// Edits the row.
        /// </summary>
        [ValidateAntiForgeryToken]
        public void EditRow()
        {
            var gridModel = new FeedbackGridViewModel();
            var editData = gridModel.OrdersGrid.GetEditData();

            if (gridModel.OrdersGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                var gridData = this.feedBackProvider.All().AsQueryable();
                Feedback order = gridData.Single(o => o.Id == Convert.ToInt16(editData.RowData["Id"]));
                order.Name = Convert.ToString(editData.RowData["Name"]);
                order.Type = Convert.ToInt32(editData.RowData["Type"]);
                order.Text = Convert.ToString(editData.RowData["Text"]);

                this.feedBackProvider.Update(order);
                this.feedBackProvider.Save();
            }
        }

        /// <summary>
        /// Sets up grid.
        /// </summary>
        /// <param name="ordersGrid">The orders grid.</param>
        private void SetUpGrid(JQGrid ordersGrid)
        {
            const int PageSize = 30;
            ordersGrid.ID = "Grid";
            ordersGrid.DataUrl = Url.Action("DataRequest");
            ordersGrid.EditUrl = Url.Action("EditRow");
            ordersGrid.ToolBarSettings.ShowEditButton = true;
            ordersGrid.ToolBarSettings.ShowAddButton = true;
            ordersGrid.ToolBarSettings.ShowDeleteButton = true;
            ordersGrid.EditDialogSettings.CloseAfterEditing = true;
            ordersGrid.AddDialogSettings.CloseAfterAdding = true;         
            ordersGrid.ToolBarSettings.ShowSearchToolBar = false;
            ordersGrid.ToolBarSettings.ShowSearchButton = false;
            ordersGrid.PagerSettings.PageSize = PageSize;
            var orderIdColumns = ordersGrid.Columns.Find(data => data.DataField == "Id");
            orderIdColumns.Searchable = true;
            orderIdColumns.DataType = typeof(int);
            orderIdColumns.SearchToolBarOperation = SearchOperation.IsEqualTo;
        }
    }
}
