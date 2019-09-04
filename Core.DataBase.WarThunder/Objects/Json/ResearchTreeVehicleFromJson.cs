using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary> A mapping entity used for automated deserialization of JSON data before passing it on into persistent objects. </summary>
    public class ResearchTreeVehicleFromJson : DeserializedFromJson
    {
        [JsonProperty("rank", Required = Required.Always)]
        public int Rank { get; set; }

        /// <summary> 1-based coordinates of the research tree grid cell this vehicle occupies with its <see cref="Rank"/>. </summary>
        [JsonProperty("rankPosXY")]
        public List<int> PresetCellCoordinatesWithinRank { get; set; }

        /// <summary> 1-based coordinates of the research tree grid cell this vehicle occupies with its <see cref="Rank"/>. </summary>
        public List<int> CellCoordinatesWithinRank { get; set; }

        [JsonProperty("reqAir")]
        public string RequiredVehicleGaijinId { get; set; }

        /// <summary> A 0-based index of the vehicle in its research tree folder. </summary>
        public int? FolderIndex { get; set; }

        [JsonProperty("gift")]
        public string CategoryOfHiddenVehicles { get; set; }

        [JsonProperty("showOnlyWhenBought")]
        public bool? ShowOnlyWhenBought { get; set; }

        [JsonProperty("showByPlatform")]
        public string PlatformGaijinIdVehicleIsRestrictedTo { get; set; }

        [JsonProperty("hideByPlatform")]
        public string PlatformGaijinIdVehicleIsHiddenOn { get; set; }

        [JsonProperty("hideFeature")]
        public string HideCondition { get; set; }

        [JsonProperty("marketplaceItemdefId")]
        public long? MarketplaceId { get; set; }
    }
}