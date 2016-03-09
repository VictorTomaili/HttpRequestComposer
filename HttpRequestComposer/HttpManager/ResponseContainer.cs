using System.Net;
using System.Net.Http;

namespace HttpRequestComposer.HttpManager
{
    public class ResponseContainer
    {
        public HttpResponseMessage HttpResponseMessage { get; set; }
        public WebException WebException { get; set; }

        public bool IsSuccess =>
            (HttpResponseMessage != null && WebException == null);

        public ResponseContainer(HttpResponseMessage httpResponseMessage)
        {
            HttpResponseMessage = httpResponseMessage;
        }

        public ResponseContainer(WebException webException)
        {
            WebException = webException;
        }
    }
}