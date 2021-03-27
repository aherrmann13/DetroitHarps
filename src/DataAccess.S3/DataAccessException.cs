namespace DetroitHarps.DataAccess.S3
{
    using System;

    public class DataAccessException : Exception
    {
        public DataAccessException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}