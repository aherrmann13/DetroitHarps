namespace DetroitHarps.DataAccess.S3.Test
{
    using DetroitHarps.DataAccess.S3;
    using Xunit;

    public class StringKeyConverterTest
    {
        private readonly StringKeyConverter _converter = new StringKeyConverter();

        [Fact]
        public void ToStringReturnsStringTest()
        {
            Assert.Equal("some str", _converter.ToString("some str"));
        }

        [Fact]
        public void FromStringReturnsStringTest()
        {
            Assert.Equal("some str", _converter.FromString("some str"));
        }
    }
}