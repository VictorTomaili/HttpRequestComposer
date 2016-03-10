using System;
using System.Net.Sockets;
using HttpRequestComposer.HttpManager;
using HttpRequestComposer.Models;
using Xunit;

namespace HttpRequestComposer.Tests
{
    public class HttpGetTestsBase : ApiSelfHostedTestBase
    {
        [Fact]
        public async void HttpRequestManagerMustThrowConnectionRefusedException()
        {
            var baseUrl = "http://localhost:9000/";
            var requestManager = new HttpRequestManager(new MainWindowModel
            {
                Url = new Uri(baseUrl)
            });

            await Assert.ThrowsAsync<SocketException>(async () =>
            {
                await requestManager.SendRequestAsync().ContinueWith(s =>
                {
                    ThrowInnerException(s.Exception);
                });
            });
        }

        [Fact]
        public async void HttpRequestManagerMustReturnListOfValues()
        {
            var values = "[\"value1\",\"value2\"]";
            var status = "Response Status: 200 (OK)";
            var hostUrl = string.Empty;
            await InHostAsync((url) =>
            {
                hostUrl = $"{url}/api/values";
                var requestManager = new HttpRequestManager(new MainWindowModel
                {
                    Url = new Uri(hostUrl)
                });

                return requestManager.SendRequestAsync();
            }).ContinueWith(task =>
            {
                var details = task.Result.ToString();
                Assert.Contains(values, details);
                Assert.Contains(status, details);
                Assert.Contains(hostUrl, details);
            });
        }
    }
}
