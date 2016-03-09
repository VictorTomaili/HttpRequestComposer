using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestComposer.HttpManager
{
    public class HttpRequestManager : IDisposable
    {
        protected virtual HttpClient HttpClient { get; }
        protected virtual string Content { get; set; }
        protected virtual string ContentType { get; set; }
        protected virtual HttpRequestMessage HttpRequestMessage { get; }

        public HttpRequestManager() : this("http://127.0.0.1") {}

        public HttpRequestManager(string url) : this(new Uri(url)) {}

        public HttpRequestManager(Uri uri)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = uri
            };

            HttpRequestMessage = new HttpRequestMessage();
        }

        public virtual HttpRequestManager SetContent(string contentType)
        {
            return SetContent(string.Empty, contentType);
        }

        public virtual HttpRequestManager SetContent(string content, string contentType)
        {
            Content = content;
            ContentType = contentType;
            return this;
        }

        public virtual HttpRequestManager AddHeaderRange(Dictionary<string, string> headers)
        {
            HttpClient.DefaultRequestHeaders.AddRange(headers);
            return this;
        }

        public virtual HttpRequestManager AddHeader(string name, string value)
        {
            return AddHeader(name, new [] {value});
        }

        public virtual HttpRequestManager AddHeader(string name, IEnumerable<string> value)
        {
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(name, value);
            return this;
        }

        public virtual HttpResponseMessage Send(HttpMethod method) => Send(method.Method, string.Empty);
        public virtual HttpResponseMessage Send(string method) => Send(method, string.Empty);
        public virtual HttpResponseMessage Send(HttpMethod method, string path) => Send(method.Method, path);
        public virtual HttpResponseMessage Send(string method, string path) => SendAsync(method, path).Result;
        public virtual Task<HttpResponseMessage> SendAsync(HttpMethod method) => SendAsync(method.Method, string.Empty);
        public virtual Task<HttpResponseMessage> SendAsync(string method) => SendAsync(method, string.Empty);
        public virtual Task<HttpResponseMessage> SendAsync(HttpMethod method, string path) => SendAsync(method.Method, path);
        public virtual Task<HttpResponseMessage> SendAsync(string method, string path)
        {
            try
            {
                var uriPath = HttpClient.BaseAddress;
                if (!string.IsNullOrEmpty(path))
                    uriPath = new Uri(path);

                if (string.IsNullOrEmpty(ContentType))
                    ContentType = "text/html";

                HttpClient.DefaultRequestHeaders.Accept.Clear();
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));

                if (!string.IsNullOrEmpty(Content))
                    HttpRequestMessage.Content = new StringContent(Content, Encoding.UTF8, ContentType);


                HttpRequestMessage.Method = new HttpMethod(method);
                HttpRequestMessage.RequestUri = uriPath;

                return HttpClient.SendAsync(HttpRequestMessage);
            }
            catch (Exception ex)
            {
                ex.ThrowInnerException();
                throw;
            }
        }

        public virtual void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
