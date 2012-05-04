using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityTravel.Domain.Services.AuthenticationProvider.Abstract
{
    /// <summary>
    /// Behavior of authentication providers.
    /// </summary>
    public interface IAuthenticationProvider
    {
        /// <summary>
        /// Authentications the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// Bool value of authentication.
        /// </returns>
        bool Authentication(string userName, string password);
    }
}
