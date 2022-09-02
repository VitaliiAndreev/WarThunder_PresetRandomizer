using Client.Wpf.Controls.Strategies.Interfaces;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Localization.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Wpf.Controls.Strategies
{
    public abstract class DisplayVehicleInformationStrategy : IDisplayVehicleInformationStrategy
    {
        #region Methods: Checks

        public bool ShowReserveTag(IVehicle vehicle) => vehicle.IsReserve;

        public bool ShowStarterGiftTag(IVehicle vehicle) => vehicle.GiftedToNewPlayersForSelectingTheirFirstBranch;

        public bool ShowEyeIcon(IVehicle vehicle) => vehicle.IsHiddenUnlessOwned;

        public bool ShowControllerIcon(IVehicle vehicle) => vehicle.IsAvailableOnlyOnConsoles;

        public bool ShowSpaceAfterControllerIcon(IVehicle vehicle) => ShowControllerIcon(vehicle) && ShowPackTag(vehicle);

        public bool ShowPackTag(IVehicle vehicle) => vehicle.IsSoldInTheStore;

        public bool ShowGoldenEagleCost(IVehicle vehicle) => vehicle.IsPurchasableForGoldenEagles && !vehicle.IsSquadronVehicle && vehicle.EconomyData.PurchaseCostInGold.HasValue;

        public bool ShowSquadronGoldenEagleCost(IVehicle vehicle) =>
            vehicle.IsPurchasableForGoldenEagles
            && vehicle.EconomyData.PurchaseCostInGoldAsSquadronVehicle.HasValue
            && vehicle.EconomyData.DiscountedPurchaseCostInGoldAsSquadronVehicle.HasValue;

        public bool ShowMarketIcon(IVehicle vehicle) => vehicle.IsSoldOnTheMarket;

        public virtual bool ShowSpaceAfterSpecialIconsAndTags(IVehicle vehicle) => ShowEyeIcon(vehicle) || ShowControllerIcon(vehicle) || ShowMarketIcon(vehicle) || ShowPackTag(vehicle) || ShowGoldenEagleCost(vehicle) || ShowSquadronGoldenEagleCost(vehicle);

        public bool ShowBinocularsIcon(IVehicle vehicle) => vehicle.GroundVehicleTags?.CanScout ?? false;

        public bool ReplaceClassWithSubclass(IVehicle vehicle) => vehicle.Subclasses.First.HasValue && vehicle.Subclasses.First.Value.IsValid();

        public bool ShowSecondSubclass(IVehicle vehicle) =>
            vehicle.Subclasses.First.HasValue
            && vehicle.Subclasses.Second.HasValue
            && vehicle.Subclasses.Second.Value.IsValid()
            && vehicle.Subclasses.Second.Value != vehicle.Subclasses.First.Value;

        public bool ShowResearchCosts(IVehicle vehicle) => vehicle.IsResearchable && vehicle.EconomyData.UnlockCostInResearch.HasValue;

        public bool ShowSilverLionCosts(IVehicle vehicle) => vehicle.IsResearchable && vehicle.EconomyData.PurchaseCostInSilver.IsPositive();

        public bool ShowPremiumIcon(IVehicle vehicle) => vehicle.IsPremium;

        public bool ShowTorpedoBomberTagAsClass(IVehicle vehicle) => vehicle.Branch == EBranch.Aviation && vehicle.AircraftTags.IsTorpedoBomber;

        #endregion Methods: Checks
        #region Methods: Output

        protected string GetLocalisedString(object localisationKey) => ApplicationHelpers.LocalisationManager.GetLocalisedString(localisationKey.ToString());
        protected string GetLocalisationText(ILocalisation localisation) => localisation?.GetLocalisation(WpfSettings.LocalizationLanguage);

        protected void SetFirstSharedPart(StringBuilder stringBuilder, EGameMode gameMode, IVehicle vehicle)
        {
            void append(object stringOrCharacter) => stringBuilder.Append(stringOrCharacter);

            if (ShowStarterGiftTag(vehicle))
                append($"{GetLocalisedString(ELocalisationKey.Starter)}{Character.Space}");
            else if (ShowReserveTag(vehicle))
                append($"{GetLocalisedString(ELocalisationKey.Reserve)}{Character.Space}");

            if (ShowEyeIcon(vehicle))
                append(Character.Eye);

            if (ShowControllerIcon(vehicle))
                append(EGaijinCharacter.Controller);
            if (ShowSpaceAfterControllerIcon(vehicle))
                append(Character.Space);

            if (ShowPackTag(vehicle))
                append(GetLocalisedString(ELocalisationKey.Pack));
            else if (ShowGoldenEagleCost(vehicle))
                append($"{vehicle.EconomyData.PurchaseCostInGold.Value}{EGaijinCharacter.GoldenEagle}");
            else if (ShowSquadronGoldenEagleCost(vehicle))
                append($"{vehicle.EconomyData.DiscountedPurchaseCostInGoldAsSquadronVehicle.Value}{Character.Minus}{vehicle.EconomyData.PurchaseCostInGoldAsSquadronVehicle.Value}{EGaijinCharacter.GoldenEagle}");

            if (ShowMarketIcon(vehicle))
                append(EGaijinCharacter.GaijinCoin);

            if (ShowSpaceAfterSpecialIconsAndTags(vehicle))
                append(Character.Space);

            if (ShowBinocularsIcon(vehicle))
                append($"{EGaijinCharacter.Binoculars}{Character.Space}");

            append(GetBattleRating(gameMode, vehicle));
        }

        protected void SetSecondSharedPart(StringBuilder stringBuilder, IVehicle vehicle)
        {
            void append(object stringOrCharacter) => stringBuilder.Append(stringOrCharacter);

            append($"{Character.Space}");
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
                append($"{EGaijinCharacter.Binoculars}{Character.Space}");

            append($"{GetClassIcon(vehicle)}{Character.Space}");
            append($"{GetClass(vehicle)}{Character.Space}");

            if (ShowSecondSubclass(vehicle))
                append($"{Character.Slash}{Character.Space}{GetLocalisedString(vehicle.Subclasses.Second)}");

            if (ShowTorpedoBomberTagAsClass(vehicle))
                append($"{Character.Slash}{Character.Space}{GetLocalisedString(EVehicleBranchTag.TorpedoBomber)}");

            return stringBuilder.ToString();
        }

        public string GetVehicleCardTagRow(IVehicle vehicle)
        {
            var stringBuilder = new StringBuilder();

            void append(object stringOrCharacter) => stringBuilder.Append(stringOrCharacter);

            var excludedTags = new List<EVehicleBranchTag> { EVehicleBranchTag.Scout, EVehicleBranchTag.TorpedoBomber };
            var tagString = vehicle
                .Tags
                .Where(tag => !tag.IsDefault() && !tag.IsIn(excludedTags))
                .Select(tag => GetLocalisedString(tag))
                .StringJoin(Separator.SpaceSlashSpace)
            ;

            append(tagString);

            return stringBuilder.ToString();
        }

        public string GetVehicleCardCountryRow(IVehicle vehicle)
        {
            var stringBuilder = new StringBuilder();

            void append(object stringOrCharacter) => stringBuilder.Append(stringOrCharacter);

            append($"{GetLocalisedString(vehicle.Country)}");
            append($"{Separator.SpaceSlashSpace}");
            append($"{GetLocalisedString(ELocalisationKey.Rank)}{Character.Colon}{Character.Space}{vehicle.RankAsEnumerationItem}");
            append($"{Separator.SpaceSlashSpace}");
            append($"{GetLocalisedString(ELocalisationKey.BattleRating)}{Character.Colon}{Character.Space}");

            return stringBuilder.ToString();
        }

        public string GetVehicleCardRequirementsRow(IVehicle vehicle)
        {
            var stringBuilder = new StringBuilder();

            void append(object stringOrCharacter) => stringBuilder.Append(stringOrCharacter);

            if (ShowReserveTag(vehicle))
                append($"{GetLocalisedString(ELocalisationKey.Reserve)}");

            if (ShowStarterGiftTag(vehicle))
                append($"{GetLocalisedString(ELocalisationKey.Starter)}{Separator.SpaceSlashSpace}");

            if (ShowGoldenEagleCost(vehicle))
                append($"{vehicle.EconomyData.PurchaseCostInGold.Value.WithNumberGroupsSeparated()}{Character.Space}{EGaijinCharacter.GoldenEagle}");

            if (ShowMarketIcon(vehicle))
                append($"{EGaijinCharacter.GaijinCoin}{Character.Space}{GetLocalisedString(ELocalisationKey.SoldOnTheMarket)}");

            if (ShowEyeIcon(vehicle))
                append($"{Character.Eye}{Character.Space}{GetLocalisedString(ELocalisationKey.Hidden)}");
            if (ShowEyeIcon(vehicle) && ShowControllerIcon(vehicle))
                append(Separator.SpaceSlashSpace);

            if (ShowControllerIcon(vehicle))
                append($"{EGaijinCharacter.Controller}{Character.Space}{GetLocalisedString(ELocalisationKey.ConsoleExclusive)}");
            if (ShowControllerIcon(vehicle) && ShowPackTag(vehicle))
                append(Separator.SpaceSlashSpace);

            if (ShowPackTag(vehicle))
                append(GetLocalisedString(ELocalisationKey.AvailableInStorePacks));

            if (ShowResearchCosts(vehicle))
                append($"{vehicle.EconomyData.UnlockCostInResearch.Value.WithNumberGroupsSeparated()}{Character.Space}{EGaijinCharacter.Research}");
            if (ShowResearchCosts(vehicle) && (ShowSilverLionCosts(vehicle) || ShowSquadronGoldenEagleCost(vehicle)))
                append(Character.Space);

            if (ShowSilverLionCosts(vehicle))
                append($"{vehicle.EconomyData.PurchaseCostInSilver.WithNumberGroupsSeparated()}{Character.Space}{EGaijinCharacter.SilverLion}");
            if (ShowSquadronGoldenEagleCost(vehicle))
                append(Character.Space);

            if (ShowSquadronGoldenEagleCost(vehicle))
                append($"{vehicle.EconomyData.DiscountedPurchaseCostInGoldAsSquadronVehicle.Value.WithNumberGroupsSeparated()}{Character.Minus}{vehicle.EconomyData.PurchaseCostInGoldAsSquadronVehicle.Value.WithNumberGroupsSeparated()}{Character.Space}{EGaijinCharacter.GoldenEagle}");

            return stringBuilder.ToString();
        }

        public string GetVehicleCardRegularCrewRequirements(IVehicle vehicle)
        {
            var cost = vehicle.EconomyData.BaseCrewTrainCostInSilver;

            if (cost.IsZero())
                return ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.Free);

            return $"{cost.WithNumberGroupsSeparated()}{Character.Space}{EGaijinCharacter.SilverLion}";
        }

        public string GetVehicleCardExpertCrewRequirements(IVehicle vehicle) =>
            $"{vehicle.EconomyData.ExpertCrewTrainCostInSilver.WithNumberGroupsSeparated()}{Character.Space}{EGaijinCharacter.SilverLion}";

        public string GetVehicleCardAceCrewRequirements(IVehicle vehicle)
        {
            var stringBuilder = new StringBuilder();

            void append(object stringOrCharacter) => stringBuilder.Append(stringOrCharacter);

            append($"{vehicle.EconomyData.AceCrewTrainCostInResearch.WithNumberGroupsSeparated()}{Character.Space}{EGaijinCharacter.Research}");
            append(Separator.SpaceSlashSpace);
            append($"{vehicle.EconomyData.AceCrewTrainCostInGold.WithNumberGroupsSeparated()}{Character.Space}{EGaijinCharacter.GoldenEagle}");

            return stringBuilder.ToString();
        }

        public string GetVehicleCardRepairCost(IVehicle vehicle, EGameMode gameMode)
        {
            var cost = vehicle.EconomyData.RepairCost[gameMode];

            if (!cost.HasValue || cost.Value.IsZero())
                return ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.Free);

            return $"{cost.Value.WithNumberGroupsSeparated()}{Character.Space}{EGaijinCharacter.SilverLion}";
        }

        #endregion Methods: Output
    }
}