using Client.Wpf.Controls.Strategies.Interfaces;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Localization.Interfaces;
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

        public bool ReplaceClassWithSubclass(IVehicle vehicle) => vehicle.Subclasses.First.IsValid();

        public bool ShowSecondSubclass(IVehicle vehicle) => vehicle.Subclasses.Second.IsValid() && vehicle.Subclasses.Second != vehicle.Subclasses.First;

        #endregion Methods: Checks
        #region Methods: Output

        protected string GetLocalisedString(object localisationKey) => ApplicationHelpers.LocalizationManager.GetLocalizedString(localisationKey.ToString());
        protected string GetLocalisationText(ILocalization localisation) => localisation?.GetLocalization(WpfSettings.LocalizationLanguage);

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

            append(GetBattleRating(gameMode, vehicle));
            append($"{ESeparator.SpaceSlashSpace}");
            append(GetRank(vehicle));
            append($"{ECharacter.Space}");
            append(GetClassIcon(vehicle));
        }

        public char GetClassIcon(IVehicle vehicle) => EReference.ClassIcons[vehicle.Class];

        public string GetClass(IVehicle vehicle)
        {
            if (ReplaceClassWithSubclass(vehicle))
                return GetLocalisedString(vehicle.Subclasses.First);

            return GetLocalisedString(vehicle.Class);
        }

        public string GetBattleRating(EGameMode gameMode, IVehicle vehicle) => vehicle.BattleRatingFormatted[gameMode];

        public ERank GetRank(IVehicle vehicle) => vehicle.Rank.CastTo<ERank>();

        /// <summary> Generates a formatted string with <paramref name="vehicle"/> information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode to account for. </param>
        /// <param name="vehicle"> The vehicle whose information to display. </param>
        /// <returns></returns>
        public abstract string GetVehicleInfoBottomRow(EGameMode gameMode, IVehicle vehicle);

        public string GetVehicleCardClassRow(IVehicle vehicle)
        {
            var stringBuilder = new StringBuilder();

            void append(object stringOrCharacter) => stringBuilder.Append(stringOrCharacter);

            if (ShowBinocularsIcon(vehicle))
                append($"{EGaijinCharacter.Binoculars}{ECharacter.Space}");

            append($"{GetClassIcon(vehicle)}{ECharacter.Space}");
            append($"{GetClass(vehicle)}{ECharacter.Space}");

            if (ShowSecondSubclass(vehicle))
                append($"{ECharacter.Slash}{ECharacter.Space}{GetLocalisedString(vehicle.Subclasses.Second)}");

            return stringBuilder.ToString();
        }

        public string GetVehicleCardCountryRow(IVehicle vehicle)
        {
            var stringBuilder = new StringBuilder();

            void append(object stringOrCharacter) => stringBuilder.Append(stringOrCharacter);

            append($"{GetLocalisedString(vehicle.Country)}");
            append($"{ESeparator.SpaceSlashSpace}");
            append($"{GetLocalisedString(EWord.Rank)}{ECharacter.Colon}{ECharacter.Space}{vehicle.RankAsEnumerationItem}");
            append($"{ESeparator.SpaceSlashSpace}");
            append($"{GetLocalisedString(EWord.BattleRating)}{ECharacter.Colon}{ECharacter.Space}");

            return stringBuilder.ToString();
        }

        #endregion Methods: Output
    }
}