using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SocialProject.BLL.Core.Services;
using SocialProject.DAL.Common.Entities;

namespace SocialProject.Authorize
{
    /// <summary>
    ///     Служба проверки подлинности
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationManager _authenticationManager;

        /// <summary>
        ///     Конструктор
        /// </summary>
        public AuthenticationService(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        /// <summary>
        ///     Аутентификация <see cref="User" />
        /// </summary>
        public void Login(User user, bool remember = true)
        {
            var userIdentity = user.CreateUserIdentity();

            Logout();

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = remember
            };

            _authenticationManager.SignIn(authenticationProperties, userIdentity);
        }

        /// <summary>
        ///     Sign-out
        /// </summary>
        public void Logout()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}