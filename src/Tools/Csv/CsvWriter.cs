namespace Tools.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Tools;
    using Tools.Csv;

    public class CsvWriter : ICsvWriter
    {
        private readonly ICsvFormatter _formatter;

        public CsvWriter(ICsvFormatter formatter)
        {
            Guard.NotNull(formatter, nameof(formatter));

            _formatter = formatter;
        }

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public byte[] GetAsCsv<T>(ICollection<T> collection)
        {
            Guard.NotNull(collection, nameof(collection));
            var csvText = _formatter.Format(collection);
            return Encoding.GetBytes(csvText);
        }
    }
}