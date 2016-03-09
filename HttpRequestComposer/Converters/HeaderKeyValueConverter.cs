using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HttpRequestComposer
{
    public class HeaderKeyValueConverter : IValueConverter
    {
        public static IValueConverter Instance  = new HeaderKeyValueConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var headers = value as IDictionary<string, string>;

            if (headers == null || !headers.Any())
                return null;

            return headers.Select(s => $"{s.Key}:{s.Value}")
                .Aggregate((s, m) => $"{s}{Environment.NewLine}{m}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var headerValuesDictionary = new Dictionary<string, string>();
            var headerStr = value as string;
            if (headerStr == null)
                return headerValuesDictionary;

            var headerValueLines = headerStr.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var headerValueLine in headerValueLines)
            {
                var keyValue = headerValueLine.Split(':');
                if(keyValue.Length == 2)
                    headerValuesDictionary.Add(keyValue[0], keyValue[1]);
            }

            return headerValuesDictionary;
        }
    }
}