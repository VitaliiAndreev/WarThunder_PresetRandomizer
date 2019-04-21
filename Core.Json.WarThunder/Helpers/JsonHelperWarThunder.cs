using Core.DataBase.WarThunder.Objects.Json;
using Core.Enumerations;
using Core.Helpers.Logger.Interfaces;
using Core.Json.WarThunder.Helpers.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Core.Json.Helpers
{
    /// <summary> Provide methods to work with JSON data specific to War Thunder. </summary>
    public class JsonHelperWarThunder : JsonHelper, IJsonHelperWarThunder
    {
        #region Constructors

        /// <summary> Creates a new War Thunder JSON helper. </summary>
        /// <param name="logger"> An instance of a logger. </param>
        public JsonHelperWarThunder(IConfiguredLogger logger)
            : base(logger)
        {
        }

        #endregion Constructors
        #region Methods: Deserialization

        /// <summary>
        /// Deserializes JSON data and creates a collection of vehicle instances from it.
        /// A custom implimentation is necessary because entities are not defined uniformly in "wpcost.blkx" and a singular approach is not enough to successfully deserialize the entirety of the the vehicle set.
        /// </summary>
        /// <param name="jsonData"> The JSON data to deserialize. </param>
        /// <returns></returns>
        public Dictionary<string, VehicleDeserializedFromJson> DeserializeVehicleList(string jsonData)
        {
            var entities = DeserializeList<dynamic>(jsonData);
            var vehicles = new List<VehicleDeserializedFromJson>();

            foreach (var entity in entities)
            {
                VehicleDeserializedFromJson deserialize(string jsonEntity)
                {
                    var vehicle = DeserializeObject<VehicleDeserializedFromJson>(jsonEntity);
                    vehicle.GaijinId = entity.Key;
                    return vehicle;
                }

                if (entity.Value is JObject jsonObject)
                {
                    vehicles.Add(deserialize(jsonObject.ToString()));
                }
                else if (entity.Value is JArray jsonArray)
                {
                    var rebuiltJsonObject = new JObject();

                    foreach (var fragmentedEntity in jsonArray)
                    {
                        foreach (var childToken in fragmentedEntity.Children())
                        {
                            var key = childToken.Path.Split(ECharacter.Period).Last();
                            if (!rebuiltJsonObject.Properties().Select(property => property.Name).Contains(key))
                                rebuiltJsonObject.Add(key, childToken.First());
                        }
                    }
                    vehicles.Add(deserialize(rebuiltJsonObject.ToString()));
                }
            }
            return vehicles.ToDictionary(vehicle => vehicle.GaijinId);
        }

        #endregion Methods: Deserialization
    }
}
