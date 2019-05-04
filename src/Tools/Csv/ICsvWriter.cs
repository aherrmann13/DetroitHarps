namespace Tools.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    ///     This class encodes a given collection as a
    ///     byte array of a given format that can be
    ///     written as a csv file.
    /// </summary>
    public interface ICsvWriter
    {
        /// <summary>
        ///     Gets or sets the encoding of the byte
        ///     array returned by the
        ///     <see cref="GetAsCsv{TRow}"/> method.
        /// </summary>
        /// <value></value>
        Encoding Encoding { get; set; }

        /// <summary>
        ///     Converts the specified collection to a
        ///     comma seperated string and returns it
        ///     as a byte array that can be written to
        ///     a file.
        /// </summary>
        /// <param name="collection">
        ///     The items to print as rows.
        /// </param>
        /// <typeparam name="T">
        ///     The type of object in each row.
        /// </typeparam>
        /// <returns>
        ///     Byte array of the csv formatted collection.
        /// </returns>
        byte[] GetAsCsv<T>(ICollection<T> collection)
            where T : class;

        /// <summary>
        ///     Converts the specified collection to a
        ///     comma seperated string and returns it
        ///     as a byte array that can be written to
        ///     a file.
        /// </summary>
        /// <param name="collection">
        ///     The items to print as rows.
        /// </param>
        /// <param name="pivot">
        ///     The pivot configuration to print additional columns.
        /// </param>
        /// <typeparam name="T">
        ///     The type of object in each row.
        /// </typeparam>
        /// <typeparam name="TPivot">
        ///     The type of object to use as an additional column in each row.
        /// </typeparam>
        /// <returns>
        ///     Byte array of the csv formatted collection.
        /// </returns>
        byte[] GetAsCsv<T, TPivot>(ICollection<T> collection, ListPivot<T, TPivot> pivot)
            where T : class
            where TPivot : class;
    }
}