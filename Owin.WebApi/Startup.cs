using System;
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
                Console.WriteLine("Request begins: {0} {1}", context.Request.Method, context.Request.Uri);
                await func.Invoke();
                Console.WriteLine("Request ends : {0} {1}", context.Request.Method, context.Request.Uri);
            });

            appBuilder.UseWebApi(config);

        }
    }
}