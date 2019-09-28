using Client.Wpf.Controls.Strategies.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Extensions;

namespace Client.Wpf.Controls.Strategies
{
    /// <summary> A strategy for generating a formatted string with extended <see cref="IVehicle"/> information for the given <see cref="EGameMode"/>. </summary>
    public class DisplayExtendedVehicleInformationStrategy : IDisplayVehicleInformationStrategy
    {
        /// <summary> Generates a formatted string with <paramref name="vehicle"/> information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode to account for. </param>
        /// <param name="vehicle"> The vehicle whose information to display. </param>
        /// <returns></returns>
        public string GetFormattedVehicleInformation(EGameMode gameMode, IVehicle vehicle) =>
            $"{EReference.BranchIcons[vehicle.Branch.AsEnumerationItem]} {vehicle.BattleRatingFormatted[gameMode]} / {vehicle.Rank.CastTo<ERank>()}";
    }
}