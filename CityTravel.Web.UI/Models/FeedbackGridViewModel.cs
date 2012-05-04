using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trirand.Web.Mvc;

namespace CityTravel.Web.UI.Models
{
    using System.Web.UI.WebControls;

    /// <summary>
    /// JQGrid model for feedback list.
    /// </summary>
    public class FeedbackGridViewModel
    {
        /// <summary>
        /// Width of grid.
        /// </summary>
        private const int Width = 1024;

        /// <summary>
        /// Height of width.
        /// </summary>
        private const int Height = 600;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackGridViewModel"/> class. 
        /// </summary>
        public FeedbackGridViewModel()
        {
            this.OrdersGrid = new JQGrid
                {
                    Columns =
                        new List<JQGridColumn>()
                            {
                                new JQGridColumn { DataField = "Id", PrimaryKey = true, Editable = false, Width = 100 },
                                new JQGridColumn { DataField = "Name", Editable = true, Width = 200 },
                                new JQGridColumn { DataField = "Type", Editable = true, Width = 200, },
                                new JQGridColumn { DataField = "Text", Editable = true, Width = 400 }
                            },
                    Width = Unit.Pixel(Width),
                    Height = Unit.Pixel(Height),
                    ToolBarSettings = {
                                          ShowRefreshButton = true 
                                      }
                };
        }

        /// <summary>
        /// Gets or sets the orders grid.
        /// </summary>
        /// <value>
        /// The orders grid.
        /// </value>
        public JQGrid OrdersGrid { get; set; }
    }
}
