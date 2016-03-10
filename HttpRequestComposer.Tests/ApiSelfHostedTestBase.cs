using System;
using System.Threading;
using System.Threading.Tasks;
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

        public Task InHostAsync(Func<string, Task> action)
        {
            var url = $"http://localhost:3{Thread.CurrentThread.ManagedThreadId:D4}/";
            using (WebApp.Start<Startup>(url))
            {
                return action(url);
            }
        }
    }
}
