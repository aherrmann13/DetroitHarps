namespace Tools.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Newtonsoft.Json;
    using Tools.Csv.Internal;

    public class CsvFormatter : ICsvFormatter
    {
        private const string Delimiter = ",";
        private const string CellIndicator = "\"";
        private const string EscapedCellIndicator = "\"\"";

        public string Format<T>(ICollection<T> items)
            where T : class => Format(items, BuildPropertyList(typeof(T)));

        public string Format<T>(ICollection<T> items, IList<string> properties)
            where T : class => FormatIternal<T, object>(items, properties, null);

        public string Format<T, TPivot>(ICollection<T> items, ListPivot<T, TPivot> pivot)
            where T : class
            where TPivot : class =>
                Format(
                    items,
                    BuildPropertyList(typeof(T)).Where(x => !Compare.EqualOrdinal(x, pivot.PropertyName)).ToList(),
                    pivot);

        public string Format<T, TPivot>(
            ICollection<T> items,
            IList<string> properties,
            ListPivot<T, TPivot> pivot)
            where T : class
            where TPivot : class =>
            FormatIternal(items, properties, BuildPivotWriter<T, TPivot>(items, pivot));

        private string FormatIternal<T, TPivot>(
            ICollection<T> items,
            IList<string> properties,
            ListPivotWriter<T, TPivot> writer)
            where T : class
            where TPivot : class
        {
            Guard.NotNull(items, nameof(items));
            var propertyCache = GetObjectProperties(typeof(T), properties);

            // TODO : better checking for writer
            StringBuilder sb = new StringBuilder();
            sb.Append(GetHeader(properties));
            sb.Append(writer != null ? $"{Delimiter}{writer.GetHeader()}" : string.Empty);
            sb.Append(Environment.NewLine);

            foreach (var item in items)
            {
                sb.Append(GetRow(item, propertyCache));
                sb.Append(writer != null ? $"{Delimiter}{writer.GetLine(item)}" : string.Empty);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        private static string GetHeader(IList<string> properties) =>
            string.Join(Delimiter, properties);

        private static string GetRow<T>(T item, IList<PropertyInfo> cache)
        {
            var values = cache
                .Select(x => new { Value = x.GetValue(item), CanSerialize = x.PropertyType.IsSerializable })
                .Select(x => !x.CanSerialize ? JsonConvert.SerializeObject(x.Value) : x.Value)
                .Select(x => x != null && x.GetType().Equals(typeof(string)) ? EscapeForCsv((string)x) : x);
            return string.Join(Delimiter, values);
        }

        private static IList<PropertyInfo> GetObjectProperties(
            Type type,
            IList<string> properties) =>
            properties.Select(x =>
                type.GetProperty(x) ??
                throw new InvalidOperationException($"{type.Name} does not contain property {x}"))
            .ToList();

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

        private static ListPivotWriter<T, TPivot> BuildPivotWriter<T, TPivot>(
            ICollection<T> items,
            ListPivot<T, TPivot> pivot)
        where T : class
        where TPivot : class =>
        ListPivotWriter<T, TPivot>.GetWriter(items, pivot);

        private IList<string> BuildPropertyList(Type type) =>
            type.GetProperties().Select(x => x.Name).ToList();
    }
}