using System.Collections.Generic;

namespace Core.Json.Helpers.Interfaces
{
    /// <summary> Provide methods to work with JSON data. </summary>
    public interface IJsonHelper
    {
        #region Methods: Deserialization

        /// <summary> Deserializes JSON data and creates an object instance from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonData"> The JSON data to deserialize. </param>
        /// <returns></returns>
        T DeserializeObject<T>(string jsonData);

        /// <summary> Deserializes JSON data and creates a collection of object instances from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonData"> The JSON data to deserialize. </param>
        /// <returns> A collection of object instances. </returns>
        IDictionary<string, T> DeserializeDictionary<T>(string jsonData);

        #endregion Methods: Deserialization
    }
}
