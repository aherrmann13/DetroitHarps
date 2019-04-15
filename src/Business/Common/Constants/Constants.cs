namespace DetroitHarps.Business.Common.Constants
{
    using System;
    using DetroitHarps.Business.Common.Exceptions;

    public static class Constants
    {
        public static Func<ArgumentNullException, BusinessException> NullExceptionGenerator =>
            e => new BusinessException(message: e.Message, inner: e);
    }
}