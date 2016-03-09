using System.Collections.Generic;

namespace HttpRequestComposer.HttpManager
{
    public interface IHttpRequestModel
    {
        string HttpMethod { get; }
        string Url { get; }
        string ContentType { get; }
        string UserAgent { get; }
        Dictionary<string, string> Headers { get; }
        string Body { get; }
        bool BodyIsFormData { get; }
    }
}