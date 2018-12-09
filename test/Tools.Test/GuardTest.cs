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
        public static void GuardNotNullWithExceptionThrowsOnNullTest()
        {
            const string paramName = "test";
            var exception = Assert.Throws<InvalidOperationException>(
                () => Guard.NotNull(
                    (object)null,
                    paramName,
                    e => new InvalidOperationException(message: e.Message, innerException: e)));

            Assert.Equal(exception.Message, $"Value cannot be null.\nParameter name: {paramName}");
            Assert.Equal(typeof(ArgumentNullException), exception.InnerException.GetType());
        }

        [Fact]
        public static void GuardNotNullWithExceptionDoesNotThrowOnObjectTest()
        {
            try
            {
                Guard.NotNull(new object(), "test", e => new InvalidOperationException(e.Message, e));
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