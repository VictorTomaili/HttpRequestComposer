using System;
using System.Globalization;
using System.Net.Http;
using System.Windows.Data;

namespace HttpRequestComposer
{
    [ValueConversion(typeof(string), typeof(HttpMethod))]
    public class HttpMethodConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as HttpMethod)?.Method ?? HttpMethod.Get.Method;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new HttpMethod((value as string ?? HttpMethod.Post.Method).ToUpper(CultureInfo.InvariantCulture));
        }
    }
}