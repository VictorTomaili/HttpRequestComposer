using System.Net.Http;
using System.Net.Sockets;
using HttpRequestComposer.HttpManager;
using Xunit;

namespace HttpRequestComposer.Tests
{
    public class HttpGetTestsBase : ApiSelfHostedTestBase
    {
        [Fact]
        public void HttpRequestManagerMustThrowConnectionRefusedException()
        {
            var baseUrl = "http://localhost:9000/";
            var requestManager = new HttpRequestManager(baseUrl);

            Assert.Throws<SocketException>(() => requestManager.Send(HttpMethod.Get));
        }

        [Fact]
        public void HttpRequestManagerMustReturnListOfValues()
        {
            var values = "[\"value1\",\"value2\"]";
            var status = "Response Content: Status: 200(OK)";

            InHost((url) =>
            {
                var requestManager = new HttpRequestManager(url);
                var details = requestManager.Send(HttpMethod.Get, "api/values").AsFormattedString();
                Assert.Contains(values, details);
                Assert.Contains(status, details);
                Assert.Contains(url, details);
            });
        }
    }
}
