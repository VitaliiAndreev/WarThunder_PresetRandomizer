using Core.DataBase.WarThunder.Objects.Json.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary> A base mapping entity used for automated deserialization of JSON data before passing it on into persistent objects. </summary>
    public class DeserializedFromJsonWithGaijinId : IDeserializedFromJsonWithGaijinId
    {
        /// <summary> The entity's Gaijin ID. </summary>
        public virtual string GaijinId { get; set; }
    }
}
