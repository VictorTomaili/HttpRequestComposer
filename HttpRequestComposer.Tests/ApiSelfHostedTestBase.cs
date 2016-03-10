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

        public async Task<T> InHostAsync<T>(Func<string, Task<T>> action)
        {
            var url = $"http://localhost:3{Thread.CurrentThread.ManagedThreadId:D4}/";
            using (WebApp.Start<Startup>(url))
            {
                return await action(url);
            }
        }

        public void ThrowInnerException(Exception ex)
        {
            while (true)
            {
                if (ex.InnerException == null)
                    throw ex;

                ex = ex.InnerException;
            }
        }
    }
}
