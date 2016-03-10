using System.Net.Http;

namespace HttpRequestComposer.HttpManager
{
    public class HttpResult
    {
        public HttpRequestMessage HttpRequestMessage { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
        public IHttpRequestModel Model { get; set; }

        public override string ToString()
        {
            return HttpRequestMessage.Stringify()
                    .Append(Model.Stringify())
                    .Append(HttpResponseMessage.Stringify())
                    .ToString();
        }
    }
}