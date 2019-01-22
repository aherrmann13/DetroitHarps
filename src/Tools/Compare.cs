namespace Tools
{
    using System;

    public static class Compare
    {
        /// <summary>
        ///     Performs a case sensitive ordinal comparison of two strings.
        /// </summary>
        /// <param name="a">The first string.</param>
        /// <param name="b">The second string.</param>
        /// <returns>
        ///     Returns <c>true</c> if the two strings are equal.
        /// </returns>
        public static bool EqualOrdinal(this string stringA, string stringB)
        {
            return string.Compare(stringA, stringB, StringComparison.Ordinal) == 0;
        }
    }
}