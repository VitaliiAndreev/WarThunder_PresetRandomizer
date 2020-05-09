using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Json;
using Core.DataBase.WarThunder.Objects.Localization.Vehicle.Interfaces;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A vehicle (air, ground, or sea). </summary>
    public interface IVehicle : IPersistentObjectWithIdAndGaijinId
    {
        #region Persistent Properties

        #region General

        /// <summary> The vehicle's country of origin (not the nation in whose research tree the vehicles is in). </summary>
        ECountry Country { get; }

        /// <summary> The vehicle's broadly defined class with a distict icon. </summary>
        EVehicleClass Class { get; }

        /// <summary> Indicates whether the vehicle can be unlocked for free with research. </summary>
        bool IsResearchable { get; }

        bool IsReserve { get; }

        /// <summary> Indicates whether the vehicle can be unlocked for free with research. </summary>
        bool IsPurchasableForGoldenEagles { get; }

        /// <summary> Indicates whether the vehicle can be unlocked with squadron research. </summary>
        bool IsSquadronVehicle { get; }

        bool IsSoldOnTheMarket { get; }

        bool IsSoldInTheStore { get; }

        /// <summary> Indicates whether the vehicle is premium or not. </summary>
        bool IsPremium { get; }

        /// <summary> Whether this vehicle is hidden from those that don't own it. </summary>
        bool IsHiddenUnlessOwned { get; }

        bool IsAvailableOnlyOnConsoles { get; }

        /// <summary> The category of hidden vehicles this one belongs to. </summary>
        string CategoryOfHiddenVehicles { get; }

        /// <summary> Whether this vehicle is gifted to new players upon selecting their first vehicle branch and completing the tutorial. </summary>
        bool GiftedToNewPlayersForSelectingTheirFirstBranch { get; }

        /// <summary> The Gaijin ID of the vehicle that has to be researched / unlocked before this one can be purchased. </summary>
        string RequiredVehicleGaijinId { get; }

        #endregion General
        #region Rank

        /// <summary> The vehicle's research rank. </summary>
        int Rank { get; }

        #endregion Rank

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle's nation. </summary>
        INation Nation { get; }

        /// <summary> The vehicle's branch. </summary>
        IBranch Branch { get; }

        /// <summary> The vehicle's subclass. </summary>
        IVehicleSubclasses Subclasses { get; }

        /// <summary> [OBSOLETE, NOW INTERNAL VALUES] The vehicle's economic rank (the predecessor of the <see cref="BattleRating"/>). The battle rating is being calculated from this. Economic ranks start at 0 and go up with a step of 1. </summary>
        VehicleGameModeParameterSet.Integer.EconomicRank EconomicRank { get; }

        /// <summary> Values used for matchmaking (falling into a ± 1.0 battle rating bracket). </summary>
        VehicleGameModeParameterSet.Decimal.BattleRating BattleRating { get; }

        /// <summary> A set of vehicle branch tags. </summary>
        IAircraftTags AircraftTags { get; }

        /// <summary> A set of vehicle branch tags. </summary>
        IGroundVehicleTags GroundVehicleTags { get; }

        /// <summary> A set of information pertaining to the research tree. </summary>
        VehicleResearchTreeData ResearchTreeData { get; }

        VehicleEconomyData EconomyData { get; }

        VehiclePerformanceData PerformanceData { get; }

        VehicleCrewData CrewData { get; }

        VehicleWeaponsData WeaponsData { get; }

        VehicleModificationsData ModificationsData { get; }

        VehicleGraphicsData GraphicsData { get; }

        IVehicleImages Images { get; }

        /// <summary> The full name of the vehicle. </summary>
        IVehicleLocalisation FullName { get; }

        /// <summary> The name of the vehicle shown in the research tree. </summary>
        IVehicleLocalisation ResearchTreeName { get; }

        /// <summary> The short name of the vehicle. </summary>
        IVehicleLocalisation ShortName { get; }

        #endregion Association Properties
        #region Non-Persistent Properties

        /// <summary> Returns the <see cref="Rank"/> as an item of <see cref="ERank"/>. </summary>
        ERank RankAsEnumerationItem { get; }

        IEnumerable<EVehicleBranchTag> Tags { get; }

        /// <summary> Values used for matchmaking (falling into a ± 1.0 battle rating bracket). </summary>
        VehicleGameModeParameterSet.String.BattleRating BattleRatingFormatted { get; }

        #endregion Non-Persistent Properties
        #region Methods: Initialization

        /// <summary> Fills properties of the object with values deserialized from JSON data read from "shop.blkx". </summary>
        /// <param name="deserializedResearchTreeVehicle"> The temporary non-persistent object storing deserialized data. </param>
        void InitializeWithDeserializedResearchTreeJson(ResearchTreeVehicleFromJson deserializedResearchTreeVehicle);

        /// <summary> Performs additional initialization with data deserialized from "unittags.blkx". </summary>
        /// <param name="deserializedVehicleData"></param>
        void InitializeWithDeserializedAdditionalVehicleDataJson(VehicleDeserializedFromJsonUnitTags deserializedVehicleData);

        /// <summary> Initializes localization association properties. </summary>
        /// <param name="fullName"> The full name of the vehicle. </param>
        /// <param name="researchTreeName"> The name of the vehicle shown in the research tree. </param>
        /// <param name="shortName"> The short name of the vehicle. </param>
        void InitializeLocalization(IList<string> fullName, IList<string> researchTreeName, IList<string> shortName);

        void SetIcon(byte[] bytes);

        void SetPortrait(byte[] bytes);

        #endregion Methods: Initialization

        IEnumerable<EVehicleAvailability> GetAvailabilityCategories();
    }
}