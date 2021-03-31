namespace DetroitHarps.DataAccess.S3.Test
{
    using System;
    using DetroitHarps.DataAccess.S3;
    using Xunit;

    public class IntKeyConverterTest
    {
        private readonly IntKeyConverter _converter = new IntKeyConverter();

        [Fact]
        public void ToStringConvertsIntToStringTest()
        {
            Assert.Equal("5", _converter.ToString(5));
        }

        [Fact]
        public void FromStringConvertsStringToIntTest()
        {
            Assert.Equal(5, _converter.FromString("5"));
        }

        [Fact]
        public void FromStringThrowsOnInvalidStringKeyTest()
        {
            var ex = Assert.Throws<FormatException>(() => _converter.FromString("not an int"));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }
    }
}