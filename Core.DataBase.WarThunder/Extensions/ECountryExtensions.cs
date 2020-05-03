using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Extensions
{
    public static class ECountryExtensions
    {
        /// <summary> Checks whether the country is valid. </summary>
        /// <param name="country"> The country to check. </param>
        /// <returns></returns>
        public static bool IsValid(this ECountry country) =>
            country.CastTo<int>() > EInteger.Number.NinetyNine;

        /// <summary> Returns all nations that have vehicles of the given <paramref name="country"/>. </summary>
        /// <param name="country"> The country to search by. </param>
        /// <returns></returns>
        public static IEnumerable<ENation> GetNations(this ECountry country) =>
            EReference.NationsByCountry.TryGetValue(country, out var nations)
                ? nations
                : new List<ENation>();
    }
}