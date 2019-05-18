using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Enumerations;
using Core.Helpers.Logger.Interfaces;
using Core.Json.Enumerations.Logger;
using Core.Json.Exceptions;
using Core.Json.Helpers.Interfaces;
using System;

namespace Core.Json.Helpers
{
    /// <summary> Provide methods to work with JSON data. </summary>
    public class JsonHelper : LoggerFluency, IJsonHelper
    {
        #region Constructors

        /// <summary> Creates a new JSON helper. </summary>
        /// <param name="logger"> An instance of a logger. </param>
        public JsonHelper(IConfiguredLogger logger)
            : base(logger, ECoreJsonLogCategory.JsonHelper)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(ECoreJsonLogCategory.JsonHelper));
        }

        #endregion Constructors

        /// <summary> Throws a <see cref="JsonDeserializationException"/> if specified JSON text is not considered valid. </summary>
        /// <param name="jsonText"> JSON text to check. </param>
        protected void ThrowIfJsonTextIsInvalid(string jsonText)
        {
            if (jsonText.IsNullOrWhiteSpaceFluently())
                throw new JsonDeserializationException(ECoreJsonLogMessage.ErrorJsonStringEmpty);
        }

        /// <summary> Throws the specified exception after logging it as an error. Note that the compiler does not see throwing in this method from where it is being called. </summary>
        /// <param name="exception"> The exception to throw. </param>
        protected void LogAndRethrow(Exception exception) =>
            LogErrorAndThrow(ECoreJsonLogMessage.ErrorDeserializingJsonText, exception);
    }
}