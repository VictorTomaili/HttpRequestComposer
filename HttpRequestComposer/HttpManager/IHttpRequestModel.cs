using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace HttpRequestComposer.HttpManager
{
    public interface IHttpRequestModel
    {
        HttpMethod HttpMethod { get; set; }
        Uri Url { get; set; }
        MediaTypeWithQualityHeaderValue ContentType { get; set; }
        string UserAgent { get; set; }
        Dictionary<string, string> Headers { get; set; }
        StringContent Content { get; set; }
        bool BodyIsFormData { get; set; }
        Encoding Encoding { get; set; }
    }
}