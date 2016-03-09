using System;
using System.Linq;

namespace HttpRequestComposer.HttpManager
{
    public static class ExceptionHelpers
    {
        public static void ThrowInnerException(this Exception ex)
        {
            if (ex.InnerException == null)
                throw ex;

            ex.ThrowInnerException();
        }

        public static string InnerExceptionMessage(this Exception ex)
        {
            if (ex.InnerException == null)
                return ex.Message;

            return ex.InnerExceptionMessage();
        }
    }
}