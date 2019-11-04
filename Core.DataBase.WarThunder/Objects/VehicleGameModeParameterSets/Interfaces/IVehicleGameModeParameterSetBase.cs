using Core.DataBase.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    public interface IVehicleGameModeParameterSetBase: IPersistentObjectWithId
    {
        /// <summary> The entity this set belongs to. </summary>
        IPersistentWarThunderObjectWithId Entity { get; }
    }
}