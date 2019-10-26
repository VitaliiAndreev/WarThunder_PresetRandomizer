using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Enumerations
{
    /// <summary> Contains static dictionaries with reference data. </summary>
    public class EReference
    {
        #region Constants

        /// <summary> The maximum amount of crew slots available only for Silver Lions. </summary>
        public const int MinimumnCrewSlots = EInteger.Number.Two;

        /// <summary> The maximum amount of crew slots available only for Silver Lions. </summary>
        public const int MaximumCrewSlotsForSilver = EInteger.Number.Five;

        /// <summary> The maximum amount of crew slots available for Silver Lions and Golder Eagles. </summary>
        public const int MaximumCrewSlotsForGold = EInteger.Number.Ten;

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
            { ENation.Britain, "country_britain" },
            { ENation.Japan, "country_japan" },
            { ENation.China, "country_china" },
            { ENation.Italy, "country_italy" },
            { ENation.France, "country_france" },
            { ENation.Sweden, "country_sweden" },
        };

        /// <summary> The map of the nation enumeration onto corresponding database values. </summary>
        public static IDictionary<string, ENation> NationsFromString { get; } = new Dictionary<string, ENation>
        {
            { "country_0", ENation.None },
            { "country_usa", ENation.Usa },
            { "country_germany", ENation.Germany },
            { "country_ussr", ENation.Ussr },
            { "country_britain", ENation.Britain },
            { "country_japan", ENation.Japan },
            { "country_china", ENation.China},
            { "country_italy", ENation.Italy },
            { "country_france", ENation.France },
            { "country_sweden", ENation.Sweden },
        };

        /// <summary> The map of the country enumeration onto corresponding database values. </summary>
        public static IDictionary<string, ECountry> CountriesFromString { get; } = new Dictionary<string, ECountry>
        {
            { "country_argentina", ECountry.Argentina },
            { "country_australia", ECountry.Australia },
            { "country_belgium", ECountry.Belgium },
            { "country_britain", ECountry.Britain },
            { "country_bulgaria", ECountry.Bulgaria },
            { "country_canada", ECountry.DominionOfCanada },
            { "country_canada_modern", ECountry.Canada },
            { "country_china", ECountry.China },
            { "country_cuba", ECountry.Cuba },
            { "country_czech", ECountry.CzechRepublic },
            { "country_finland", ECountry.Finland },
            { "country_france", ECountry.France },
            { "country_gdr", ECountry.Gdr },
            { "country_germany", ECountry.NaziGermany },
            { "country_germany_modern", ECountry.Germany },
            { "country_greece", ECountry.Greece },
            { "country_hungary", ECountry.Hungary },
            { "country_iraq", ECountry.Iraq },
            { "country_israel", ECountry.Israel },
            { "country_italy", ECountry.KingdomOfItaly },
            { "country_italy_modern", ECountry.Italy },
            { "country_japan", ECountry.EmpireOfJapan },
            { "country_japan_modern", ECountry.Japan },
            { "country_netherlands", ECountry.Netherlands },
            { "country_new_zealand", ECountry.NewZealand },
            { "country_north_korea", ECountry.NorthKorea },
            { "country_norway", ECountry.Norway },
            { "country_philippines", ECountry.Philippines },
            { "country_poland", ECountry.Poland },
            { "country_portugal", ECountry.Portugal },
            { "country_romania", ECountry.Romania },
            { "country_russia", ECountry.Russia },
            { "country_south_africa", ECountry.SouthAfrica },
            { "country_sweden", ECountry.Sweden },
            { "country_thailand", ECountry.Thailand },
            { "country_ukraine", ECountry.Ukraine },
            { "country_usa", ECountry.Usa },
            { "country_ussr", ECountry.Ussr },
            { "country_yugoslavia", ECountry.Yugoslavia },
        };

        /// <summary> Nation special character icons. </summary>
        public static IDictionary<ENation, char> NationIcons { get; } = new Dictionary<ENation, char>
        {
            { ENation.None, ECharacter.Space },
            { ENation.Usa, '▃' },
            { ENation.Germany, '▀' },
            { ENation.Ussr, '▂' },
            { ENation.Britain, '▄' },
            { ENation.Japan, '▅' },
            { ENation.China, '␗' },
            { ENation.Italy, '▄' },
            { ENation.France, '▄' },
            { ENation.Sweden, '⌉' },
        };

        /// <summary> The map of the country enumeration onto corresponding resource keys. </summary>
        public static IDictionary<ECountry, string> CountryIconsKeys { get; }

        /// <summary> Countries grouped by nations they are aligned with. </summary>
        public static IDictionary<ENation, IEnumerable<ECountry>> CountriesByNation { get; }

        /// <summary> Nations grouped by countries aligned with them. </summary>
        public static IDictionary<ECountry, IEnumerable<ENation>> NationsByCountry { get; }

        /// <summary> The map of the military branch enumeration onto corresponding database values. </summary>
        public static IDictionary<EBranch, string> BranchesFromEnumeration { get; } = new Dictionary<EBranch, string>
        {
            { EBranch.Army, "tank" },
            { EBranch.Aviation, "aircraft" },
            { EBranch.Fleet, "ship" },
            { EBranch.Helicopters, "helicopter" },
        };

        /// <summary> The map of the military branch enumeration onto corresponding database values. </summary>
        public static IDictionary<string, EBranch> BranchesFromString { get; } = new Dictionary<string, EBranch>
        {
            { "tank", EBranch.Army },
            { "aircraft", EBranch.Aviation },
            { "ship", EBranch.Fleet },
            { "helicopter", EBranch.Helicopters },
        };

        /// <summary> Vehicle branch special character icons. </summary>
        public static IDictionary<EBranch, char> BranchIcons { get; } = new Dictionary<EBranch, char>
        {
            { EBranch.Army, '╤' },
            { EBranch.Aviation, '┏' },
            { EBranch.Fleet, '┚' },
            { EBranch.Helicopters, '⋡' },
        };

        /// <summary> Vehicle branch special character icons. </summary>
        public static IDictionary<EVehicleClass, char> ClassIcons { get; } = new Dictionary<EVehicleClass, char>
        {
            { EVehicleClass.LightTank, '┪' },
            { EVehicleClass.MediumTank, '┬' },
            { EVehicleClass.HeavyTank, '┨' },
            { EVehicleClass.TankDestroyer, '┴' },
            { EVehicleClass.Spaa, '┱' },
            { EVehicleClass.AttackHelicopter, '┞' },
            { EVehicleClass.UtilityHelicopter, '┠' },
            { EVehicleClass.Fighter, '┤' },
            { EVehicleClass.Attacker, '┞' },
            { EVehicleClass.Bomber, '┠' },
            { EVehicleClass.Boat, '␉' },
            { EVehicleClass.HeavyBoat, '␊' },
            { EVehicleClass.Barge, '␋' },
            { EVehicleClass.Destroyer, '␌' },
            { EVehicleClass.LightCruiser, '␎' },
            { EVehicleClass.HeavyCruiser, '␏' },
        };

        #endregion Properties
        #region Constructors

        static EReference()
        {
            CountryIconsKeys = new Dictionary<ECountry, string>();
            CountriesByNation = new Dictionary<ENation, IEnumerable<ECountry>>();
            NationsByCountry = new Dictionary<ECountry, IEnumerable<ENation>>();
        }

        #endregion Constructors
    }
}