using System;
using System.Linq;
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
                Console.WriteLine("Request Headers: {0}", context.Request.Headers.Select(s => $"{s.Key}:{s.Value.Aggregate((k,j) => $"{k}, {j}")}").Aggregate((s,m) => $"{s} {Environment.NewLine} {m}"));
                await func.Invoke();
                Console.WriteLine("Response Headers: {0}", context.Response.Headers.Select(s => $"{s.Key}:{s.Value.Aggregate((k, j) => $"{k}, {j}")}").Aggregate((s, m) => $"{s} {Environment.NewLine} {m}"));
                Console.WriteLine("Request ends : {0} {1}", context.Request.Method, context.Request.Uri);
            });

            appBuilder.UseWebApi(config);

        }
    }
}