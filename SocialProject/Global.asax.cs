using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using SocialProject.BLL.Common.MapperConfiguration;

namespace SocialProject
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.Formatting =
                Formatting.Indented;

            MapperConfig.Configure();
        }
    }
}