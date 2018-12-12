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
            if (input == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        ///     Throws an <see cref="{T}" />
        ///     if <paramref name="input"/> is null.
        /// </summary>
        /// <remarks>
        ///     Useful if you want to rethrow exception, removes
        ///     the boilerplate of try catch to rethrow something
        ///     new, allows you to provide a lambda to create a
        ///     new exception with the old one.
        /// </remarks>
        /// <param name="input">Object to check for null.</param>
        /// <param name="parameterName">Name of object.</param>
        /// <param name="exceptionGenerator">
        ///     Function that takes a string message and outputs
        ///     a new exception to throw.
        /// </param>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="{T}"></exception>
        public static void NotNull<T>(
            object input,
            string parameterName,
            Func<ArgumentNullException, T> exceptionGenerator)
            where T : Exception
        {
            Guard.NotNull(exceptionGenerator, nameof(exceptionGenerator));

            try
            {
                Guard.NotNull(input, parameterName);
            }
            catch (ArgumentNullException e)
            {
                throw exceptionGenerator(e);
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
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}