using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary> A mapping entity used for automated deserialization of JSON data before passing it on into persistent objects. </summary>
    [JsonObject()]
    public class WeaponDeserializedFromJson : DeserializedFromJsonWithOwner<VehicleDeserializedFromJsonWpCost>
    {
        [JsonProperty("value", Required = Required.Always)]
        public int PurchaseCostInSilver { get; set; }
    
        [JsonProperty("validIfBought")]
        public bool? RequiresModificationPurchase { get; set; }
    
        [JsonProperty("reqModification")]
        public List<string> ModificationsRequired { get; set; }

        [JsonProperty("maxCount")]
        public int? MaximumStockpileAmount { get; set; }

        [JsonProperty("mass_per_sec")]
        public decimal? MassPerSecond { get; set; }

        [JsonProperty("isAntiTankWeap")]
        public bool? IsAntiTank { get; set; }

        [JsonProperty("isATGM")]
        public bool? IsAntiTankGuidedMissile { get; set; }

        [JsonProperty("hasDepthCharge")]
        public bool? IsDepthCharge { get; set; }

        [JsonProperty("spentOnDeath")]
        public bool? IsDisabledWhenDestroyed { get; set; }

        [JsonProperty("rocketDistanceFuse")]
        public bool? HasDistanceFuse { get; set; }

        [JsonProperty("image", Required = Required.Always)]
        public string Image { get; set; }

        [JsonProperty("weaponmask")]
        public string WeaponMask { get; set; }
    }
}