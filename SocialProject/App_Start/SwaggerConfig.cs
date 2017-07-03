using System;
using System.Web.Http;
using SocialProject;
using Swashbuckle.Application;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof (SwaggerConfig), "Register")]

namespace SocialProject
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof (SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "SocialProject API");

                    c.IncludeXmlComments(string.Format(@"{0}\bin\SocialProject.XML",
                        AppDomain.CurrentDomain.BaseDirectory));
                })
                .EnableSwaggerUi(c => { });
        }
    }
}