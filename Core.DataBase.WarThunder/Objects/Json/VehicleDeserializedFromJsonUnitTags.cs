﻿using Newtonsoft.Json;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary> A mapping entity used for automated deserialization of JSON data before passing it on into persistent objects. </summary>
    public class VehicleDeserializedFromJsonUnitTags : DeserializedFromJsonWithGaijinId
    {
        /// <summary> The Gaijin ID of the vehicle's branch. </summary>
        [JsonProperty("type", Required = Required.Always)]
        public string BranchGaijinId { get; set; }

        /// <summary> The Gaijin ID of the vehicle's country of origin (not the nation in whose research tree the vehicles is in). </summary>
        [JsonProperty("operatorCountry")]
        public string CountryGaijinId { get; set; }

        [JsonProperty("tags")]
        public VehicleTagsDeserializedFromJson Tags { get; set; }
    }
}