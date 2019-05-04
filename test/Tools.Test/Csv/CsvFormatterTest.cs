namespace Tools.Test.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Tools.Csv;
    using Xunit;

    public class CsvFormatterTest
    {
        private const string Delimiter = ",";
        private const string CellIndicator = "\"";
        private const string EscapedCellIndicator = "\"\"";

        private readonly CsvFormatter _formatter;

        public CsvFormatterTest()
        {
            _formatter = new CsvFormatter();
        }

        private static string GuidString => Guid.NewGuid().ToString();

        [Fact]
        public void FormatsAllPropertiesInTypeTest()
        {
            var items = new List<TestObject>
            {
                new TestObject { StringField = GuidString, IntField = 2 },
                new TestObject { StringField = GuidString, IntField = 4 },
            };

            var csvString = _formatter.Format(items);
            var expectedString =
                $"{nameof(TestObject.StringField)},{nameof(TestObject.IntField)}{Environment.NewLine}" +
                $"{items[0].StringField},{items[0].IntField}{Environment.NewLine}" +
                $"{items[1].StringField},{items[1].IntField}{Environment.NewLine}";
            Assert.Equal(expectedString, csvString);
        }

        [Fact]
        public void FormatsAllPropertiesGivenTest()
        {
            var items = new List<TestObject>
            {
                new TestObject { StringField = GuidString, IntField = 2 },
                new TestObject { StringField = GuidString, IntField = 4 },
            };

            var propertyNames = new List<string>
            {
                nameof(TestObject.StringField),
                nameof(TestObject.IntField)
            };

            var csvString = _formatter.Format(items, propertyNames);
            var expectedString =
                $"{nameof(TestObject.StringField)},{nameof(TestObject.IntField)}{Environment.NewLine}" +
                $"{items[0].StringField},{items[0].IntField}{Environment.NewLine}" +
                $"{items[1].StringField},{items[1].IntField}{Environment.NewLine}";
            Assert.Equal(expectedString, csvString);
        }

        [Fact]
        public void FormatsSomePropertiesGivenTest()
        {
            var items = new List<TestObject>
            {
                new TestObject { StringField = GuidString, IntField = 2 },
                new TestObject { StringField = GuidString, IntField = 4 },
            };

            var propertyNames = new List<string> { nameof(TestObject.IntField) };

            var csvString = _formatter.Format(items, propertyNames);
            var expectedString =
                $"{nameof(TestObject.IntField)}{Environment.NewLine}" +
                $"{items[0].IntField}{Environment.NewLine}" +
                $"{items[1].IntField}{Environment.NewLine}";
            Assert.Equal(expectedString, csvString);
        }

        [Fact]
        public void FormatsAnonTypesTest()
        {
            var items = new List<TestObject>
            {
                new TestObject { StringField = GuidString, IntField = 2 },
                new TestObject { StringField = GuidString, IntField = 4 },
            }.Select(x => new { AnonProp = x.StringField }).ToList();

            var propertyNames = new List<string> { "AnonProp" };

            var csvString = _formatter.Format(items, propertyNames);
            var expectedString =
                $"{propertyNames[0]}{Environment.NewLine}" +
                $"{items[0].AnonProp}{Environment.NewLine}" +
                $"{items[1].AnonProp}{Environment.NewLine}";
            Assert.Equal(expectedString, csvString);
        }

        [Fact]
        public void FormatsTypeWithComplexTypeTest()
        {
            var items = new List<ComplexTestObject>
            {
                new ComplexTestObject
                {
                    StringField = GuidString,
                    IntField = 2,
                    ObjectField = new TestObject { StringField = GuidString, IntField = 4 }
                },
                new ComplexTestObject
                {
                    StringField = GuidString,
                    IntField = 3,
                    ObjectField = new TestObject { StringField = GuidString, IntField = 5 }
                }
            };

            var propertyNames = new List<string>
            {
                nameof(ComplexTestObject.ObjectField),
                nameof(ComplexTestObject.StringField),
                nameof(ComplexTestObject.IntField)
            };

            var csvString = _formatter.Format(items, propertyNames);
            var expectedString =
                $"{nameof(ComplexTestObject.ObjectField)},{nameof(ComplexTestObject.StringField)},{nameof(ComplexTestObject.IntField)}{Environment.NewLine}" +
                $"{Serialize(items[0].ObjectField)},{items[0].StringField},{items[0].IntField}{Environment.NewLine}" +
                $"{Serialize(items[1].ObjectField)},{items[1].StringField},{items[1].IntField}{Environment.NewLine}";
            Assert.Equal(expectedString, csvString);
        }

        [Fact]
        public void FormatsAnonTypeWithComplexTypeTest()
        {
            var num = 0;
            var items = new List<int> { 0, 1 }
                .Select(x => new
                {
                    Field1 = x,
                    Field2 = new
                    {
                        Inner1 = num++,
                        Inner2 = num++
                    }
                }).ToList();

            var propertyNames = new List<string>
            {
                "Field1",
                "Field2"
            };

            var csvString = _formatter.Format(items, propertyNames);
            var expectedString =
                $"{propertyNames[0]},{propertyNames[1]}{Environment.NewLine}" +
                $"{items[0].Field1},{Serialize(items[0].Field2)}{Environment.NewLine}" +
                $"{items[1].Field1},{Serialize(items[1].Field2)}{Environment.NewLine}";
            Assert.Equal(expectedString, csvString);
        }

        [Fact]
        public void FormatsTypeWithListTest()
        {
            var items = new List<TestObjectWithList>
            {
                new TestObjectWithList
                {
                    StringField = GuidString,
                    IntField = 2,
                    ListField = new List<TestObject>
                    {
                        new TestObject { StringField = GuidString, IntField = 1 },
                        new TestObject { StringField = GuidString, IntField = 2 }
                    }
                },
                new TestObjectWithList
                {
                    StringField = GuidString,
                    IntField = 3,
                    ListField = new List<TestObject>
                    {
                        new TestObject { StringField = GuidString, IntField = 4 },
                        new TestObject { StringField = GuidString, IntField = 5 }
                    }
                }
            };

            var propertyNames = new List<string>
            {
                nameof(TestObjectWithList.ListField),
                nameof(TestObjectWithList.StringField),
                nameof(TestObjectWithList.IntField)
            };

            var csvString = _formatter.Format(items, propertyNames);
            var expectedString =
                $"{nameof(TestObjectWithList.ListField)},{nameof(TestObjectWithList.StringField)},{nameof(TestObjectWithList.IntField)}{Environment.NewLine}" +
                $"{Serialize(items[0].ListField)},{items[0].StringField},{items[0].IntField}{Environment.NewLine}" +
                $"{Serialize(items[1].ListField)},{items[1].StringField},{items[1].IntField}{Environment.NewLine}";
            Assert.Equal(expectedString, csvString);
        }

        [Fact]
        public void InsertsNullPropertiesAsBlankTest()
        {
            var items = new List<TestObject>
            {
                new TestObject { StringField = null, IntField = 2 },
                new TestObject { StringField = GuidString, IntField = 4 },
            };

            var propertyNames = new List<string>
            {
                nameof(TestObject.StringField),
                nameof(TestObject.IntField)
            };

            var csvString = _formatter.Format(items, propertyNames);
            var expectedString =
                $"{nameof(TestObject.StringField)},{nameof(TestObject.IntField)}{Environment.NewLine}" +
                $"{string.Empty},{items[0].IntField}{Environment.NewLine}" +
                $"{items[1].StringField},{items[1].IntField}{Environment.NewLine}";
            Assert.Equal(expectedString, csvString);
        }

        [Fact]
        public void CorrectlyFormatsStringWithCommaTest()
        {
            var stringWithComma = $"test{Delimiter}test";
            var items = new List<TestObject>
            {
                new TestObject { StringField = stringWithComma }
            };

            var propertyNames = new List<string>
            {
                nameof(TestObject.StringField)
            };

            var csvString = _formatter.Format(items, propertyNames);

            var expectedString =
                $"{nameof(TestObject.StringField)}{Environment.NewLine}" +
                $"{CellIndicator}{items[0].StringField}{CellIndicator}{Environment.NewLine}";
            Assert.Equal(expectedString, csvString);
        }

        [Fact]
        public void CorrectlyFormatsWithListPivotAndPropertyListTest()
        {
            var items = new List<TestObjectWithList>
            {
                new TestObjectWithList
                {
                    StringField = "stringField",
                    IntField = 1,
                    ListField = new List<TestObject>
                    {
                        new TestObject
                        {
                            StringField = "testheader",
                            IntField = 1
                        }
                    }
                }
            };

            var pivot = new ListPivot<TestObjectWithList, TestObject>(
                "test",
                x => x.ListField,
                x => x.StringField,
                x => x.IntField.ToString());

            var propertyNames = new List<string>
            {
                nameof(TestObject.StringField)
            };

            var csvString = _formatter.Format(items, propertyNames, pivot);

            var expectedString =
                $"{nameof(TestObject.StringField)},{items[0].ListField[0].StringField}{Environment.NewLine}" +
                $"{items[0].StringField},{items[0].ListField[0].IntField}{Environment.NewLine}";
            Assert.Equal(expectedString, csvString);
        }

        [Fact]
        public void CorrectlyFormatsWithListPivotTest()
        {
            var items = new List<TestObjectWithList>
            {
                new TestObjectWithList
                {
                    StringField = "stringField",
                    IntField = 1,
                    ListField = new List<TestObject>
                    {
                        new TestObject
                        {
                            StringField = "testheader",
                            IntField = 1
                        }
                    }
                }
            };

            var pivot = new ListPivot<TestObjectWithList, TestObject>(
                nameof(TestObjectWithList.ListField),
                x => x.ListField,
                x => x.StringField,
                x => x.IntField.ToString());

            var csvString = _formatter.Format(items, pivot);

            var expectedString =
                $"{nameof(TestObject.StringField)},{nameof(TestObject.IntField)},{items[0].ListField[0].StringField}{Environment.NewLine}" +
                $"{items[0].StringField},{items[0].IntField},{items[0].ListField[0].IntField}{Environment.NewLine}";
            Assert.Equal(expectedString, csvString);
        }

        [Fact]
        public void ThrowsOnNonExistantPropertyNameTest()
        {
            var items = new List<TestObject>
            {
                new TestObject { StringField = GuidString, IntField = 2 },
                new TestObject { StringField = GuidString, IntField = 4 },
            };

            var propertyNames = new List<string>
            {
                GuidString,
                nameof(TestObject.IntField)
            };

            var exception = Assert.Throws<InvalidOperationException>(() =>
                _formatter.Format(items, propertyNames));
            var expectedMessage =
                $"{nameof(TestObject)} does not contain property {propertyNames[0]}";
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void ThrowsOnNullItemListTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _formatter.Format((ICollection<string>)null, new List<string>()));
        }

        [Fact]
        public void ThrowsOnNullPropertyListTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _formatter.Format(new List<string>(), null));
        }

        private string Serialize(object obj)
        {
            var data = JsonConvert.SerializeObject(obj);

            if (data.Contains(CellIndicator))
            {
                data = data.Replace(CellIndicator, EscapedCellIndicator);
                data = $"{CellIndicator}{data}{CellIndicator}";
            }
            else if (data.Contains(Delimiter))
            {
                data = $"{CellIndicator}{data}{CellIndicator}";
            }

            return data;
        }

        private class TestObject
        {
            public string StringField { get; set; }

            public int IntField { get; set; }
        }

        private class ComplexTestObject
        {
            public string StringField { get; set; }

            public int IntField { get; set; }

            public TestObject ObjectField { get; set; }
        }

        private class TestObjectWithList
        {
            public string StringField { get; set; }

            public int IntField { get; set; }

            public IList<TestObject> ListField { get; set; }
        }
    }
}