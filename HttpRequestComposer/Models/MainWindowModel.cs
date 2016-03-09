using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using HttpRequestComposer.HttpManager;
using PropertyChanged;

namespace HttpRequestComposer.Models
{
    [ImplementPropertyChanged]
    public class MainWindowModel : IHttpRequestModel
    {
        public List<string> HttpMethods { get; set; }
        public string HttpMethod { get; set; }
        public string Url { get; set; }
        public string ContentType { get; set; }
        public string UserAgent { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
        public bool BodyIsFormData { get; set; }
        public string Response { get; set; }

        public MainWindowModel()
        {
            HttpMethods = new List<string>();
            Headers = new Dictionary<string, string>();

            InitializeHttpMethods();
        }

        private void InitializeHttpMethods()
        {
            var methods = (typeof(HttpMethod).GetProperties())
                .Where(s => s.PropertyType == typeof(HttpMethod))
                .Select(s => s.Name);

            foreach (var method in methods)
                HttpMethods.Add(method);

            HttpMethod = HttpMethods.FirstOrDefault();
        }
    }
}
