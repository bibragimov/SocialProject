﻿using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using SocialProject.DAL.Common.Entities;

namespace SocialProject
{
    public class SocialApiAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="identityTypes">Типы <see cref="User" /> для которых разрешен доступ</param>
        public SocialApiAuthorizeAttribute(params Type[] identityTypes)
        {
            IdentityTypes = identityTypes;
        }

        /// <summary>
        ///     Типы <see cref="User" /> для которых разрешен доступ
        /// </summary>
        public Type[] IdentityTypes { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var isAuthorized = base.IsAuthorized(actionContext);
            if (!isAuthorized)
                return false;

            var user = actionContext.ControllerContext.RequestContext.Principal;

            isAuthorized = user.IsAuthorized(true, IdentityTypes);

            return isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}