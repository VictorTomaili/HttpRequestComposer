using System;
using System.Globalization;
using System.Net.Http.Headers;
using System.Windows.Data;

namespace HttpRequestComposer
{
    public class StringContentTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as MediaTypeWithQualityHeaderValue)?.MediaType ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as string;
            MediaTypeWithQualityHeaderValue mediaType = null;
            MediaTypeWithQualityHeaderValue.TryParse(val, out mediaType);
            return mediaType;
        }
    }
}