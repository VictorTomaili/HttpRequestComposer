using System;
using Microsoft.Owin.Hosting;

namespace Owin.WebApi
{
    public class Program : IDisposable
    {
        public static IDisposable WebApi { get; set; }
        static void Main()
        {
            WebApi = WebApp.Start<Startup>(url: "http://localhost:9000/");
            Console.ReadLine();
        }

        public void Dispose()
        {
            WebApi.Dispose();
        }
    }
}
