namespace Tools.Csv
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Class that contains the configuration to use a list
    ///     contained within an object as additional columns
    ///     in that items row in an csv.
    /// </summary>
    /// <typeparam name="TItem">
    ///     Type of item that used as a row in a csv.
    /// </typeparam>
    /// <typeparam name="TPivotItem">
    ///     The type of item that will be used as a single column
    ///     on its containing elements row
    /// </typeparam>
    public class ListPivot<TItem, TPivotItem>
        where TItem : class
        where TPivotItem : class
    {
        /// <summary>
        ///     Config object to pivot columns in a csv
        /// </summary>
        /// <param name="propName">
        ///     Name of the column that is pivoting
        /// </param>
        /// <param name="listAccessor">
        ///     Function to access the items to display as columns
        /// </param>
        /// <param name="columnNameAccessor">
        ///     Function to access the property to use as the column header
        /// </param>
        /// <param name="columnValueAccessor">
        ///     Function to access the property to use as the row value
        /// </param>
        public ListPivot(
            string propName,
            Func<TItem, ICollection<TPivotItem>> listAccessor,
            Func<TPivotItem, string> columnNameAccessor,
            Func<TPivotItem, string> columnValueAccessor)
        {
            Guard.NotNullOrWhiteSpace(propName, nameof(propName));
            Guard.NotNull(listAccessor, nameof(listAccessor));
            Guard.NotNull(columnNameAccessor, nameof(columnNameAccessor));
            Guard.NotNull(columnValueAccessor, nameof(columnValueAccessor));

            PropertyName = propName;
            ListAccess = listAccessor;
            ColumnNameAccess = columnNameAccessor;
            ColumnValueAccess = columnValueAccessor;
        }

        /// <summary>
        ///     Gets the name of the column that is being pivoted
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        ///     Gets a function to access the list to display items as new columns
        /// </summary>
        public Func<TItem, ICollection<TPivotItem>> ListAccess { get; }

        /// <summary>
        /// Gets a function to access the property to use as the column header
        /// </summary>
        public Func<TPivotItem, string> ColumnNameAccess { get; }

        /// <summary>
        /// Gets a function to access the property to use as the row value
        /// </summary>
        public Func<TPivotItem, string> ColumnValueAccess { get; }
    }
}