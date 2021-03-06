﻿using System.Collections.Generic;

namespace Core.Json.Helpers.Interfaces
{
    /// <summary> Provides methods to work with JSON data. </summary>
    public interface IJsonHelper
    {
        #region Methods: Deserialization

        /// <summary> Deserializes JSON text and creates an object instance from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonText"> The JSON text to deserialize. </param>
        /// <param name="suppressStandardization"> Whether to avoid doing any pre-processing of JSON entities. </param>
        /// <returns></returns>
        T DeserializeObject<T>(string jsonText, bool suppressStandardization = false);

        /// <summary> Deserializes JSON text and creates a collection of object instances from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonText"> The JSON text to deserialize. </param>
        /// <returns> A collection of object instances. </returns>
        IDictionary<string, T> DeserializeDictionary<T>(string jsonText);

        #endregion Methods: Deserialization
    }
}