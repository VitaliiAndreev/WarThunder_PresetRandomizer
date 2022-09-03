using System;

namespace Core
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