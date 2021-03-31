namespace DetroitHarps.DataAccess.S3.Test
{
    using System;
    using DetroitHarps.DataAccess.S3;
    using Xunit;

    public class GuidKeyConverterTest
    {
        private readonly GuidKeyConverter _converter = new GuidKeyConverter();

        [Fact]
        public void ToStringConvertsGuidToStringTest()
        {
            var guid = Guid.NewGuid();
            Assert.Equal(
                "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                _converter.ToString(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")));
        }

        [Fact]
        public void FromStringConvertsStringToGuidTest()
        {
            var guid = Guid.NewGuid();
            Assert.Equal(
                new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                _converter.FromString("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));
        }

        [Fact]
        public void FromStringThrowsOnInvalidGuidKeyTest()
        {
            var ex = Assert.Throws<FormatException>(() => _converter.FromString("not a guid"));
            Assert.Equal(
                "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).",
                ex.Message);
        }
    }
}