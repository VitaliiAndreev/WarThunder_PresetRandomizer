using System;

namespace Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static bool ThrowOrReturnFalse(this Exception exception, bool @throw)
        {
            if (@throw)
                throw exception;
            else
                return false;
        }
    }
}