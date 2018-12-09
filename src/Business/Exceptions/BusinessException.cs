namespace DetroitHarps.Business.Exception
{
    using System;

    public class BusinessException : Exception
    {
        public BusinessException()
        {
        }

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