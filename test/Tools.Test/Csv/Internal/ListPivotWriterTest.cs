namespace Tools.Test.Csv.Internal
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using Tools.Csv;
    using Tools.Csv.Internal;
    using Xunit;

    public class ListPivotWriterTest
    {
        [Fact]
        public void ThrowsOnNullItemsTest()
        {
            var pivot = new ListPivot<string, string>(
                "test",
                x => new List<string>(),
                x => x,
                x => x);
            Assert.Throws<ArgumentNullException>(
                () => ListPivotWriter<string, string>.GetWriter(
                    null,
                    pivot));
        }

        [Fact]
        public void ThrowsOnNullPivotTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => ListPivotWriter<string, string>.GetWriter(
                    new List<string>(),
                    null));
        }

        [Fact]
        public void GetHeaderReturnsWithHeaderNoRepeatsTest()
        {
            var containerItems = new List<TestItemContainer>
            {
                new TestItemContainer
                {
                    Items = new List<TestItem>
                    {
                        new TestItem { Field1 = "field1" },
                        new TestItem { Field1 = "another field" },
                    }
                },
                new TestItemContainer
                {
                    Items = new List<TestItem>
                    {
                        new TestItem { Field1 = "another field" }
                    }
                },
                new TestItemContainer
                {
                    Items = new List<TestItem>
                    {
                        new TestItem { Field1 = "field1" }
                    }
                }
            };

            var pivot = new ListPivot<TestItemContainer, TestItem>(
                "test",
                x => x.Items,
                x => x.Field1,
                x => x.Field2);

            var writer = ListPivotWriter<TestItemContainer, TestItem>.GetWriter(
                containerItems,
                pivot);

            var header = writer.GetHeader();

            Assert.Equal("field1,another field", header);
        }

        [Fact]
        public void GetHeaderReturnsWithHeaderWithNullsTest()
        {
            var containerItem = new TestItemContainer
            {
                Items = new List<TestItem>
                {
                    new TestItem { Field1 = "field1" },
                    new TestItem { Field1 = "another field" },
                    new TestItem { Field1 = null }
                }
            };

            var pivot = new ListPivot<TestItemContainer, TestItem>(
                "test",
                x => x.Items,
                x => x.Field1,
                x => x.Field2);

            var writer = ListPivotWriter<TestItemContainer, TestItem>.GetWriter(
                new List<TestItemContainer> { containerItem },
                pivot);

            var header = writer.GetHeader();

            Assert.Equal("field1,another field,", header);
        }

        [Fact]
        public void GetLineReturnsLineWithMissingValuesTest()
        {
            var containerItems = new List<TestItemContainer>
            {
                new TestItemContainer
                {
                    Items = new List<TestItem>
                    {
                        new TestItem { Field1 = "field1", Field2 = "field2" },
                        new TestItem { Field1 = "another field", Field2 = "field2" },
                    }
                },
                new TestItemContainer
                {
                    Items = new List<TestItem>
                    {
                        new TestItem { Field1 = "another field", Field2 = "field2" },
                    }
                }
            };

            var pivot = new ListPivot<TestItemContainer, TestItem>(
                "test",
                x => x.Items,
                x => x.Field1,
                x => x.Field2);

            var writer = ListPivotWriter<TestItemContainer, TestItem>.GetWriter(
                containerItems,
                pivot);

            var line = writer.GetLine(containerItems[1]);

            Assert.Equal(",field2", line);
        }

        [Fact]
        public void GetLineReturnsLineInCorrectOrderTest()
        {
            var containerItems = new List<TestItemContainer>
            {
                new TestItemContainer
                {
                    Items = new List<TestItem>
                    {
                        new TestItem { Field1 = "headervalue1", Field2 = "field2" },
                        new TestItem { Field1 = "headervalue2", Field2 = "field2" },
                    }
                },
                new TestItemContainer
                {
                    Items = new List<TestItem>
                    {
                        new TestItem { Field1 = "headervalue2", Field2 = "value1" },
                        new TestItem { Field1 = "headervalue1", Field2 = "value2" },
                    }
                }
            };

            var pivot = new ListPivot<TestItemContainer, TestItem>(
                "test",
                x => x.Items,
                x => x.Field1,
                x => x.Field2);

            var writer = ListPivotWriter<TestItemContainer, TestItem>.GetWriter(
                containerItems,
                pivot);

            var line = writer.GetLine(containerItems[1]);

            Assert.Equal("value2,value1", line);
        }

        [Fact]
        public void GetLineThrowsWithMultipleIdenticalHeaderValuesTest()
        {
            var containerItem = new TestItemContainer
            {
                Items = new List<TestItem>
                {
                    new TestItem { Field1 = "field1" },
                    new TestItem { Field1 = "field1" }
                }
            };

            var pivot = new ListPivot<TestItemContainer, TestItem>(
                "test",
                x => x.Items,
                x => x.Field1,
                x => x.Field2);

            var writer = ListPivotWriter<TestItemContainer, TestItem>.GetWriter(
                new List<TestItemContainer> { containerItem },
                pivot);

            var ex = Assert.Throws<InvalidOperationException>(
                () => writer.GetLine(containerItem));

            Assert.Equal("cannot have two identical column names", ex.Message);
        }

        private class TestItemContainer
        {
            public IList<TestItem> Items { get; set; }
        }

        private class TestItem
        {
            public string Field1 { get; set; }

            public string Field2 { get; set; }
        }
    }
}