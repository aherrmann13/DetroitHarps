namespace DetroitHarps.Business.Constants
{
    using System;
    using DetroitHarps.Business.Exception;

    public static class Constants
    {
        public static Func<ArgumentNullException, BusinessException> NullExceptionGenerator =>
            e => new BusinessException(message: e.Message, inner: e);
    }
}