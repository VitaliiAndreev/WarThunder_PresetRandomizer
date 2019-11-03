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
        public ECountry Country { get; }

        /// <summary> The vehicle's broadly defined class with a distict icon. </summary>
        public EVehicleClass Class { get; }

        /// <summary> Indicates whether the vehicle is premium or not. </summary>
        bool IsPremium { get; }

        /// <summary> Whether this vehicle is hidden from those that don't own it. </summary>
        bool ShowOnlyWhenBought { get; }

        /// <summary> The category of hidden vehicles this one belongs to. </summary>
        string CategoryOfHiddenVehicles { get; }

        /// <summary> The purchase requirement that grants ownerhip of this vehicle. </summary>
        string OwnershipPurchasePrerequisite { get; }

        /// <summary>
        /// The custom research category that this vehicle is unlocked with.
        /// NULL means that standard research is used.
        /// <para>
        /// This property had been introduced with special squadron vehicles that are researched with squadron activity instead of the normal research,
        /// or are purchased with Golden Eagles, with discount (see <see cref="DiscountedPurchaseCostInGold"/>) if some research progress is made.
        /// </para>
        /// </summary>
        string ResearchUnlockType { get; }

        /// <summary> The price of purchasing the vehicle with Golden Eagles. </summary>
        int? PurchaseCostInGold { get; }

        /// <summary> The price of purchasing a squadron-researchable vehicle (see <see cref="ResearchUnlockType"/>) after some progress towards its unlocking is made. </summary>
        int? DiscountedPurchaseCostInGold { get; }

        /// <summary> The Gaijin ID of the vehicle that has to be researched / unlocked before this one can be purchased. </summary>
        string RequiredVehicleGaijinId { get; }

        #endregion General
        #region Rank

        /// <summary> The vehicle's research rank. </summary>
        int Rank { get; }

        #endregion Rank

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> A set of vehicle tags. </summary>
        VehicleTagSet Tags { get; }

        /// <summary> The vehicle's nation. </summary>
        INation Nation { get; }

        /// <summary> The vehicle's branch. </summary>
        IBranch Branch { get; }

        /// <summary> [OBSOLETE, NOW AN INTERNAL VALUES] The vehicle's economic rank (the predecessor of the <see cref="BattleRating"/>). The battle rating is being calculated from this. Economic ranks start at 0 and go up with a step of 1. </summary>
        VehicleGameModeParameterSet.Integer.EconomicRank EconomicRank { get; }

        /// <summary> Values used for matchmaking (falling into a ± 1.0 battle rating bracket). </summary>
        VehicleGameModeParameterSet.Decimal.BattleRating BattleRating { get; }

        /// <summary> A set of information pertaining to the research tree. </summary>
        VehicleResearchTreeData ResearchTreeData { get; }

        /// <summary> The full name of the vehicle. </summary>
        IVehicleLocalization FullName { get; }

        /// <summary> The name of the vehicle shown in the research tree. </summary>
        IVehicleLocalization ResearchTreeName { get; }

        /// <summary> The short name of the vehicle. </summary>
        IVehicleLocalization ShortName { get; }

        /// <summary> The name of the vehicle's <see cref="Class"/>. </summary>
        IVehicleLocalization ClassName { get; }

        #endregion Association Properties
        #region Non-Persistent Properties

        /// <summary> Returns the <see cref="Rank"/> as an item of <see cref="ERank"/>. </summary>
        ERank RankAsEnumerationItem { get; }

        /// <summary> Values used for matchmaking (falling into a ± 1.0 battle rating bracket). </summary>
        VehicleGameModeParameterSet.String.BattleRating BattleRatingFormatted { get; }

        /// <summary> Indicates whether the vehicle can be unlocked for free with research. </summary>
        bool NotResearchable { get; }

        /// <summary> Indicates whether the vehicle can be unlocked with squadron research. </summary>
        bool IsSquadronVehicle { get; }

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
        /// <param name="className"> The name of the vehicle's <see cref="Class"/>. </param>
        void InitializeLocalization(IList<string> fullName, IList<string> researchTreeName, IList<string> shortName, IList<string> className);

        #endregion Methods: Initialization
    }
}