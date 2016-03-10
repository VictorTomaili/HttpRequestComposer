using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using HttpRequestComposer.HttpManager;
using HttpRequestComposer.Models;
using Newtonsoft.Json;
using Xunit;

namespace HttpRequestComposer.Tests
{
    public class HttpRequestManagerTests : ApiSelfHostedTestBase
    {
        [Fact]
        public async void HttpRequestManagerMustThrowConnectionRefusedException()
        {
            var baseUrl = "http://localhost:9000/";
            var requestManager = new HttpRequestManager(new HttpRequestModel
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
        public async void HttpRequestManagerGet()
        {
            var values = "[\"value1\",\"value2\"]";
            var status = "Response Status: 200 (OK)";
            var hostUrl = string.Empty;
            await InHostAsync(url =>
            {
                hostUrl = $"{url}api/values";
                return new HttpRequestManager(new HttpRequestModel
                {
                    Url = new Uri(hostUrl)
                }).SendRequestAsync();

            }).ContinueWith(task =>
            {
                var details = task.Result.ToString();
                Assert.Contains(values, details);
                Assert.Contains(status, details);
                Assert.Contains(hostUrl, details);
            });
        }

        [Fact]
        public async void HttpRequestManagerGetValueXml()
        {
            var value = "<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/\">value</string>";
            var status = "Response Status: 200 (OK)";
            var headerXml = "Accept: application/xml";

            await InHostAsync(url =>
            {
                url += "api/values/1";
                return new HttpRequestManager(new HttpRequestModel
                {
                    Url = new Uri(url),
                    HttpMethod = HttpMethod.Get,
                    Encoding = Encoding.UTF8,
                    ContentType = MediaTypeXml
                }).SendRequestAsync();

            }).ContinueWith(task =>
            {
                Assert.Null(task.Exception);

                var result = task.Result.ToString();
                Assert.Contains(value, result);
                Assert.Contains(status, result);
                Assert.Contains(headerXml, result);
            });
        }

        [Fact]
        public async void HttpRequestManagerPost()
        {
            var created = "Response Status: 201 (Created)";
            await InHostAsync(url =>
            {
                url += "api/values";
                return new HttpRequestManager(new HttpRequestModel
                {
                    Url = new Uri(url),
                    HttpMethod = HttpMethod.Post,
                    Encoding = Encoding.UTF8,
                    ContentType = MediaTypeJson,
                    Content = JsonConvert.SerializeObject(new { value = "world" })
                }).SendRequestAsync();
            }).ContinueWith(task =>
            {
                Assert.Null(task.Exception);

                var result = task.Result.ToString();
                Assert.Contains(created, result);
            });
        }

        [Fact]
        public async void HttpRequestManagerPostWrongPostData()
        {
            var created = "Response Status: 201 (Created)";
            await InHostAsync(url =>
            {
                url += "api/values";
                return new HttpRequestManager(new HttpRequestModel
                {
                    Url = new Uri(url),
                    HttpMethod = HttpMethod.Post,
                    Encoding = Encoding.UTF8,
                    ContentType = MediaTypeJson,
                    Content = JsonConvert.SerializeObject(new { value = "world" })
                }).SendRequestAsync();
            }).ContinueWith(task =>
            {
                Assert.Null(task.Exception);

                var result = task.Result.ToString();
                Assert.Contains(created, result);
            });
        }


        [Fact]
        public async void HttpRequestManagerPut()
        {
            var accepted = "Response Status: 202 (Accepted)";
            await InHostAsync(url =>
            {
                url += "api/values/1";
                return new HttpRequestManager(new HttpRequestModel
                {
                    Url = new Uri(url),
                    HttpMethod = HttpMethod.Put,
                    Encoding = Encoding.UTF8,
                    ContentType = MediaTypeJson,
                    Content = JsonConvert.SerializeObject(new { value = "world" })
                }).SendRequestAsync();
            }).ContinueWith(task =>
            {
                Assert.Null(task.Exception);

                var result = task.Result.ToString();
                Assert.Contains(accepted, result);
            });
        }

        [Fact]
        public async void HttpRequestManagerDelete()
        {
            var noContent = "Response Status: 204 (NoContent)";
            await InHostAsync(url =>
            {
                url += "api/values/1";
                return new HttpRequestManager(new HttpRequestModel
                {
                    Url = new Uri(url),
                    HttpMethod = HttpMethod.Delete,
                    Encoding = Encoding.UTF8,
                    ContentType = MediaTypeJson,
                }).SendRequestAsync();
            }).ContinueWith(task =>
            {
                Assert.Null(task.Exception);

                var result = task.Result.ToString();
                Assert.Contains(noContent, result);
            });
        }
    }
}
