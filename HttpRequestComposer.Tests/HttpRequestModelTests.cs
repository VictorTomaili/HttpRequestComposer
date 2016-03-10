using HttpRequestComposer.Models;
using Xunit;

namespace HttpRequestComposer.Tests
{
    public class HttpRequestModelTests
    {
        [Fact]
        public void MethodsMustFilled()
        {
            Assert.Equal(7, new HttpRequestModel().HttpMethods.Count);
        }

        [Fact]
        public void EncodingsMustFilled()
        {
            Assert.Equal(7, new HttpRequestModel().Encodings.Count);
        }
    }
}
