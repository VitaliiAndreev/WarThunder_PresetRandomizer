using Client.Shared.Interfaces;
using Core;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Extensions;
using System;

namespace Client.Shared.Objects
{
    public class VehicleLite : IVehicleLite
    {
        private const decimal placeholderBattleRating = -1m;
        private const int placeholderCost = -1;

        #region Properties

        public string GaijinIdLite { get; }
        public string Name { get; }
        public string Nation { get; }
        public string Country { get; }
        public string Branch { get; }
        public ERank Rank { get; }
        public decimal BattleRatingInArcade { get; }
        public decimal BattleRatingInRealistic { get; }
        public decimal BattleRatingInSimulator { get; }
        public string Class { get; }
        public string Subclass1 { get; }
        public string Subclass2 { get; }
        public string Tag1 { get; }
        public string Tag2 { get; }
        public string Tag3 { get; }
        public bool IsResearchable { get; }
        public bool IsReserve { get; }
        public bool IsSquadronVehicle { get; }
        public int UnlockCostInResearch { get; }
        public int PurchaseCostInSilver { get; }
        public bool IsHiddenUnlessOwned { get; }
        public bool IsPremium { get; }
        public bool IsPurchasableForGoldenEagles { get; }
        public int MinimumPurchaseCostInGold { get; }
        public int PurchaseCostInGold { get; }
        public bool GiftedToNewPlayersForSelectingTheirFirstBranch { get; }
        public bool IsSoldInTheStore { get; }
        public bool IsSoldOnTheMarket { get; }
        public bool IsAvailableOnlyOnConsoles { get; }
        public int RegularCrewTrainingCost { get; }
        public int ExpertCrewTrainingCost { get; }
        public int AceCrewTrainingCostInResearch { get; }
        public int AceCrewTrainingCostInGold { get; }
        public int RepairCostInArcade { get; }
        public int RepairCostInRealistic { get; }
        public int RepairCostInSimulator { get; }
        public decimal ResearchGainMultiplierLite { get; }
        public decimal SilverGainMultiplierInArcade { get; }
        public decimal SilverGainMultiplierInRealistic { get; }
        public decimal SilverGainMultiplierInSimulator { get; }

        #endregion Properties
        #region Constructors

        public VehicleLite(IVehicle vehicle, Language language, Func<object, string> localise, Func<int, string> getTag)
        {
            GaijinIdLite = vehicle.GaijinId;
            Name = vehicle.ResearchTreeName.GetLocalisation(language);
            Nation = localise(vehicle.Nation.AsEnumerationItem);
            Country = localise(vehicle.Country);
            Branch = localise(vehicle.Category.AsEnumerationItem);
            Rank = vehicle.RankAsEnumerationItem;
            BattleRatingInArcade = vehicle.BattleRating.Arcade ?? placeholderBattleRating;
            BattleRatingInRealistic = vehicle.BattleRating.Realistic ?? placeholderBattleRating;
            BattleRatingInSimulator = vehicle.BattleRating.Simulator ?? placeholderBattleRating;
            Class = localise(vehicle.Class);
            Subclass1 = localise(vehicle.Subclasses.First);
            Subclass2 = localise(vehicle.Subclasses.Second);
            Tag1 = getTag(0);
            Tag2 = getTag(1);
            Tag3 = getTag(2);
            IsResearchable = vehicle.IsResearchable;
            IsReserve = vehicle.IsReserve;
            IsSquadronVehicle = vehicle.IsSquadronVehicle;
            UnlockCostInResearch = (IsResearchable || IsSquadronVehicle)
                && vehicle.EconomyData is VehicleEconomyData
                && vehicle.EconomyData.UnlockCostInResearch.HasValue
                && vehicle.EconomyData.UnlockCostInResearch.Value.IsPositive()
                    ? vehicle.EconomyData.UnlockCostInResearch.Value
                    : IsReserve ? 0 : placeholderCost;
            PurchaseCostInSilver = (IsResearchable || IsSquadronVehicle)
                && vehicle.EconomyData is VehicleEconomyData
                && vehicle.EconomyData.PurchaseCostInSilver.IsPositive()
                    ? vehicle.EconomyData.PurchaseCostInSilver
                    : IsReserve ? 0 : placeholderCost;
            IsHiddenUnlessOwned = vehicle.IsHiddenUnlessOwned;
            IsPremium = vehicle.IsPremium;
            IsPurchasableForGoldenEagles = vehicle.IsPurchasableForGoldenEagles;
            PurchaseCostInGold = IsPurchasableForGoldenEagles && (IsPremium && vehicle.EconomyData.PurchaseCostInGold.HasValue || IsSquadronVehicle && vehicle.EconomyData.PurchaseCostInGoldAsSquadronVehicle.HasValue)
                ? (IsPremium ? vehicle.EconomyData.PurchaseCostInGold.Value : vehicle.EconomyData.PurchaseCostInGoldAsSquadronVehicle.Value)
                : placeholderCost;
            MinimumPurchaseCostInGold = IsSquadronVehicle && vehicle.EconomyData.DiscountedPurchaseCostInGoldAsSquadronVehicle.HasValue
                ? vehicle.EconomyData.DiscountedPurchaseCostInGoldAsSquadronVehicle.Value
                : placeholderCost;
            GiftedToNewPlayersForSelectingTheirFirstBranch = vehicle.GiftedToNewPlayersForSelectingTheirFirstBranch;
            IsSoldInTheStore = vehicle.IsSoldInTheStore;
            IsSoldOnTheMarket = vehicle.IsSoldOnTheMarket;
            IsAvailableOnlyOnConsoles = vehicle.IsAvailableOnlyOnConsoles;
            RegularCrewTrainingCost = vehicle.EconomyData.BaseCrewTrainCostInSilver;
            ExpertCrewTrainingCost = vehicle.EconomyData.ExpertCrewTrainCostInSilver;
            AceCrewTrainingCostInResearch = vehicle.EconomyData.AceCrewTrainCostInResearch;
            AceCrewTrainingCostInGold = vehicle.EconomyData.AceCrewTrainCostInGold;
            RepairCostInArcade = vehicle.EconomyData.RepairCost.Arcade ?? placeholderCost;
            RepairCostInRealistic = vehicle.EconomyData.RepairCost.Realistic ?? placeholderCost;
            RepairCostInSimulator = vehicle.EconomyData.RepairCost.Simulator ?? placeholderCost;
            ResearchGainMultiplierLite = vehicle.EconomyData.ResearchGainMultiplier;
            SilverGainMultiplierInArcade = vehicle.EconomyData.SilverGainMultiplier.Arcade ?? placeholderBattleRating;
            SilverGainMultiplierInRealistic = vehicle.EconomyData.SilverGainMultiplier.Realistic ?? placeholderBattleRating;
            SilverGainMultiplierInSimulator = vehicle.EconomyData.SilverGainMultiplier.Simulator ?? placeholderBattleRating;
        }

        #endregion Constructors
    }
}