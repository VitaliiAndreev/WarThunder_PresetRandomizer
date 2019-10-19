using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Extensions
{
    public static class ECountryExtensions
    {
        /// <summary> Returns all nations that have vehicles of the given <paramref name="country"/>. </summary>
        /// <param name="country"> The country to search by. </param>
        /// <returns></returns>
        public static IEnumerable<ENation> GetNations(this ECountry country) =>
            EReference.NationsByCountry.TryGetValue(country, out var nations)
                ? nations
                : new List<ENation>();
    }
}