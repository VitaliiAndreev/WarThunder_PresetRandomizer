using System;

namespace Core.UnpackingToolsIntegration
{
    public static class FuncExtensions
    {
        public static bool TryExecuting<T>(this Func<T> function, out T result, out Exception caughtException) where T : class
        {
            result = null;
            caughtException = null;

            if (function is null)
            {
                caughtException = new ArgumentException("No function is given.");
                return false;
            }

            try
            {
                result = function.Invoke();
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            return !(result is null);
        }
    }
}