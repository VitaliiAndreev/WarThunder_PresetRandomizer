using System;

namespace Core.Json.Exceptions
{
    /// <summary> A custom exception to catch when there are errors in standardization of JSON objects. </summary>
    public class JsonStandardizationException : Exception
    {
        public JsonStandardizationException()
        {
        }

        public JsonStandardizationException(string message)
            : base(message)
        {
        }

        public JsonStandardizationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
