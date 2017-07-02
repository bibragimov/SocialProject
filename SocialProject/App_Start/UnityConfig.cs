using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using SocialProject.DAL.Core;
using SocialProject.DAL.Common;
using SocialProject.DAL.Common.Entities;
using SocialProject.DAL.Common.Repositories;
using SocialProject.BLL.Core.CQRS;
using System.Collections.Generic;
using System.Drawing;
using SocialProject.BLL.Common.CQRS.Queries.Users;
using SocialProject.BLL.Common.Models;
using SocialProject.DAL.Core.Repositories;
using SocialProject.BLL.Common.CQRS.Commands;
using Microsoft.Owin.Security;
using System.Web;
using Microsoft.Practices.ServiceLocation;

namespace SocialProject.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            var unityServiceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);

            container.RegisterType<IAuthenticationManager>(new PerResolveLifetimeManager(),
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<AuthenticationService, AuthenticationService>(new PerResolveLifetimeManager());
            container.RegisterType<IdentityValidator, IdentityValidator>(new ContainerControlledLifetimeManager());
            

            #region Repositories
            container.RegisterType<IRepository<User>, UserRepository>(new PerRequestLifetimeManager());
            #endregion

            #region DB
            container.RegisterType<IDBContext, SocialProjectContext>(new PerRequestLifetimeManager());
            #endregion

            #region Query
            container.RegisterType<IQueryHandler<GetUserInfoQuery, UserInfoDto>, GetUserInfoQueryHandler>(new PerRequestLifetimeManager());
            container.RegisterType<IQueryHandler<GetAllUsersQuery, List<UserInfoDto>>, GetAllUsersQueryHandler>(new PerRequestLifetimeManager());
            container.RegisterType<IQueryHandler<CheckAuthQuery, User>, CheckAuthQueryHandler>(new PerRequestLifetimeManager());
            #endregion

            #region Command
            container.RegisterType<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>(new PerRequestLifetimeManager());
            #endregion

        }
    }
}
