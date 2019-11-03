using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    public interface IPersistentWarThunderObjectWithId : IPersistentObjectWithId
    {
        /// <summary> Fills valid properties of the object with values deserialized from JSON data. </summary>
        /// <param name="instanceDeserializedFromJson"> The temporary non-persistent object storing deserialized data. </param>
        void InitializeWithDeserializedJson(IDeserializedFromJsonWithGaijinId instanceDeserializedFromJson);
    }
}