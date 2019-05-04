namespace Tools.Csv.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ListPivotWriter<TItem, TPivotItem>
        where TItem : class
        where TPivotItem : class
    {
        private const string Delimiter = ",";

        private readonly ListPivot<TItem, TPivotItem> _pivot;
        private IList<string> _columnCache;

        private ListPivotWriter(
            ListPivot<TItem, TPivotItem> pivot)
        {
            Guard.NotNull(pivot, nameof(pivot));

            _pivot = pivot;
        }

        public static ListPivotWriter<TItem, TPivotItem> GetWriter(
            ICollection<TItem> items,
            ListPivot<TItem, TPivotItem> pivot)
        {
            var writer = new ListPivotWriter<TItem, TPivotItem>(pivot);

            writer.CacheColumnNames(items);

            return writer;
        }

        public string GetHeader() => string.Join(Delimiter, _columnCache);

        public string GetLine(TItem item)
        {
            var pivotItemDict = BuildItemDictionary(item);

            var values = _columnCache
                .Select(x => pivotItemDict.ContainsKey(x) ? pivotItemDict[x] : string.Empty);

            return string.Join(Delimiter, values);
        }

        private IDictionary<string, string> BuildItemDictionary(TItem line)
        {
            try
            {
                return _pivot
                .ListAccess(line)
                .ToDictionary(
                    key => _pivot.ColumnNameAccess(key),
                    value => _pivot.ColumnValueAccess(value));
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException(
                    "cannot have two identical column names",
                    ex);
            }
        }

        private void CacheColumnNames(ICollection<TItem> items)
        {
            _columnCache = items
                .SelectMany(_pivot.ListAccess)
                .Select(_pivot.ColumnNameAccess)
                .Distinct()
                .ToList();
        }
    }
}