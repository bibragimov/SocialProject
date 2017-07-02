using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Practices.Unity;
using Owin;
using SocialProject;
using SocialProject.App_Start;

[assembly: OwinStartup(typeof(Startup))]

namespace SocialProject
{
    public partial class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            var container = UnityConfig.GetConfiguredContainer();

            DataProtectionProvider = app.GetDataProtectionProvider();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                ExpireTimeSpan = TimeSpan.FromDays(7),
                SlidingExpiration = true,
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = container.Resolve<IdentityValidator>().ValidateIdentity
                }
            });
        }
    }
}