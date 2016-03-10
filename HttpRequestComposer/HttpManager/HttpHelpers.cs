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
        public static string AsFormattedString(this HttpResult result)
        {
            var builder = new StringBuilder();

            builder.AppendLine("General:")
                .Append(result.HttpRequestMessage.Method).Append(" ")
                .Append(result.HttpRequestMessage.RequestUri).Append(" ")
                .Append("HTTP/").Append(result.HttpRequestMessage.Version).Append(Environment.NewLine)
                .Append("Host: ").Append(result.HttpRequestMessage.RequestUri.Host).Append(Environment.NewLine)
                .Append(Environment.NewLine)
                .AppendLine("Request Headers:")
                .AppendEnumerable(result.HttpRequestMessage.Headers)
                .AppendEnumerable(result.HttpRequestMessage.Properties)
                .Append(Environment.NewLine);

            if (result.HttpRequestMessage.Content != null)
                builder.Append("Request Content: ").Append(result.HttpRequestMessage.Content.ReadAsStringAsync().Result).Append(Environment.NewLine);

            builder.Append("Response Status: ")
                .Append((int)result.HttpResponseMessage.StatusCode)
                .Append(" (").Append(result.HttpResponseMessage.StatusCode).Append(")")
                .Append(Environment.NewLine)
                .AppendLine("Response Headers:")
                .AppendEnumerable(result.HttpResponseMessage.Headers)
                .Append(Environment.NewLine);

            if (result.HttpResponseMessage.Content != null)
                builder.AppendLine("Response Content: ")
                    .AppendLine(result.HttpResponseMessage.Content.ReadAsStringAsync().Result).Append(Environment.NewLine);

            return builder.ToString();
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