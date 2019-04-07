using System;

namespace Core.WarThunderUnpackingToolsIntegration.Exceptions
{
    /// <summary> A custom exception to catch when there are errors in parsing .yup files. </summary>
    public class YupFileParsingException : Exception
    {
        public YupFileParsingException()
        {

        }

        public YupFileParsingException(string message)
            : base(message)
        {

        }

        public YupFileParsingException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
