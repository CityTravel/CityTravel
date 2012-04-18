using System.Security.Principal;
using System.Web;

namespace CityTravel.Tests.Helpers
{
    /// <summary>
    /// The mock http context.
    /// </summary>
    public class MockHttpContext : HttpContextBase
    {
        #region Constants and Fields

        /// <summary>
        /// The user.
        /// </summary>
        private readonly IPrincipal user = new GenericPrincipal(new GenericIdentity("SomeUser"), null /* roles */);

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public override IPrincipal User
        {
            get
            {
                return this.user;
            }

            set
            {
                base.User = value;
            }
        }

        #endregion
    }
}