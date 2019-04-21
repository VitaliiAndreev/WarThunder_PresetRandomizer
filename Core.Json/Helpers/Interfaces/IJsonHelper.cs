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
        T DeserializeObject<T>(string jsonData) where T : class, new();

        /// <summary> Deserializes JSON data and creates a collection of object instances from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonData"> The JSON data to deserialize. </param>
        /// <returns> A collection of object instances. </returns>
        Dictionary<string, T> DeserializeList<T>(string jsonData) where T : class, new();

        #endregion Methods: Deserialization
    }
}
