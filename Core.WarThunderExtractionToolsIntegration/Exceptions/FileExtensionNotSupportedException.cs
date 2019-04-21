using System;

namespace Core.UnpackingToolsIntegration.Exceptions
{
    /// <summary> A custom exception to catch when there are errors in selecting appropriate unpackers for given file extensions. </summary>
    class FileExtensionNotSupportedException : Exception
    {
        public FileExtensionNotSupportedException()
        {
        }

        public FileExtensionNotSupportedException(string message)
            : base(message)
        {
        }

        public FileExtensionNotSupportedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
