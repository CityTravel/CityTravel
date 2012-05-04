namespace CityTravel.Domain.Services.AuthenticationProvider.Concrete
{
    using System.Web.Security;
    using CityTravel.Domain.Services.AuthenticationProvider.Abstract;

    /// <summary>
    /// Concrete form authentication.
    /// </summary> 
    public class FormsAuthenticationProvider : IAuthenticationProvider
    {
        /// <summary>
        /// Authentications the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// Bool value of authentication.
        /// </returns>
        public bool Authentication(string userName, string password)
        {
            bool result = FormsAuthentication.Authenticate(userName, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(userName, false);
            }

            return result;
        }
    }
}
