using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Enumerations;
using Core.Helpers.Logger.Interfaces;
using Core.Json.Exceptions;
using Core.Json.Helpers.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Json.Helpers
{
    /// <summary> Provide methods to work with JSON data. </summary>
    public class JsonHelper : LoggerFluency, IJsonHelper
    {
        #region Constructors

        /// <summary> Creates a new JSON helper. </summary>
        /// <param name="logger"> An instance of a logger. </param>
        public JsonHelper(IConfiguredLogger logger)
            : base(logger, ECoreLogCategory.JsonHelper)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(ECoreLogCategory.JsonHelper));
        }

        #endregion Constructors
        #region Methods: Deserialization

        private void ThrowIfJsonDataIsInvalid(string jsonData)
        {
            if (jsonData.IsNullOrWhiteSpaceFluently())
                throw new JsonDeserializationException(ECoreLogMessage.JsonStringEmpty);
        }

        private void LogAndRethrow(Exception exception)
        {
            LogError(ECoreLogMessage.ErrorDeserializingJsonData, exception);
            throw exception;
        }

        /// <summary> Deserializes JSON data and creates an object instance from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonData"> The JSON data to deserialize. </param>
        /// <returns></returns>
        public T DeserializeObject<T>(string jsonData) where T : class, new()
        {
            LogDebug(ECoreLogMessage.TryingToDeserializeJsonStringIntoObject.ResetFormattingPlaceholders().FormatFluently(jsonData.Count(), new T().GetType().Name));
            var deserializedInstance = default(T);

            try
            {
                ThrowIfJsonDataIsInvalid(jsonData);

                deserializedInstance = JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch (Exception exception)
            {
                LogAndRethrow(exception);
            }

            LogDebug(ECoreLogMessage.DeserializedInstance);
            return deserializedInstance;
        }

        /// <summary> Deserializes JSON data and creates a collection of object instances from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonData"> The JSON data to deserialize. </param>
        /// <returns> A collection of object instances. </returns>
        public virtual Dictionary<string, T> DeserializeList<T>(string jsonData) where T : class, new()
        {
            LogDebug(ECoreLogMessage.TryingToDeserializeJsonStringIntoCollection.ResetFormattingPlaceholders().FormatFluently(jsonData.Count(), new T().GetType().Name));
            var deserializedInstances = default(Dictionary<string, T>);

            try
            {
                ThrowIfJsonDataIsInvalid(jsonData);

                deserializedInstances = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonData);
            }
            catch(Exception exception)
            {
                LogAndRethrow(exception);
            }

            LogDebug(ECoreLogMessage.DeserializedInstances.FormatFluently(deserializedInstances.Count()));
            return deserializedInstances;
        }

        #endregion Methods: Deserialization
    }
}
