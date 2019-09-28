using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;

namespace Client.Wpf.Controls.Strategies.Interfaces
{
    /// <summary> A strategy for generating a formatted string with <see cref="IVehicle"/> information for the given <see cref="EGameMode"/>. </summary>
    public interface IDisplayVehicleInformationStrategy
    {
        /// <summary> Generates a formatted string with <paramref name="vehicle"/> information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode to account for. </param>
        /// <param name="vehicle"> The vehicle whose information to display. </param>
        /// <returns></returns>
        string GetFormattedVehicleInformation(EGameMode gameMode, IVehicle vehicle);
    }
}