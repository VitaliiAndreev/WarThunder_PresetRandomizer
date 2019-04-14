using System;

namespace Core.UnpackingToolsIntegration.Exceptions
{
    /// <summary> A custom exception to catch when the output directory for file unpacking is not found. </summary>
    class OutputDirectoryNotFoundException : Exception
    {
        public OutputDirectoryNotFoundException()
        {

        }

        public OutputDirectoryNotFoundException(string message)
            : base(message)
        {

        }

        public OutputDirectoryNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
