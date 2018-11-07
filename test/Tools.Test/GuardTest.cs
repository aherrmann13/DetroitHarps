namespace Tools.Test
{
    using System;
    using Tools;
    using Xunit;

    public static class GuardTest
    {
        [Fact]
        public static void GuardNotNullThrowsOnNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.NotNull((object)null, "test"));
        }

        [Fact]
        public static void GuardNotNullDoesNotThrowOnObjectTest()
        {
            try
            {
                Guard.NotNull(new object(), "test");
            }
            catch (Exception)
            {
                Assert.False(true);
            }
        }

         [Fact]
        public static void GuardNotNullOrWhiteSpaceThrowsOnNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.NotNullOrWhiteSpace(null, "test"));
        }

         [Fact]
        public static void GuardNotNullOrWhiteSpaceThrowsOnEmptyTest()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.NotNullOrWhiteSpace("   ", "test"));
        }

        [Fact]
        public static void GuardNotNullOrWhiteSpaceDoesNotThrowOnStringTest()
        {
            try
            {
                Guard.NotNullOrWhiteSpace("test", "test");
            }
            catch (Exception)
            {
                Assert.False(true);
            }
        }
    }
}