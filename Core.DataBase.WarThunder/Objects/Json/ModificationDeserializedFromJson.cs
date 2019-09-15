using Newtonsoft.Json;

namespace Core.DataBase.WarThunder.Objects.Json
{
    public class ModificationDeserializedFromJson : DeserializedFromJsonWithOwner<VehicleDeserializedFromJsonWpCost>
    {
        [JsonProperty("costGold", Required = Required.Always)]
        public int PurchaseCostInGold { get; set; }
    }
}