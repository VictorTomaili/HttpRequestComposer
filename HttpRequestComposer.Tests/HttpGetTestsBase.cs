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
        public void HttpRequestManagerMustThrowConnectionRefusedException()
        {
            var baseUrl = "http://localhost:9000/";
            var requestManager = new HttpRequestManager(new MainWindowModel
            {
                Url = new Uri(baseUrl)
            });

            Assert.Throws<SocketException>(() => requestManager.SendRequestAsync().Wait());
        }

        [Fact]
        public void HttpRequestManagerMustReturnListOfValues()
        {
            var values = "[\"value1\",\"value2\"]";
            var status = "Response Content: Status: 200(OK)";

            InHost((url) =>
            {
                var requestManager = new HttpRequestManager(new MainWindowModel
                {
                    Url = new Uri(url)
                });

                requestManager.SendRequestAsync().ContinueWith(task =>
                {
                    var details = task.Result.AsFormattedString();
                    Assert.Contains(values, details);
                    Assert.Contains(status, details);
                    Assert.Contains(url, details);
                }).Wait();
            });
        }
    }
}
