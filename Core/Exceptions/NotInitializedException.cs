using System;

namespace Core.Exceptions
{
    /// <summary> A custom exception to catch when an instance is not initialized properly. </summary>
    public class NotInitializedException : Exception
    {
        public NotInitializedException()
        {
        }

        public NotInitializedException(string message)
            : base(message)
        {
        }

        public NotInitializedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}