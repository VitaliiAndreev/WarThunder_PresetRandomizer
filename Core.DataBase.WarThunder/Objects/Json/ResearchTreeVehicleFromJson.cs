using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary> A mapping entity used for automated deserialization of JSON data before passing it on into persistent objects. </summary>
    public class ResearchTreeVehicleFromJson : DeserializedFromJson
    {
        /// <summary> The vehicle's research rank. </summary>
        [JsonProperty("rank", Required = Required.Always)]
        public int Rank { get; set; }

        /// <summary> 1-based coordinates of the research tree grid cell this vehicle occupies with its <see cref="Rank"/>. </summary>
        [JsonProperty("rankPosXY")]
        public List<int> PresetCellCoordinatesWithinRank { get; set; }

        /// <summary> 1-based coordinates of the research tree grid cell this vehicle occupies with its <see cref="Rank"/>. </summary>
        public List<int> CellCoordinatesWithinRank { get; set; }

        /// <summary> The Gaijin ID of the vehicle that is required to unlock this one. This property is unreliable as it is only used explicitly when the classic JSON structure (the one used with planes and tanks) is not followed. </summary>
        [JsonProperty("reqAir")]
        public string RequiredVehicleGaijinId { get; set; }

        /// <summary> A 0-based index of the vehicle in its research tree folder. </summary>
        public int? FolderIndex { get; set; }

        /// <summary> The category of hidden vehicles this one belongs to. </summary>
        [JsonProperty("gift")]
        public string CategoryOfHiddenVehicles { get; set; }

        /// <summary> Whether this vehicle is hidden from those that don't own it. </summary>
        [JsonProperty("showOnlyWhenBought")]
        public bool ShowOnlyWhenBought { get; set; }

        /// <summary>
        /// The Gaijin ID of the game platform this vehicle is available for purchase on. It is implicitly considered not available on others. Already purchased vehicles are not affected.
        /// <para> This is the opposite of <see cref="PlatformGaijinIdVehicleIsHiddenOn"/>. </para>
        /// </summary>
        [JsonProperty("showByPlatform")]
        public string PlatformGaijinIdVehicleIsAvailableOn { get; set; }

        /// <summary>
        /// The Gaijin ID of the game platform this vehicle is unavailable for purchase on. It is implicitly considered available on others. Already purchased vehicles are not affected.
        /// <para> This is the opposite of <see cref="PlatformGaijinIdVehicleIsAvailableOn"/>. </para>
        /// </summary>
        [JsonProperty("hideByPlatform")]
        public string PlatformGaijinIdVehicleIsHiddenOn { get; set; }

        /// <summary>
        /// The condition for hiding this vehicle.
        /// <para> Not related to <see cref="ShowOnlyWhenBought"/> and <see cref="PlatformGaijinIdVehicleIsHiddenOn"/>. </para>
        /// </summary>
        [JsonProperty("hideFeature")]
        public string HideCondition { get; set; }

        /// <summary> The Gaijin Marketplace ID. </summary>
        [JsonProperty("marketplaceItemdefId")]
        public long? MarketplaceId { get; set; }
    }
}