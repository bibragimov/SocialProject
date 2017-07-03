using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using SocialProject.DAL.Common.Entities;
using SocialProject.DAL.Core.Repositories;

namespace SocialProject.Authorize
{
    /// <summary>
    ///     Валидация идентификаторов
    /// </summary>
    public sealed class IdentityValidator
    {
        private readonly IRepository<User> _userRepository;

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="userRepository"> Репозиторий для работы с пользовательскими данными </param>
        public IdentityValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        ///     Проверка кук <seealso cref="CookieValidateIdentityContext" />
        /// </summary>
        /// <param name="validateIdentityContext"></param>
        /// <returns></returns>
        public async Task ValidateIdentity(CookieValidateIdentityContext validateIdentityContext)
        {
            var claimsIdentity = validateIdentityContext.Identity;
            if (claimsIdentity.IsAuthenticated)
            {
                var userId = claimsIdentity.GetUserId<long>();
                var securityStamp = claimsIdentity.GetSecurityStamp();

                var identity = _userRepository.Get(userId);

                var isValid = identity != null;

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