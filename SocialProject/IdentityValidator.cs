using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using SocialProject.DAL.Common.Entities;
using SocialProject.DAL.Core.Repositories;

namespace SocialProject
{
    public sealed class IdentityValidator
    {
        private readonly IRepository<User> _userRepository;

        public IdentityValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateIdentity(CookieValidateIdentityContext validateIdentityContext)
        {
            var claimsIdentity = validateIdentityContext.Identity;
            if (claimsIdentity.IsAuthenticated)
            {
                var userId = claimsIdentity.GetUserId<long>();
                var securityStamp = claimsIdentity.GetSecurityStamp();

                var identity = _userRepository.Get(userId);

                bool isValid = identity != null;

                if (isValid && securityStamp != identity.SecurityStamp)
                    isValid = false;

                if (isValid)
                    validateIdentityContext.ReplaceIdentity(claimsIdentity);
                else
                    validateIdentityContext.RejectIdentity();
            }
        }
    }
}