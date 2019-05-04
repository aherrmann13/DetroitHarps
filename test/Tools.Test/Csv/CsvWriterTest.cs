namespace Tools.Test.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Moq;
    using Tools.Csv;
    using Xunit;

    public class CsvWriterTest
    {
        private readonly CsvWriter _csvWriter;
        private readonly Mock<ICsvFormatter> _csvFormatterMock;

        public CsvWriterTest()
        {
            _csvFormatterMock = new Mock<ICsvFormatter>();
            _csvWriter = new CsvWriter(_csvFormatterMock.Object);
        }

        private string GuidString => Guid.NewGuid().ToString();

        [Fact]
        public void WritesStringAsUTF8ByteArrayTest()
        {
            var csv = GuidString;
            var collection = new List<string>();

            // using string for the generic type
            _csvFormatterMock
                .Setup(x => x.Format(It.IsAny<ICollection<string>>()))
                .Returns(csv);

            var returned = _csvWriter.GetAsCsv(collection);

            Assert.Equal(Encoding.UTF8.GetBytes(csv), returned);
            _csvFormatterMock.Verify(x =>
                x.Format(It.Is<ICollection<string>>(y => y.Equals(collection))));
        }

        [Fact]
        public void WritesStringAsChangedEncodingByteArrayTest()
        {
            var csv = GuidString;
            var collection = new List<string>();
            var encoding = Encoding.ASCII;

            // using string for the generic type
            _csvFormatterMock
                .Setup(x => x.Format(It.IsAny<ICollection<string>>()))
                .Returns(csv);

            _csvWriter.Encoding = encoding;
            var returned = _csvWriter.GetAsCsv(collection);

            Assert.Equal(encoding.GetBytes(csv), returned);
            _csvFormatterMock.Verify(x =>
                x.Format(It.Is<ICollection<string>>(y => y.Equals(collection))));
        }

        [Fact]
        public void PassesPivotToCsvFormatterWhenUsedTest()
        {
            var csv = GuidString;
            var collection = new List<string>();
            var pivot = new ListPivot<string, string>(
                "test",
                x => new List<string>(),
                x => x,
                x => x);

            // using string for the generic type
            _csvFormatterMock
                .Setup(x =>
                    x.Format(
                        It.IsAny<ICollection<string>>(),
                    It.IsAny<ListPivot<string, string>>()))
                .Returns(csv);

            var returned = _csvWriter.GetAsCsv(collection, pivot);

            Assert.Equal(Encoding.UTF8.GetBytes(csv), returned);
            _csvFormatterMock.Verify(x =>
                x.Format(
                    It.Is<ICollection<string>>(y => y.Equals(collection)),
                    It.Is<ListPivot<string, string>>(y => y.Equals(pivot))));
        }
    }
}