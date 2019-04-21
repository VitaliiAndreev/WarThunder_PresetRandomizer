using Core.DataBase.WarThunder.Objects.Json;
using Core.Json.Helpers.Interfaces;
using System.Collections.Generic;

namespace Core.Json.WarThunder.Helpers.Interfaces
{
    /// <summary> Provide methods to work with JSON data specific to War Thunder. </summary>
    public interface IJsonHelperWarThunder : IJsonHelper
    {
        /// <summary> Deserializes JSON data and creates a collection of vehicle instances from it. </summary>
        /// <param name="jsonData"> The JSON data to deserialize. </param>
        /// <returns></returns>
        Dictionary<string, VehicleDeserializedFromJson> DeserializeVehicleList(string jsonData);
    }
}
