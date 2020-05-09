using Client.Shared.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using System;

namespace Client.Shared.Objects
{
    public class VehicleLite : IVehicleLite
    {
        #region Properties

        public string GaijinId { get; }
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
        public int PurchaseCostInGold { get; }
        public int MinimumPurchaseCostInGold { get; }
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

        #endregion Properties
        #region Constructors

        public VehicleLite(IVehicle vehicle, ELanguage language, Func<object, string> localise, Func<int, string> getTag)
        {
            GaijinId = vehicle.GaijinId;
            Name = vehicle.ResearchTreeName.GetLocalisation(language);
            Nation = localise(vehicle.Nation.AsEnumerationItem);
            Country = localise(vehicle.Country);
            Branch = localise(vehicle.Branch.AsEnumerationItem);
            Rank = vehicle.RankAsEnumerationItem;
            BattleRatingInArcade = vehicle.BattleRating.Arcade ?? -EDecimal.Number.One;
            BattleRatingInRealistic = vehicle.BattleRating.Realistic ?? -EDecimal.Number.One;
            BattleRatingInSimulator = vehicle.BattleRating.Simulator ?? -EDecimal.Number.One;
            Class = localise(vehicle.Class);
            Subclass1 = localise(vehicle.Subclasses.First);
            Subclass2 = localise(vehicle.Subclasses.Second);
            Tag1 = getTag(EInteger.Number.Zero);
            Tag2 = getTag(EInteger.Number.One);
            Tag3 = getTag(EInteger.Number.Two);
            IsResearchable = vehicle.IsResearchable;
            IsReserve = vehicle.IsReserve;
            IsSquadronVehicle = vehicle.IsSquadronVehicle;
            UnlockCostInResearch = (IsResearchable || IsSquadronVehicle)
                && vehicle.EconomyData is VehicleEconomyData
                && vehicle.EconomyData.UnlockCostInResearch.HasValue
                && vehicle.EconomyData.UnlockCostInResearch.Value.IsPositive()
                    ? vehicle.EconomyData.UnlockCostInResearch.Value
                    : IsReserve ? EInteger.Number.Zero : -EInteger.Number.One;
            PurchaseCostInSilver = (IsResearchable || IsSquadronVehicle)
                && vehicle.EconomyData is VehicleEconomyData
                && vehicle.EconomyData.PurchaseCostInSilver.IsPositive()
                    ? vehicle.EconomyData.PurchaseCostInSilver
                    : IsReserve ? EInteger.Number.Zero : -EInteger.Number.One;
            IsHiddenUnlessOwned = vehicle.IsHiddenUnlessOwned;
            IsPremium = vehicle.IsPremium;
            IsPurchasableForGoldenEagles = vehicle.IsPurchasableForGoldenEagles;
            PurchaseCostInGold = IsPurchasableForGoldenEagles && (IsPremium && vehicle.EconomyData.PurchaseCostInGold.HasValue || IsSquadronVehicle && vehicle.EconomyData.PurchaseCostInGoldAsSquadronVehicle.HasValue)
                ? (IsPremium ? vehicle.EconomyData.PurchaseCostInGold.Value : vehicle.EconomyData.PurchaseCostInGoldAsSquadronVehicle.Value)
                : -EInteger.Number.One;
            MinimumPurchaseCostInGold = IsSquadronVehicle && vehicle.EconomyData.DiscountedPurchaseCostInGoldAsSquadronVehicle.HasValue
                ? vehicle.EconomyData.DiscountedPurchaseCostInGoldAsSquadronVehicle.Value
                : -EInteger.Number.One;
            GiftedToNewPlayersForSelectingTheirFirstBranch = vehicle.GiftedToNewPlayersForSelectingTheirFirstBranch;
            IsSoldInTheStore = vehicle.IsSoldInTheStore;
            IsSoldOnTheMarket = vehicle.IsSoldOnTheMarket;
            IsAvailableOnlyOnConsoles = vehicle.IsAvailableOnlyOnConsoles;
            RegularCrewTrainingCost = vehicle.EconomyData.BaseCrewTrainCostInSilver;
            ExpertCrewTrainingCost = vehicle.EconomyData.ExpertCrewTrainCostInSilver;
            AceCrewTrainingCostInResearch = vehicle.EconomyData.AceCrewTrainCostInResearch;
            AceCrewTrainingCostInGold = vehicle.EconomyData.AceCrewTrainCostInGold;
            RepairCostInArcade = vehicle.EconomyData.RepairCost.Arcade ?? -EInteger.Number.One;
            RepairCostInRealistic = vehicle.EconomyData.RepairCost.Realistic ?? -EInteger.Number.One;
            RepairCostInSimulator = vehicle.EconomyData.RepairCost.Simulator ?? -EInteger.Number.One;
        }

        #endregion Constructors
    }
}