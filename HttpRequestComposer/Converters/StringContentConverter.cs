using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Windows.Data;
using HttpRequestComposer.HttpManager;

namespace HttpRequestComposer
{
    public class StringContentConverter : IValueConverter
    {
        public IHttpRequestModel Model { get; set; }

        public StringContentConverter(IHttpRequestModel model)
        {
            Model = model;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as StringContent)?.ReadAsStringAsync().Result ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as string;
            if (string.IsNullOrEmpty(val))
                return string.Empty;
            return new StringContent(val, Model.Encoding ?? Encoding.UTF8, Model.ContentType?.MediaType);
        }

        public static StringContentConverter Instance { get; set; }
    }
}