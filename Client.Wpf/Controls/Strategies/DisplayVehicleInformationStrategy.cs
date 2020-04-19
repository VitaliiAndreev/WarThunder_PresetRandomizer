﻿using Client.Wpf.Controls.Strategies.Interfaces;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using System.Text;

namespace Client.Wpf.Controls.Strategies
{
    public abstract class DisplayVehicleInformationStrategy : IDisplayVehicleInformationStrategy
    {
        #region Methods: Checks

        public bool ShowReserveTag(IVehicle vehicle) => vehicle.IsResearchable && vehicle.EconomyData.PurchaseCostInSilver.IsZero();

        public bool ShowStarterGiftTag(IVehicle vehicle) => vehicle.GiftedToNewPlayersForSelectingTheirFirstBranch;

        public bool ShowEyeIcon(IVehicle vehicle) => vehicle.IsHiddenUnlessOwned;

        public bool ShowControllerIcon(IVehicle vehicle) => vehicle.IsAvailableOnlyOnConsoles;

        public bool ShowSpaceAfterControllerIcon(IVehicle vehicle) => ShowControllerIcon(vehicle) && ShowPackTag(vehicle);

        public bool ShowPackTag(IVehicle vehicle) => vehicle.IsSoldInTheStore;

        public bool ShowGoldenEagleCost(IVehicle vehicle) => vehicle.IsPurchasableForGoldenEagles && !vehicle.IsSquadronVehicle;

        public bool ShowMarketIcon(IVehicle vehicle) => vehicle.IsSoldOnTheMarket;

        public virtual bool ShowSpaceAfterSpecialIconsAndTags(IVehicle vehicle) => ShowEyeIcon(vehicle) || ShowControllerIcon(vehicle) || ShowMarketIcon(vehicle) || ShowPackTag(vehicle);

        public bool ShowBinocularsIcon(IVehicle vehicle) => vehicle.GroundVehicleTags?.CanScout ?? false;

        #endregion Methods: Checks
        #region Methods: Output

        protected void SetSharedLeftPart(StringBuilder stringBuilder, IVehicle vehicle)
        {
            void append(object stringOrCharacter) => stringBuilder.Append(stringOrCharacter);

            if (ShowStarterGiftTag(vehicle))
                append($"{EWord.Starter}{ECharacter.Space}");
            else if (ShowReserveTag(vehicle))
                append($"{EWord.Reserve}{ECharacter.Space}");

            if (ShowEyeIcon(vehicle))
                append(ECharacter.Eye);

            if (ShowControllerIcon(vehicle))
                append(EGaijinCharacter.Controller);
            if (ShowSpaceAfterControllerIcon(vehicle))
                append(ECharacter.Space);
        }

        protected void SetSharedRightPart(StringBuilder stringBuilder, EGameMode gameMode, IVehicle vehicle)
        {
            void append(object stringOrCharacter) => stringBuilder.Append(stringOrCharacter);

            if (ShowMarketIcon(vehicle))
                append(EGaijinCharacter.GaijinCoin);

            if (ShowSpaceAfterSpecialIconsAndTags(vehicle))
                append(ECharacter.Space);

            if (ShowBinocularsIcon(vehicle))
                append($"{EGaijinCharacter.Binoculars}{ECharacter.Space}");

            append(GetBattleRating(vehicle, gameMode));
            append($"{ECharacter.Space}");
            append($"{ECharacter.Slash}");
            append($"{ECharacter.Space}");
            append(GetRank(vehicle));
            append($"{ECharacter.Space}");
            append(GetClassIcon(vehicle));
        }

        public char GetClassIcon(IVehicle vehicle) => EReference.ClassIcons[vehicle.Class];
        public string GetBattleRating(IVehicle vehicle, EGameMode gameMode) => vehicle.BattleRatingFormatted[gameMode];
        public ERank GetRank(IVehicle vehicle) => vehicle.Rank.CastTo<ERank>();

        /// <summary> Generates a formatted string with <paramref name="vehicle"/> information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode to account for. </param>
        /// <param name="vehicle"> The vehicle whose information to display. </param>
        /// <returns></returns>
        public abstract string GetFormattedVehicleInformation(EGameMode gameMode, IVehicle vehicle);

        #endregion Methods: Output
    }
}