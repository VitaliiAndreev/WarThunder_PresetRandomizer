namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A persistent (stored in a database) object that has an ID and a Gaijin ID. </summary>
    public interface IPersistentObjectWithIdAndGaijinId : IPersistentDeserialisedObjectWithId
    {
        /// <summary> The object's Gaijin ID. </summary>
        string GaijinId { get; }
    }
}