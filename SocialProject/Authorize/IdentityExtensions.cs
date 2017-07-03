using System;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using SocialProject.DAL.Common.Entities;

namespace SocialProject.Authorize
{
    /// <summary>
    ///     Расширения для идентификаторов
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        static IdentityExtensions()
        {
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            UserIdClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            UserNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
            SecurityStampClaimType = "AspNet.Identity.SecurityStamp";
            MarketIdentityClaimType = "Market.IdentityType";
            MarketIdentityClaimTypeBase = "Market.IdentityType.Base";
            UserFullNameClaimType = "Market.FullName";
        }

        public static string RoleClaimType { get; }

        public static string UserNameClaimType { get; }

        public static string UserIdClaimType { get; }

        public static string SecurityStampClaimType { get; }

        public static string MarketIdentityClaimType { get; }

        public static string MarketIdentityClaimTypeBase { get; private set; }

        public static string UserFullNameClaimType { get; private set; }

        /// <summary>
        ///     Создает идентификатор <see cref="ClaimsIdentity" /> для <seealso cref="User" />
        /// </summary>
        public static ClaimsIdentity CreateUserIdentity(this User user)
        {
            var id = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie, UserNameClaimType, RoleClaimType);
            id.AddClaim(new Claim(UserIdClaimType, user.Id.ToString(), "http://www.w3.org/2001/XMLSchema#string"));
            id.AddClaim(new Claim(UserNameClaimType, user.Login, "http://www.w3.org/2001/XMLSchema#string"));
            id.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));
            id.AddClaim(new Claim(SecurityStampClaimType, user.SecurityStamp));
            id.AddClaim(new Claim(MarketIdentityClaimType, user.GetType().Name));
            id.AddClaim(new Claim(RoleClaimType, "User", "http://www.w3.org/2001/XMLSchema#string"));

            return id;
        }

        /// <summary>
        ///     Проверяет, авториован ли заданный пользователь
        /// </summary>
        /// <param name="principal"><see cref="IPrincipal" /> пользователя</param>
        /// <param name="checkSecurityStamp">Указывает необходимость проверки SecurityStamp пользователя</param>
        /// <param name="identityTypes">Типы пользователей</param>
        public static bool IsAuthorized(this IPrincipal principal, bool checkSecurityStamp = false,
            params Type[] identityTypes)
        {
            var claimsIdentity = principal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return false;

            foreach (var identityType in identityTypes)
            {
                if (!typeof (User).IsAssignableFrom(identityType))
                    throw new Exception(string.Format("Wrong IdentityType {0}", identityType));

                if (claimsIdentity.HasClaim(MarketIdentityClaimType, identityType.Name))
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Проверяет, является ли пользователь <see cref="User" />
        /// </summary>
        public static bool IsUser(this IPrincipal principal)
        {
            return IsAuthorized(principal, false, typeof (User));
        }

        /// <summary>
        ///     Получение штампа безопасности <see cref="ClaimsIdentity" />
        /// </summary>
        /// <param name="claimsIdentity">Идентификатор доступа</param>
        public static string GetSecurityStamp(this ClaimsIdentity claimsIdentity)
        {
            if (!claimsIdentity.IsAuthenticated)
                throw new InvalidOperationException();

            return claimsIdentity.FindFirst(SecurityStampClaimType).Value;
        }
    }
}