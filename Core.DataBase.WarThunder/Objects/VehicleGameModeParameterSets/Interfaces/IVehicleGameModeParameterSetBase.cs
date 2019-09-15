using Core.DataBase.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    public interface IVehicleGameModeParameterSetBase: IPersistentObjectWithId
    {
        /// <summary> The vehicle this set belongs to. </summary>
        IVehicle Vehicle { get; }
    }
}