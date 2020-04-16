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
            "{0}{1}{2}{3}{4}{5}{6}{7} {8} / {9}"
                .FormatFluently
                (
                    vehicle.IsHiddenUnlessOwned ? ECharacter.Eye : string.Empty,
                    vehicle.IsAvailableOnlyOnConsoles ? EGaijinCharacter.Controller.ToString() : string.Empty,
                    vehicle.IsAvailableOnlyOnConsoles && vehicle.IsSoldInTheStore ? ECharacter.Space.ToString() : string.Empty,
                    vehicle.IsSoldInTheStore ? EWord.Pack : string.Empty,
                    vehicle.IsSoldOnTheMarket ? EGaijinCharacter.GaijinCoin.ToString() : string.Empty,
                    vehicle.IsHiddenUnlessOwned || vehicle.IsAvailableOnlyOnConsoles || vehicle.IsSoldOnTheMarket || vehicle.IsSoldInTheStore ? ECharacter.Space.ToString() : string.Empty,
                    vehicle.GroundVehicleTags?.CanScout ?? false ? EGaijinCharacter.Binoculars.ToString() : string.Empty,
                    EReference.ClassIcons[vehicle.Class],
                    vehicle.BattleRatingFormatted[gameMode],
                    vehicle.Rank.CastTo<ERank>()
                )
            ;
    }
}