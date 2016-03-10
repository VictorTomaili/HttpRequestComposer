using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpRequestComposer.HttpManager
{
    public class HttpRequestManager
    {
        private IHttpRequestModel Model { get; set; }

        public HttpRequestManager(IHttpRequestModel model)
        {
            Model = model;
        }

        public virtual async Task<HttpResult> SendRequestAsync()
        {
            UpdateModel();
            var httpClient = CreateHttpClient();
            var httpRequestMessage = CreateHttpRequestMessage();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            return new HttpResult
            {
                HttpRequestMessage = httpRequestMessage,
                HttpResponseMessage = httpResponseMessage
            };
        }

        public void UpdateModel()
        {
            if(Model.Url == null)
                throw new ArgumentNullException(nameof(Model.Url));

            if(Model.HttpMethod == null)
                Model.HttpMethod = HttpMethod.Get;
        }

        public HttpRequestMessage CreateHttpRequestMessage()
        {
            return new HttpRequestMessage
            {
                Content = Model.Content,
                Method = Model.HttpMethod,
                RequestUri = Model.Url
            };
        }

        public HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = Model.Url
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();

            if(Model.ContentType != null)
                httpClient.DefaultRequestHeaders.Accept.Add(Model.ContentType);

            if(Model.Headers.Any())
                httpClient.DefaultRequestHeaders.AddRange(Model.Headers);

            if(!string.IsNullOrEmpty(Model.UserAgent))
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", Model.UserAgent);

            return httpClient;
        }
    }

    public class HttpResult
    {
        public HttpRequestMessage HttpRequestMessage { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}
