using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using Xunit;

namespace HttpRequestComposer.Tests
{
    public class ValueConverterTests
    {
        public Dictionary<string, string> TestHeaders => new Dictionary<string, string>
        {
            { "test", "value" },
            { "test2", "value2" }
        };

        [Fact]
        public void BooleanConverterTest()
        {
            var trueValue = "ping";
            var falseValue = "pong";
            var booleanConverter = new BooleanConverter<string>(trueValue, falseValue);

            Assert.Equal(trueValue, booleanConverter.Convert(true, typeof(string), null, CultureInfo.InvariantCulture));
            Assert.Equal(falseValue, booleanConverter.Convert(false, typeof(string), null, CultureInfo.InvariantCulture));
            Assert.Equal(true, booleanConverter.ConvertBack(trueValue, typeof(bool), null, CultureInfo.InvariantCulture));
            Assert.Equal(false, booleanConverter.ConvertBack(falseValue, typeof(bool), null, CultureInfo.InvariantCulture));
        }

        [Fact]
        public void BooleanToVisibilityConverterTest()
        {
            var booleanConverter = new BooleanToVisibilityConverter();

            Assert.Equal(Visibility.Visible, booleanConverter.Convert(true, typeof(string), null, CultureInfo.InvariantCulture));
            Assert.Equal(Visibility.Collapsed, booleanConverter.Convert(false, typeof(string), null, CultureInfo.InvariantCulture));
            Assert.Equal(true, booleanConverter.ConvertBack(Visibility.Visible, typeof(Visibility), null, CultureInfo.InvariantCulture));
            Assert.Equal(false, booleanConverter.ConvertBack(Visibility.Collapsed, typeof(Visibility), null, CultureInfo.InvariantCulture));
        }

        [Fact]
        public void HeaderKeyValueConverterTest()
        {
            var flattenHeaders = "test:value\r\ntest2:value2";
            var headerKeyValueConverter = new HeaderKeyValueConverter();

            Assert.Equal(flattenHeaders, headerKeyValueConverter.Convert(TestHeaders, typeof(string), null, CultureInfo.InvariantCulture));
            Assert.Equal(TestHeaders, headerKeyValueConverter.ConvertBack(flattenHeaders, typeof(Dictionary<string, string>), null, CultureInfo.InvariantCulture));
        }

        [Fact]
        public void HttpMethodConverterTest()
        {
            var httpMethodConverter = new HttpMethodConverter();

            Assert.Equal(HttpMethod.Get.Method, httpMethodConverter.Convert(HttpMethod.Get, typeof(string), null, CultureInfo.InvariantCulture));
            Assert.Equal(HttpMethod.Get, httpMethodConverter.ConvertBack(HttpMethod.Get.Method, typeof(HttpMethod), null, CultureInfo.InvariantCulture));

            Assert.Equal(HttpMethod.Post.Method, httpMethodConverter.Convert(HttpMethod.Post, typeof(string), null, CultureInfo.InvariantCulture));
            Assert.Equal(HttpMethod.Post, httpMethodConverter.ConvertBack(HttpMethod.Post.Method, typeof(HttpMethod), null, CultureInfo.InvariantCulture));

            Assert.Equal(HttpMethod.Put.Method, httpMethodConverter.Convert(HttpMethod.Put, typeof(string), null, CultureInfo.InvariantCulture));
            Assert.Equal(HttpMethod.Put, httpMethodConverter.ConvertBack(HttpMethod.Put.Method, typeof(HttpMethod), null, CultureInfo.InvariantCulture));

            Assert.Equal(HttpMethod.Delete.Method, httpMethodConverter.Convert(HttpMethod.Delete, typeof(string), null, CultureInfo.InvariantCulture));
            Assert.Equal(HttpMethod.Delete, httpMethodConverter.ConvertBack(HttpMethod.Delete.Method, typeof(HttpMethod), null, CultureInfo.InvariantCulture));
        }

        [Fact]
        public void StringContentTypeConverterTest()
        {
            var contentType = "application/json";
            var mediaType = new MediaTypeWithQualityHeaderValue(contentType);
            var stringContentTypeConverter = new StringContentTypeConverter();

            Assert.Equal(contentType, stringContentTypeConverter.Convert(mediaType, typeof(string), null, CultureInfo.InvariantCulture));
            Assert.Equal(mediaType, stringContentTypeConverter.ConvertBack(contentType, typeof(MediaTypeWithQualityHeaderValue), null, CultureInfo.InvariantCulture));
        }

        [Fact]
        public void StringEncodingConverterTest()
        {
            var stringEncodingConverter = new StringEncodingConverter();

            Assert.Equal(Encoding.UTF8.BodyName, stringEncodingConverter.Convert(Encoding.UTF8, typeof(string), null, CultureInfo.InvariantCulture));
            Assert.Equal(Encoding.UTF8, stringEncodingConverter.ConvertBack(Encoding.UTF8.BodyName, typeof(Encoding), null, CultureInfo.InvariantCulture));
        }

        [Fact]
        public void UriStringConverterConverterTest()
        {
            var invalidUri = "github.com";
            var validUri = "http://github.com/";
            var uri = new Uri(validUri);

            var uriStringConverter = new UriStringConverter();

            Assert.Equal(validUri, uriStringConverter.Convert(uri, typeof(string), null, CultureInfo.InvariantCulture));
            Assert.Equal(uri, uriStringConverter.ConvertBack(validUri, typeof(Uri), null, CultureInfo.InvariantCulture));
            Assert.Equal(uri, uriStringConverter.ConvertBack(invalidUri, typeof(Uri), null, CultureInfo.InvariantCulture));
        }
    }
}
