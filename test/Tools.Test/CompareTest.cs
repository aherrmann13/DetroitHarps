namespace Tools.Test
{
    using Tools;
    using Xunit;

    public static class CompareTest
    {
        [Fact]
        public static void CompareEqualOrdinalReturnsTrueOnEqualStringsTest()
        {
            Assert.True(Compare.EqualOrdinal("stringA", "stringA"));
        }

        [Fact]
         public static void CompareEqualOrdinalReturnsFalseOnEqualStringsDifferentCapitalizationTest()
        {
            Assert.False(Compare.EqualOrdinal("stringA", "stringa"));
        }

        [Fact]
         public static void CompareEqualOrdinalReturnsFalseOnUnequalStrings()
        {
            Assert.False(Compare.EqualOrdinal("stringA", "stringB"));
        }
    }
}