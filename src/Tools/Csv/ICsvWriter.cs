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
        /// <typeparam name="T">
        ///     The type of object in each row.
        /// </typeparam>
        /// <returns>
        ///     Byte array of the csv formatted collection.
        /// </returns>
        byte[] GetAsCsv<T>(ICollection<T> collection);
    }
}