using System.ComponentModel.DataAnnotations;

namespace CityTravel.Web.UI.Models
{
    /// <summary>
    /// The make route view model.
    /// </summary>
    public class MakeRouteViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets EndPointLatitude.
        /// </summary>
		[Required(ErrorMessage="Wrong EndPointLatitude")]
        public string EndPointLatitude { get; set; }

        /// <summary>
        /// Gets or sets EndPointLongitude.
        /// </summary>
		[Required(ErrorMessage = "Wrong EndPointLongitude")]
        public string EndPointLongitude { get; set; }

        /// <summary>
        /// Gets or sets StartPointLatitude.
        /// </summary>
		[Required(ErrorMessage = "Wrong StartPointLatitude")]
        public string StartPointLatitude { get; set; }

        /// <summary>
        /// Gets or sets StartPointLongitude.
        /// </summary>
		[Required(ErrorMessage = "Wrong StartPointLongitude")]
        public string StartPointLongitude { get; set; }

		/// <summary>
		/// Gets Address of Point A.
		/// </summary>
		[Required(ErrorMessage = "Wrong Address Point A")]
		public string AddressPointA { get; set; }

		/// <summary>
		/// Gets Address of Point B.
		/// </summary>
		[Required(ErrorMessage = "Wrong Address Point B")]
		public string AddressPointB { get; set; }

        #endregion
    }
}