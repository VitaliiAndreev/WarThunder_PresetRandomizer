using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Localization.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Localization.Vehicle.Interfaces
{
    /// <summary> A vehicle localisation set. </summary>
    public interface IVehicleLocalisation : ILocalisation
    {
        /// <summary> The vehicle this localisation belongs to. </summary>
        IVehicle Vehicle { get; }
    }
}