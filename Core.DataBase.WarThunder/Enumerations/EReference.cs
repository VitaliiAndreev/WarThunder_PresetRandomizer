using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Enumerations
{
    /// <summary> Contains static dictionaries with reference data. </summary>
    public class EReference
    {
        #region Constants

        /// <summary> The maximum amount of crew slots available only for Silver Lions. </summary>
        public const int MinimumnCrewSlots = Integer.Number.Two;

        /// <summary> The maximum amount of crew slots available only for Silver Lions. </summary>
        public const int MaximumCrewSlotsForSilver = Integer.Number.Five;

        /// <summary> The maximum amount of crew slots available for Silver Lions and Golder Eagles. </summary>
        public const int MaximumCrewSlotsForGold = Integer.Number.Ten;

        #endregion Constants
        #region Properties

        /// <summary> The maximum <see cref="IVehicle.EconomicRank"/> (zero-based). </summary>
        public static int MaximumEconomicRank { get; internal set; }

        /// <summary> The map of the nation enumeration onto corresponding database values. </summary>
        public static IDictionary<ENation, string> NationsFromEnumeration { get; } = new Dictionary<ENation, string>
        {
            { ENation.None, "country_0" },
            { ENation.Usa, "country_usa" },
            { ENation.Germany, "country_germany" },
            { ENation.Ussr, "country_ussr" },
            { ENation.GreatBritain, "country_britain" },
            { ENation.Japan, "country_japan" },
            { ENation.China, "country_china" },
            { ENation.Italy, "country_italy" },
            { ENation.France, "country_france" },
            { ENation.Sweden, "country_sweden" },
            { ENation.Israel, "country_israel" },
        };

        /// <summary> The map of the nation enumeration onto corresponding database values. </summary>
        public static IDictionary<string, ENation> NationsFromString { get; } = new Dictionary<string, ENation>
        {
            { "country_0", ENation.None },
            { "country_usa", ENation.Usa },
            { "country_germany", ENation.Germany },
            { "country_ussr", ENation.Ussr },
            { "country_britain", ENation.GreatBritain },
            { "country_japan", ENation.Japan },
            { "country_china", ENation.China},
            { "country_italy", ENation.Italy },
            { "country_france", ENation.France },
            { "country_sweden", ENation.Sweden },
            { "country_israel", ENation.Israel },
        };

        /// <summary> Nations indexed by string prefixes. </summary>
        public static IDictionary<string, ENation> NationsFromPrefix { get; } = new Dictionary<string, ENation>
        {
            { string.Empty, ENation.None },
            { "us", ENation.Usa },
            { "germ", ENation.Germany },
            { "ussr", ENation.Ussr },
            { "uk", ENation.GreatBritain },
            { "jp", ENation.Japan },
            { "cn", ENation.China},
            { "it", ENation.Italy },
            { "fr", ENation.France },
            { "sw", ENation.Sweden },
        };

        /// <summary> Nation prefixes indexed by nations. </summary>
        public static IDictionary<ENation, string> NationPrefixes { get; } = new Dictionary<ENation, string>
        {
            { ENation.None, string.Empty },
            { ENation.Usa, "us" },
            { ENation.Germany, "germ" },
            { ENation.Ussr, "ussr" },
            { ENation.GreatBritain, "uk" },
            { ENation.Japan, "jp" },
            { ENation.China, "cn"},
            { ENation.Italy, "it" },
            { ENation.France, "fr" },
            { ENation.Sweden, "sw" },
        };

        /// <summary> The map of the country enumeration onto corresponding database values. </summary>
        public static IDictionary<string, ECountry> CountriesFromString { get; } = new Dictionary<string, ECountry>
        {
            { "country_argentina", ECountry.Argentina },
            { "country_australia", ECountry.Australia },
            { "country_austria", ECountry.Austria },
            { "country_bangladesh", ECountry.Bangladesh },
            { "country_belgium", ECountry.Belgium },
            { "country_britain", ECountry.GreatBritain },
            { "country_britain_empire_navy", ECountry.BritishEmpire },
            { "country_bulgaria", ECountry.Bulgaria },
            { "country_canada", ECountry.DominionOfCanada },
            { "country_canada_modern", ECountry.Canada },
            { "country_china", ECountry.China },
            { "country_cuba", ECountry.Cuba },
            { "country_czech", ECountry.CzechRepublic },
            { "country_egypt", ECountry.Egypt },
            { "country_finland", ECountry.Finland },
            { "country_france", ECountry.France },
            { "country_gdr", ECountry.Gdr },
            { "country_germany", ECountry.NaziGermany },
            { "country_germany_empire_navy", ECountry.GermanEmpire },
            { "country_germany_modern", ECountry.Germany },
            { "country_greece", ECountry.Greece },
            { "country_hungary", ECountry.Hungary },
            { "country_iraq", ECountry.Iraq },
            { "country_israel", ECountry.Israel },
            { "country_italy", ECountry.Italy },
            { "country_italy_kingdom", ECountry.KingdomOfItaly },
            { "country_italy_modern", ECountry.Italy },
            { "country_japan", ECountry.EmpireOfJapan },
            { "country_japan_modern", ECountry.Japan },
            { "country_netherlands", ECountry.Netherlands },
            { "country_new_zealand", ECountry.NewZealand },
            { "country_north_korea", ECountry.NorthKorea },
            { "country_norway", ECountry.Norway },
            { "country_philippines", ECountry.Philippines },
            { "country_poland", ECountry.Poland },
            { "country_republic_china", ECountry.RepublicOfChina },
            { "country_portugal", ECountry.Portugal },
            { "country_romania", ECountry.Romania },
            { "country_russia", ECountry.Russia },
            { "country_russia_empire_navy", ECountry.RussianEmpire },
            { "country_south_africa", ECountry.UnionOfSouthAfrica },
            { "country_south_africa_modern", ECountry.RepublicOfSouthAfrica },
            { "country_sweden", ECountry.Sweden },
            { "country_syria", ECountry.Syria },
            { "country_thailand", ECountry.Thailand },
            { "country_turkey", ECountry.Turkey },
            { "country_ukraine", ECountry.Ukraine },
            { "country_usa", ECountry.Usa },
            { "country_usa_modern", ECountry.Usa },
            { "country_ussr", ECountry.Ussr },
            { "country_vietnam", ECountry.Vietnam },
            { "country_yugoslavia", ECountry.Yugoslavia },
        };

        /// <summary> Nation special character icons. </summary>
        public static IDictionary<ENation, char> NationIcons { get; } = new Dictionary<ENation, char>
        {
            { ENation.None, ' ' },
            { ENation.Usa, EGaijinCharacter.StarInCircle },
            { ENation.Germany, EGaijinCharacter.GermanCross },
            { ENation.Ussr, EGaijinCharacter.Star },
            { ENation.GreatBritain, EGaijinCharacter.BritishRoundel },
            { ENation.Japan, EGaijinCharacter.RisingSun },
            { ENation.China, EGaijinCharacter.CommunistStars },
            { ENation.Italy, EGaijinCharacter.ItalianRoundel },
            { ENation.France, EGaijinCharacter.FrenchRoundel },
            { ENation.Sweden, EGaijinCharacter.Globe },
            { ENation.Israel, EGaijinCharacter.StarOfDavid },
        };

        /// <summary> The map of the country enumeration onto corresponding resource keys. </summary>
        public static IDictionary<ECountry, string> CountryIconKeys { get; }

        /// <summary> Countries grouped by nations they are aligned with. </summary>
        public static IDictionary<ENation, IEnumerable<ECountry>> CountriesByNation { get; }

        /// <summary> Nations grouped by countries aligned with them. </summary>
        public static IDictionary<ECountry, IEnumerable<ENation>> NationsByCountry { get; }

        /// <summary> The map of the military branch enumeration onto corresponding database values. </summary>
        public static IDictionary<EVehicleCategory, string> CategoriesFromEnumeration { get; } = new Dictionary<EVehicleCategory, string>
        {
            { EVehicleCategory.Army, "tank" },
            { EVehicleCategory.Aviation, "aircraft" },
            { EVehicleCategory.Fleet, "ship" },
            { EVehicleCategory.Helicopters, "helicopter" },
        };

        /// <summary> The map of the military branch enumeration onto corresponding database values. </summary>
        public static IDictionary<string, EVehicleCategory> CategoriesFromString { get; } = new Dictionary<string, EVehicleCategory>
        {
            { "tank", EVehicleCategory.Army },
            { "aircraft", EVehicleCategory.Aviation },
            { "ship", EVehicleCategory.Fleet },
            { "helicopter", EVehicleCategory.Helicopters },
        };

        /// <summary> Vehicle branch special character icons. </summary>
        public static IDictionary<EBranch, char> BranchIcons { get; } = new Dictionary<EBranch, char>
        {
            { EBranch.Army, EGaijinCharacter.TankFront },
            { EBranch.Helicopters, EGaijinCharacter.AttackHelicopter },
            { EBranch.Aviation, EGaijinCharacter.FighterDiagonal },
            { EBranch.BluewaterFleet, EGaijinCharacter.Battleship },
            { EBranch.CoastalFleet, EGaijinCharacter.Boat },
        };

        public static IDictionary<EBranch, char> SmallerBranchIcons { get; } = new Dictionary<EBranch, char>
        {
            { EBranch.Army, EGaijinCharacter.TankFacingLeft },
            { EBranch.Aviation, EGaijinCharacter.FighterStraight },
            { EBranch.Helicopters, EGaijinCharacter.AttackHelicopter },
            { EBranch.BluewaterFleet, EGaijinCharacter.Anchor },
            { EBranch.CoastalFleet, EGaijinCharacter.Propeller },
        };

        /// <summary> Vehicle branch special character icons. </summary>
        public static IDictionary<EVehicleClass, char> ClassIcons { get; } = new Dictionary<EVehicleClass, char>
        {
            { EVehicleClass.None, ' ' },
            { EVehicleClass.LightTank, EGaijinCharacter.LightTankPip },
            { EVehicleClass.MediumTank, EGaijinCharacter.MediumTankPip },
            { EVehicleClass.HeavyTank, EGaijinCharacter.HeavyTankPip },
            { EVehicleClass.TankDestroyer, EGaijinCharacter.TankDestroyerPip },
            { EVehicleClass.Spaa, EGaijinCharacter.SpaaPip },
            { EVehicleClass.AttackHelicopter, EGaijinCharacter.AttackHelicopterPip },
            { EVehicleClass.UtilityHelicopter, EGaijinCharacter.UtilityHelicopterPip },
            { EVehicleClass.Fighter, EGaijinCharacter.FighterPip },
            { EVehicleClass.Attacker, EGaijinCharacter.AttackerPip },
            { EVehicleClass.Bomber, EGaijinCharacter.BomberPip },
            { EVehicleClass.Boat, EGaijinCharacter.BoatPip },
            { EVehicleClass.HeavyBoat, EGaijinCharacter.HeavyBoatPip },
            { EVehicleClass.Barge, EGaijinCharacter.BargePip },
            { EVehicleClass.Frigate, EGaijinCharacter.FrigatePip },
            { EVehicleClass.Destroyer, EGaijinCharacter.DestroyerPip },
            { EVehicleClass.LightCruiser, EGaijinCharacter.LightCruiserPip },
            { EVehicleClass.HeavyCruiser, EGaijinCharacter.HeavyCruiserPip },
            { EVehicleClass.Battlecruiser, EGaijinCharacter.BattlecruiserPip },
            { EVehicleClass.Battleship, EGaijinCharacter.BattleshipPip },
        };

        /// <summary> String representations of ranks. </summary>
        public static IDictionary<ERank, string> RankIcons { get; } = new Dictionary<ERank, string>
        {
            { ERank.I, "I" },
            { ERank.II, "II" },
            { ERank.III, "III" },
            { ERank.IV, "IV" },
            { ERank.V, "V" },
            { ERank.VI, "VI" },
            { ERank.VII, "VII" },
        };

        public static IDictionary<EVehicleAvailability, string> VehicleAvailabilityIcons { get; } = new Dictionary<EVehicleAvailability, string>
        {
            { EVehicleAvailability.None, string.Empty },
            { EVehicleAvailability.All, EGaijinCharacter.GroundAirSea.ToString() },
            { EVehicleAvailability.Researchable, EGaijinCharacter.Research.ToString() },
            { EVehicleAvailability.Reserve, EGaijinCharacter.Backup.ToString() },
            { EVehicleAvailability.ResearchableInSquadron, EGaijinCharacter.Officer.ToString() },
            { EVehicleAvailability.PurchasableForGoldenEagles, EGaijinCharacter.GoldenEagle.ToString() },
            { EVehicleAvailability.StarterGift, EGaijinCharacter.Trumpet.ToString() },
            { EVehicleAvailability.PurchasableInTheStore, Character.DollarFullwidth.ToString() },
            { EVehicleAvailability.PurchasableOnTheMarket, EGaijinCharacter.GaijinCoin.ToString() },
            { EVehicleAvailability.Premium, EGaijinCharacter.Premium.ToString() },
            { EVehicleAvailability.ConsoleExclusive, EGaijinCharacter.Controller.ToString() },
            { EVehicleAvailability.Hidden, Character.Eye },
        };

        public static IDictionary<ECategory, string> EconomicCategoryIcons { get; } = new Dictionary<ECategory, string>
        {
            { ECategory.RepairCost, $"{EGaijinCharacter.Wrench}{EGaijinCharacter.SilverLion}" },
        };

        #endregion Properties
        #region Constructors

        static EReference()
        {
            CountryIconKeys = new Dictionary<ECountry, string>();
            CountriesByNation = new Dictionary<ENation, IEnumerable<ECountry>>();
            NationsByCountry = new Dictionary<ECountry, IEnumerable<ENation>>();
        }

        #endregion Constructors
    }
}