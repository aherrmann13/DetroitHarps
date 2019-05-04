namespace Tools.Test.Csv
{
    using System;
    using System.Collections.Generic;
    using Tools.Csv;
    using Xunit;

    public class ListPivotTest
    {
        [Fact]
        public void ThrowsOnNullListAccessor()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ListPivot<string, string>("test", null, x => x, x => x));
        }

        [Fact]
        public void ThrowsOnNullColumnNameAccessor()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ListPivot<string, string>("test", x => new List<string>(), null, x => x));
        }

        [Fact]
        public void ThrowsOnNullColumnValueAccessor()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ListPivot<string, string>("test", x => new List<string>(), x => x, null));
        }

        [Fact]
        public void SetsPropertyGetters()
        {
            Func<string, List<string>> listAccessor = x => new List<string>();
            Func<string, string> columnNameAccessor = x => x;
            Func<string, string> columnValueAccessor = x => x;

            var pivot = new ListPivot<string, string>(
                "test",
                listAccessor,
                columnNameAccessor,
                columnValueAccessor);
            Assert.Equal("test", pivot.PropertyName);
            Assert.True(listAccessor.Equals(pivot.ListAccess));
            Assert.True(columnNameAccessor.Equals(pivot.ColumnNameAccess));
            Assert.True(columnValueAccessor.Equals(pivot.ColumnValueAccess));
        }
    }
}