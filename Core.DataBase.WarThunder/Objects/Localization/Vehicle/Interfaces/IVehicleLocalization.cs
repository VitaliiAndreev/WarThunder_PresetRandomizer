using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Localization.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Localization.Vehicle.Interfaces
{
    /// <summary> A vehicle localization set. </summary>
    public interface IVehicleLocalization : ILocalization
    {
        /// <summary> The vehicle this localization belongs to. </summary>
        IVehicle Vehicle { get; }
    }
}