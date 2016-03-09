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

        private bool isDisposed = false;
        ~Program()
        {
            WebApi.Dispose();
            isDisposed = true;
        }

        public void Dispose()
        {
            if(!isDisposed)
                WebApi.Dispose();
        }
    }
}
