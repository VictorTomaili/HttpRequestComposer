using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace HttpRequestComposer.HttpManager
{
    public static class HttpHelpers
    {
        public static StringBuilder Stringify(this HttpRequestMessage request)
        {
            return new StringBuilder()
                .AppendLine("General:")
                .Append(request.Method).Append(" ")
                .Append(request.RequestUri).Append(" ")
                .Append("HTTP/").Append(request.Version).Append(Environment.NewLine)
                .Append("Host: ").Append(request.RequestUri.Host).Append(Environment.NewLine)
                .Append(Environment.NewLine)
                .AppendLine("Request Headers:")
                .AppendEnumerable(request.Headers)
                .AppendEnumerable(request.Properties)
                .Append(Environment.NewLine);
        }

        public static StringBuilder Stringify(this IHttpRequestModel model)
        {
            var builder = new StringBuilder();
            if (string.IsNullOrEmpty(model.Content))
                builder.Append("Request Content: ").Append(model.Content).Append(Environment.NewLine);
            return builder;
        }

        public static StringBuilder Stringify(this HttpResponseMessage response)
        {
            var builder = new StringBuilder()
                .Append("Response Status: ")
                .Append((int)response.StatusCode)
                .Append(" (").Append(response.StatusCode).Append(")")
                .Append(Environment.NewLine)
                .AppendLine("Response Headers:")
                .AppendEnumerable(response.Headers)
                .Append(Environment.NewLine);

            if (response.Content != null)
                builder.AppendLine("Response Content: ")
                    .AppendLine(response.Content.ReadAsStringAsync().Result).Append(Environment.NewLine);

            return builder;
        }

        public static StringBuilder AppendEnumerable<T>(this StringBuilder builder, IEnumerable<KeyValuePair<string, T>> keyValuePairs)
        {
            return builder.AppendEnumerable(keyValuePairs.Select(s => new KeyValuePair<string, object>(s.Key, s.Value)));
        }

        public static StringBuilder AppendEnumerable(this StringBuilder builder, IEnumerable<KeyValuePair<string, object>> keyValuePairs)
        {
            foreach (var keyValuePair in keyValuePairs)
            {
                builder.Append(keyValuePair.Key).Append(": ");

                var valueList = keyValuePair.Value as IEnumerable<object>;
                if (valueList != null)
                    builder.Append(valueList.Aggregate((s, m) => $"{s}, {m}"));

                builder.Append(Environment.NewLine);
            }

            return builder;
        }

        public static bool IsUrl(this string val)
        {
            return Regex.Match(val, @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?(/)?$").Success;
        }

        public static void AddRange(this HttpRequestHeaders headers, IDictionary<string, string> dictionary)
        {
            foreach (var keyValuePair in dictionary)
            {
                headers.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}