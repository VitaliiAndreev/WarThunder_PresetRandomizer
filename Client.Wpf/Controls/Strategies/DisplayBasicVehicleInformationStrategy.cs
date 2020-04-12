using Client.Wpf.Controls.Strategies.Interfaces;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;

namespace Client.Wpf.Controls.Strategies
{
    /// <summary> A strategy for generating a formatted string with basic <see cref="IVehicle"/> information for the given <see cref="EGameMode"/>. </summary>
    public class DisplayBasicVehicleInformationStrategy : IDisplayVehicleInformationStrategy
    {
        /// <summary> Generates a formatted string with <paramref name="vehicle"/> information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode to account for. </param>
        /// <param name="vehicle"> The vehicle whose information to display. </param>
        /// <returns></returns>
        public string GetFormattedVehicleInformation(EGameMode gameMode, IVehicle vehicle) =>
            "{0}{1}{2}{3} {4} / {5}"
                .FormatFluently
                (
                    vehicle.IsHiddenUnlessOwned ? $"{ECharacter.Eye}{ECharacter.Space}" : string.Empty,
                    vehicle.IsSoldOnTheMarket ? $"{EGaijinCharacter.GaijinCoin}{ECharacter.Space}" : string.Empty,
                    vehicle.IsSoldInTheStore ? $"{EWord.Pack}{ECharacter.Space}" : string.Empty,
                    EReference.ClassIcons[vehicle.Class],
                    vehicle.BattleRatingFormatted[gameMode],
                    vehicle.Rank.CastTo<ERank>()
                )
            ;
    }
}