using Client.Shared.Attributes;
using Client.Shared.LiteObjectProfiles;
using Core.DataBase.WarThunder.Enumerations;

namespace Client.Shared.Interfaces
{
    public interface IVehicleLite
    {
        [ShowVehicleProperty(EVehicleProfile.None)]
        string GaijinIdLite { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        string Name { get; }

        [ShowVehicleProperty(EVehicleProfile.None | EVehicleProfile.Nation | EVehicleProfile.NationAndBranch | EVehicleProfile.NationAndClass | EVehicleProfile.NationAndCountry | EVehicleProfile.NationAndSubclass | EVehicleProfile.NationAndTag)]
        string Nation { get; }

        [ShowVehicleProperty(EVehicleProfile.None | EVehicleProfile.Country | EVehicleProfile.BranchAndCountry | EVehicleProfile.NationAndCountry)]
        string Country { get; }

        [ShowVehicleProperty(EVehicleProfile.None | EVehicleProfile.Branch | EVehicleProfile.BranchAndClass | EVehicleProfile.BranchAndCountry | EVehicleProfile.NationAndBranch)]
        string Branch { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        ERank Rank { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        decimal BattleRatingInArcade { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        decimal BattleRatingInRealistic { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        decimal BattleRatingInSimulator { get; }

        [ShowVehicleProperty(EVehicleProfile.None | EVehicleProfile.Class | EVehicleProfile.BranchAndClass | EVehicleProfile.NationAndClass)]
        string Class { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        string Subclass1 { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        string Subclass2 { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        string Tag1 { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        string Tag2 { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        string Tag3 { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        bool IsResearchable { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        bool IsReserve { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        bool IsSquadronVehicle { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        int UnlockCostInResearch { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        int PurchaseCostInSilver { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        bool IsHiddenUnlessOwned { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        bool IsPremium { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        bool IsPurchasableForGoldenEagles { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        int PurchaseCostInGold { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        int MinimumPurchaseCostInGold { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        bool GiftedToNewPlayersForSelectingTheirFirstBranch { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        bool IsSoldInTheStore { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        bool IsSoldOnTheMarket { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        bool IsAvailableOnlyOnConsoles { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        public int RegularCrewTrainingCost { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        public int ExpertCrewTrainingCost { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        public int AceCrewTrainingCostInResearch { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        public int AceCrewTrainingCostInGold { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        public int RepairCostInArcade { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        public int RepairCostInRealistic { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        public int RepairCostInSimulator { get; }

        [ShowVehicleProperty(EVehicleProfile.None)]
        public decimal ResearchGainMultiplierLite { get; }
    }
}