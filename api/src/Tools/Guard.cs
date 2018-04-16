namespace Tools
{
    using System;

    public static class Guard
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if <see cref="input" /> is null.
        /// </summary>
        /// <param name="input">Object to check for null.</param>
        /// <param name="parameterName">Name of object type.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull(object input, string parameterName)
        {
            if(input == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}