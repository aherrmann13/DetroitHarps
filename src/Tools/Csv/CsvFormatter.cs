namespace Tools.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Newtonsoft.Json;

    public class CsvFormatter : ICsvFormatter
    {
        private const string Delimiter = ",";
        private const string CellIndicator = "\"";
        private const string EscapedCellIndicator = "\"\"";

        public string Format<T>(ICollection<T> items)
        {
            var properties = typeof(T)
                .GetProperties()
                .Select(x => x.Name)
                .ToList();
            return Format(items, properties);
        }

        public string Format<T>(ICollection<T> items, IList<string> properties)
        {
            Guard.NotNull(items, nameof(items));
            var propertyCache = GetObjectProperties(typeof(T), properties);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetHeader(properties));

            foreach (var item in items)
            {
                sb.AppendLine(GetRow(item, propertyCache));
            }

            return sb.ToString();
        }

        private static string GetHeader(IList<string> properties)
        {
            return string.Join(Delimiter, properties);
        }

        private static string GetRow<T>(T item, IList<PropertyInfo> cache)
        {
            // TODO : clean up
            var values = cache
                .Select(x => new { Value = x.GetValue(item), CanSerialize = x.PropertyType.IsSerializable })
                .Select(x => !x.CanSerialize ? JsonConvert.SerializeObject(x.Value) : x.Value)
                .Select(x => x != null && x.GetType().Equals(typeof(string)) ? EscapeForCsv((string)x) : x);
            return string.Join(Delimiter, values);
        }

        private static IList<PropertyInfo> GetObjectProperties(
            Type type, IList<string> properties)
        {
            return properties.Select(x =>
                type.GetProperty(x) ??
                throw new InvalidOperationException($"{type.Name} does not contain property {x}"))
            .ToList();
        }

        private static string EscapeForCsv(string data)
        {
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
    }
}