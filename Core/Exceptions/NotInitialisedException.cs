using System;

namespace Core
{
    public class NotInitialisedException : Exception
    {
        public NotInitialisedException()
        {
        }

        public NotInitialisedException(string message)
            : base(message)
        {
        }

        public NotInitialisedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}