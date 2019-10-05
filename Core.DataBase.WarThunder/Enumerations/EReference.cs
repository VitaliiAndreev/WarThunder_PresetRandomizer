using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Enumerations
{
    /// <summary> Contains static dictionaries with reference data. </summary>
    public class EReference
    {
        /// <summary> The maximum <see cref="IVehicle.EconomicRank"/> (zero-based) equivalent to battle rating 10.0. </summary>
        public const int MaximumEconomicRank = 27;

        /// <summary> The map of the nation enumeration onto corresponding database values. </summary>
        public static IDictionary<ENation, string> NationsFromEnumeration { get; } = new Dictionary<ENation, string>
        {
            { ENation.None, "country_0" },
            { ENation.Usa, "country_usa" },
            { ENation.Germany, "country_germany" },
            { ENation.Ussr, "country_ussr" },
            { ENation.Commonwealth, "country_britain" },
            { ENation.Japan, "country_japan" },
            { ENation.China, "country_china" },
            { ENation.Italy, "country_italy" },
            { ENation.France, "country_france" },
        };

        /// <summary> The map of the nation enumeration onto corresponding database values. </summary>
        public static IDictionary<string, ENation> NationsFromString { get; } = new Dictionary<string, ENation>
        {
            { "country_0", ENation.None },
            { "country_usa", ENation.Usa },
            { "country_germany", ENation.Germany },
            { "country_ussr", ENation.Ussr },
            { "country_britain", ENation.Commonwealth },
            { "country_japan", ENation.Japan },
            { "country_china", ENation.China},
            { "country_italy", ENation.Italy },
            { "country_france", ENation.France },
        };

        /// <summary> Nation special character icons. </summary>
        public static IDictionary<ENation, char> NationIcons { get; } = new Dictionary<ENation, char>
        {
            { ENation.None, ECharacter.Space },
            { ENation.Usa, '▃' },
            { ENation.Germany, '▀' },
            { ENation.Ussr, '▂' },
            { ENation.Commonwealth, '▄' },
            { ENation.Japan, '▅' },
            { ENation.China, '␗' },
            { ENation.Italy, '▄' },
            { ENation.France, '▄' },
        };

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
    }
}