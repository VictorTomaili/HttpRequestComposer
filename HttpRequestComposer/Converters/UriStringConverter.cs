using System;
using System.Globalization;
using System.Windows.Data;
using HttpRequestComposer.HttpManager;

namespace HttpRequestComposer
{
    [ValueConversion(typeof(string), typeof(Uri))]
    public class UriStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value?.ToString()) ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as string;
            if (string.IsNullOrEmpty(val)) return null;

            return val.IsHttpLink() ?
                new Uri(val) :
                new Uri($"http://{val}");
        }
    }
}