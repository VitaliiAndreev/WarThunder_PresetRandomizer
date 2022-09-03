using System;

namespace Core
{
    public static class ExceptionExtensions
    {
        public static bool ThrowOrReturnFalse(this Exception exception, bool shouldThrow)
            => shouldThrow ? throw exception : false;
    }
}