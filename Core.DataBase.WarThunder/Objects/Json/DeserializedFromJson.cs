using Core.DataBase.WarThunder.Objects.Json.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary> The base class for mapping entities used for automated deserialization of JSON data before passing it on into persistent objects. </summary>
    public class DeserializedFromJson : IDeserializedFromJson
    {
        /// <summary> The entity's Gaijin ID. </summary>
        public virtual string GaijinId { get; set; }
    }
}
