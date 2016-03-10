using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace HttpRequestComposer
{
    [ValueConversion(typeof(string), typeof(Encoding))]
    public class StringEncodingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Encoding)?.BodyName ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as string;
            return !string.IsNullOrEmpty(val)?
                Encoding.GetEncoding(val) :
                null;
        }
    }
}