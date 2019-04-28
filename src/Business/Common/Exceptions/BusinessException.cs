namespace DetroitHarps.Business.Common.Exceptions
{
    using System;

    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message)
        {
        }

        public BusinessException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}