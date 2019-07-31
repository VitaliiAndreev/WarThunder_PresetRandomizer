using System;

namespace Core.Localization.Exceptions
{
    /// <summary> A custom exception to catch when there is no language that matches the name. </summary>
    public class LanguageNotRecognizedException : Exception
    {
        public LanguageNotRecognizedException()
        {
        }

        public LanguageNotRecognizedException(string message)
            : base(message)
        {
        }

        public LanguageNotRecognizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}