using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using HttpRequestComposer.HttpManager;
using PropertyChanged;

namespace HttpRequestComposer.Models
{
    [ImplementPropertyChanged]
    public class MainWindowModel : IHttpRequestModel
    {
        public List<string> HttpMethods { get; set; }
        public List<string> Encodings { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public Uri Url { get; set; }
        public MediaTypeWithQualityHeaderValue ContentType { get; set; }
        public string UserAgent { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
        public string Content { get; set; }
        public Encoding Encoding { get; set; }
        public string Response { get; set; }

        public MainWindowModel()
        {
            HttpMethods = new List<string>();
            Encodings = new List<string>();
            Headers = new Dictionary<string, string>();

            InitializeHttpMethods();
            InitializeEncodings();
        }

        private void InitializeEncodings()
        {
            var encodings = (typeof (Encoding).GetProperties())
                .Where(s => s.PropertyType == typeof(Encoding))
                .Select(s => ((Encoding)(s.GetValue(null))).BodyName);

            Encodings.AddRange(encodings);
            Encoding = Encoding.UTF8;
        }

        private void InitializeHttpMethods()
        {
            var methods = (typeof(HttpMethod).GetProperties())
                .Where(s => s.PropertyType == typeof(HttpMethod))
                .Select(s => s.Name.ToUpper(CultureInfo.InvariantCulture));

            HttpMethods.AddRange(methods);

            HttpMethod = new HttpMethod(HttpMethods.FirstOrDefault());
        }
    }
}
