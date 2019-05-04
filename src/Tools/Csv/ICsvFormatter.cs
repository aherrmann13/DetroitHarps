namespace Tools.Csv
{
    using System.Collections.Generic;

    /// <summary>
    ///     This class provides methods for formatting
    ///     a given collection of objects into a
    ///     string that can be written to a csv file.
    /// </summary>
    public interface ICsvFormatter
    {
        /// <summary>
        ///     Returns a comma delimited string of all of
        ///     the items in the string, with the propery
        ///     names as headers.
        /// </summary>
        /// <param name="items">
        ///     <see cref="ICollection{T}"/> of all of the rows for the csv.
        /// </param>
        /// <typeparam name="T">
        ///     The type of item in each row.
        /// </typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        string Format<T>(ICollection<T> items)
            where T : class;

        /// <summary>
        ///     Returns a comma delimited string of all of
        ///     the items in the string, with the propery
        ///     names as headers.
        /// </summary>
        /// <param name="items">
        ///     <see cref="ICollection{T}"/> of all of the rows for the csv.
        /// </param>
        /// <param name="properties">
        ///     Ordered <see cref="IList{string}"/> of all of the property
        ///     names to include in the order.
        /// </param>
        /// <typeparam name="T">
        ///     The type of item in each row.
        /// </typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        string Format<T>(ICollection<T> items, IList<string> properties)
            where T : class;

        /// <summary>
        ///     Returns a comma delimited string of all of
        ///     the items in the string, with the propery
        ///     names as headers along with additional pivot
        ///     configuration.
        /// </summary>
        /// <param name="items">
        ///     <see cref="ICollection{T}"/> of all of the rows for the csv.
        /// </param>
        /// <param name="pivot">
        ///     The pivot configuration to print additional columns.
        /// </param>
        /// <typeparam name="T">
        ///     The type of item in each row.
        /// </typeparam>
        /// <typeparam name="TPivot">
        ///     The type of object to use as an additional column in each row.
        /// </typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        string Format<T, TPivot>(ICollection<T> items, ListPivot<T, TPivot> pivot)
            where T : class
            where TPivot : class;

        /// <summary>
        ///     Returns a comma delimited string of all of
        ///     the items in the string, with the propery
        ///     names as headers along with additional pivot
        ///     configuration.
        /// </summary>
        /// <param name="items">
        ///     <see cref="ICollection{T}"/> of all of the rows for the csv.
        /// </param>
        /// <param name="properties">
        ///     Ordered <see cref="IList{string}"/> of all of the property
        ///     names to include in the order.
        /// </param>
        /// <param name="pivot">
        ///     The pivot configuration to print additional columns.
        /// </param>
        /// <typeparam name="T">
        ///     The type of item in each row.
        /// </typeparam>
        /// <typeparam name="TPivot">
        ///     The type of object to use as an additional column in each row.
        /// </typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        string Format<T, TPivot>(
            ICollection<T> items,
            IList<string> properties,
            ListPivot<T, TPivot> pivot)
            where T : class
            where TPivot : class;
    }
}