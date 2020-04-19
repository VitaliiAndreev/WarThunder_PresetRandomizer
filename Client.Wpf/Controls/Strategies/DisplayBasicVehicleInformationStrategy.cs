using Client.Wpf.Controls.Strategies.Interfaces;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using System.Text;

namespace Client.Wpf.Controls.Strategies
{
    /// <summary> A strategy for generating a formatted string with basic <see cref="IVehicle"/> information for the given <see cref="EGameMode"/>. </summary>
    public class DisplayBasicVehicleInformationStrategy : IDisplayVehicleInformationStrategy
    {
        /// <summary> Generates a formatted string with <paramref name="vehicle"/> information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode to account for. </param>
        /// <param name="vehicle"> The vehicle whose information to display. </param>
        /// <returns></returns>
        public string GetFormattedVehicleInformation(EGameMode gameMode, IVehicle vehicle)
        {
            var stringBuilder = new StringBuilder();

            void append(object stringOrCharacter) => stringBuilder.Append(stringOrCharacter);

            if (vehicle.GiftedToNewPlayersForSelectingTheirFirstBranch)
                append($"{EWord.Starter}{ECharacter.Space}");
            else if (vehicle.IsResearchable && vehicle.EconomyData.PurchaseCostInSilver.IsZero())
                append($"{EWord.Reserve}{ECharacter.Space}");

            if (vehicle.IsHiddenUnlessOwned)
                append(ECharacter.Eye);

            if (vehicle.IsAvailableOnlyOnConsoles)
            {
                append(EGaijinCharacter.Controller);

                if (vehicle.IsSoldInTheStore)
                    append(ECharacter.Space);
            }

            if (vehicle.IsSoldInTheStore)
                append(EWord.Pack);

            if (vehicle.IsSoldOnTheMarket)
                append(EGaijinCharacter.GaijinCoin);

            if (vehicle.IsHiddenUnlessOwned || vehicle.IsAvailableOnlyOnConsoles || vehicle.IsSoldOnTheMarket || vehicle.IsSoldInTheStore)
                append(ECharacter.Space);

            if (vehicle.GroundVehicleTags?.CanScout ?? false)
                append(EGaijinCharacter.Binoculars);

            append(EReference.ClassIcons[vehicle.Class]);
            append(vehicle.BattleRatingFormatted[gameMode]);
            append($"{ECharacter.Space}{ECharacter.Slash}{ECharacter.Space}{vehicle.Rank.CastTo<ERank>()}");

            return stringBuilder.ToString();
        }
    }
}