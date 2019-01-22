namespace Tools.Csv
{
    using System.Collections.Generic;

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
        string Format<T>(ICollection<T> items);

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
        string Format<T>(ICollection<T> items, IList<string> properties);
    }
}