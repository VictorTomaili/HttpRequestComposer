using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using HttpRequestComposer.HttpManager;
using HttpRequestComposer.Models;
using Xunit;

namespace HttpRequestComposer.Tests
{
    public class HttpHelpersTest
    {
        public Dictionary<string, string> TestHeaders => new Dictionary<string, string>
        {
            { "test", "value" },
            { "test2", "value2" }
        };

        [Fact]
        public void UrlMachTest()
        {
            Assert.True("https://google.com".IsHttpLink());
            Assert.True("http://google.com".IsHttpLink());
            Assert.True("https://127.0.0.1".IsHttpLink());
            Assert.True("http://127.0.0.1".IsHttpLink());
            Assert.True("https://google.com/".IsHttpLink());
            Assert.True("http://google.com/".IsHttpLink());
            Assert.True("https://google".IsHttpLink());
            Assert.True("http://google".IsHttpLink());
            Assert.True("https://google.com/test".IsHttpLink());
            Assert.True("http://google.com/test".IsHttpLink());
            Assert.True("https://google.com/test?key=value".IsHttpLink());
            Assert.True("http://google.com/test?key=value".IsHttpLink());
            Assert.True("https://google.com/test?key=value&key2=multipleValue".IsHttpLink());
            Assert.True("http://google.com/test?key=value&key2=multipleValue".IsHttpLink());
            Assert.False("hello@hello".IsHttpLink());
            Assert.False("hello.123".IsHttpLink());
            Assert.False("https//hello".IsHttpLink());
            Assert.False("http//hello".IsHttpLink());
            Assert.False("ftp://address".IsHttpLink());
        }

        [Fact]
        public void HttpRequestHeadersAddRangeTest()
        {
            var headers = TestHeaders;
            using (var request = new HttpRequestMessage())
            {
                var httpheaders = request.Headers;

                httpheaders.AddRange(headers);

                foreach (var header in headers)
                {
                    Assert.True(httpheaders.Any(s => s.Key == header.Key && header.Value == s.Value.FirstOrDefault()));
                }
            }
        }

        [Fact]
        public void AppendEnumarableTests()
        {
            var headers = TestHeaders;
            using (var request = new HttpRequestMessage())
            {
                var httpheaders = request.Headers;

                httpheaders.AddRange(headers);

                var str = new StringBuilder().AppendEnumerable(httpheaders).ToString();

                Assert.Equal("test: value\r\ntest2: value2\r\n", str);
            }
        }

        [Fact]
        public void HttpResponseMessageStringifyTest()
        {
            var expectedResponse = "Response Status: 202 (Accepted)\r\nResponse Headers:\r\ntest: value\r\ntest2: value2\r\n\r\nResponse Content: \r\n{ test:'value'}\r\n\r\n";
            using (var response = new HttpResponseMessage())
            {
                response.StatusCode = HttpStatusCode.Accepted;
                response.Headers.AddRange(TestHeaders);
                response.Content = new StringContent("{ test:'value'}", Encoding.UTF8, "application/json");

                var responseText = response.Stringify().ToString();
                Assert.Equal(expectedResponse, responseText);
            }
        }

        [Fact]
        public void HttpResponseMessageStringifyTestWithNoResponseContent()
        {
            var expectedResponse = "Response Status: 202 (Accepted)\r\nResponse Headers:\r\ntest: value\r\ntest2: value2\r\n\r\n";
            using (var response = new HttpResponseMessage())
            {
                response.StatusCode = HttpStatusCode.Accepted;
                response.Headers.AddRange(TestHeaders);

                var responseText = response.Stringify().ToString();
                Assert.Equal(expectedResponse, responseText);
            }
        }

        [Fact]
        public void IHttpRequestModelStringifyTestWithNoContent()
        {
            var httpRequestModel = new HttpRequestModel();

            var responseText = httpRequestModel.Stringify().ToString();
            Assert.Equal("", responseText);
        }

        [Fact]
        public void HttpRequestMessageStringifyTest()
        {
            var expectedResponse = "General:\r\nGET http://127.0.0.1/ HTTP/1.1\r\nHost: 127.0.0.1\r\n\r\nRequest Headers:\r\ntest: value, value\r\ntest2: value2, value2\r\ntest: \r\n\r\n";
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri("http://127.0.0.1");
                request.Headers.AddRange(TestHeaders);
                request.Version = Version.Parse("1.1");
                request.Headers.AddRange(TestHeaders);
                request.Properties.Add(new KeyValuePair<string, object>("test", "test"));

                var requestText = request.Stringify().ToString();
                Assert.Equal(expectedResponse, requestText);
            }
        }
    }
}
