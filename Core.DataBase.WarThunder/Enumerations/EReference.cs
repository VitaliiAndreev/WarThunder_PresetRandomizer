using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;

namespace Core.DataBase.Enumerations
{
    /// <summary> Contains static dictionaries with reference data. </summary>
    public class EReference
    {
        /// <summary> The map of the nation enumeration onto corresponding database values. </summary>
        public static IDictionary<ENation, string> Nations { get; } = new Dictionary<ENation, string>
        {
            { ENation.None, "country_0" },
            { ENation.Usa, "country_usa" },
            { ENation.Germany, "country_germany" },
            { ENation.Ussr, "country_ussr" },
            { ENation.Commonwealth, "country_britain" },
            { ENation.Japan, "country_japan" },
            { ENation.Italy, "country_italy" },
            { ENation.France, "country_france" },
        };

        /// <summary> The map of the military branch enumeration onto corresponding database values. </summary>
        public static IDictionary<EBranch, string> Branches { get; } = new Dictionary<EBranch, string>
        {
            { EBranch.Army, "tank" },
            { EBranch.Aviation, "aircraft" },
            { EBranch.Fleet, "ship" },
            { EBranch.Helicopters, "helicopter" },
        };
    }
}