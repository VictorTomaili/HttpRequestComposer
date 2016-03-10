using System.Web.Http;

namespace Owin.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            appBuilder.Use(async (context, func) =>
            {
                await func.Invoke();
            });

            appBuilder.UseWebApi(config);

        }
    }
}