namespace Tools
{
    using System;

    public static class Guard
    {
        /// <summary>
        ///     Throws an <see cref="ArgumentNullException" />
        ///     if <paramref name="input"/> is null.
        /// </summary>
        /// <param name="input">Object to check for null.</param>
        /// <param name="parameterName">Name of object.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull(object input, string parameterName)
        {
            if(input == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentNullException" />
        ///     if <see cref="input" /> is null or empty.
        /// </summary>
        /// <param name="input">String to check for null or whitespace.</param>
        /// <param name="parameterName">Name of string.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNullOrWhiteSpace(string input, string parameterName)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}