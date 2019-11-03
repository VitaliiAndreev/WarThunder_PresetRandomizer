using Newtonsoft.Json;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary> A mapping entity used for automated deserialization of JSON data before passing it on into persistent objects. </summary>
    public class ModificationDeserializedFromJson : DeserializedFromJsonWithOwner<VehicleDeserializedFromJsonWpCost>
    {
        [JsonProperty("costGold", Required = Required.Always)]
        public int PurchaseCostInGold { get; set; }
    }
}