using System;
using System.Threading;
using Owin.WebApi;
using Microsoft.Owin.Hosting;

namespace HttpRequestComposer.Tests
{
    public abstract class ApiSelfHostedTestBase
    {
        public void InHost(Action<string> action)
        {
            var url = $"http://localhost:3{Thread.CurrentThread.ManagedThreadId:D4}/";
            using (WebApp.Start<Startup>(url))
            {
                action(url);
            }
        }
    }
}
