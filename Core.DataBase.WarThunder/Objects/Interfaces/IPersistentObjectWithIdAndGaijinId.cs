using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A persistent (stored in a database) object that has an ID and a Gaijin ID. </summary>
    public interface IPersistentObjectWithIdAndGaijinId : IPersistentObjectWithId
    {
        /// <summary> The object's Gaijin ID. </summary>
        string GaijinId { get; }

        /// <summary> Fills valid properties of the object with values deserialized from JSON data. </summary>
        /// <param name="instanceDeserializedFromJson"> The temporary non-persistent object storing deserialized data. </param>
        void InitializeWithDeserializedJson(IDeserializedFromJson instanceDeserializedFromJson);
    }
}