using System;
using System.IO;

namespace Core.UnpackingToolsIntegration.Exceptions
{
    /// <summary> A custom exception to catch when the output file for file unpacking is not found. </summary>
    public class OutputFileNotFoundException : FileNotFoundException
    {
        public OutputFileNotFoundException()
        {

        }

        public OutputFileNotFoundException(string message)
            : base(message)
        {

        }

        public OutputFileNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
