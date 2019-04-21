using System;

namespace Core.Json.Exceptions
{
    /// <summary> A custom exception to catch when there are errors in deserialization of JSON files. </summary>
    public class JsonDeserializationException : Exception
    {
        public JsonDeserializationException()
        {
        }

        public JsonDeserializationException(string message)
            : base(message)
        {
        }

        public JsonDeserializationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
