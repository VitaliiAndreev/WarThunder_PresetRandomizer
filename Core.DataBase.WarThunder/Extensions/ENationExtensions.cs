﻿using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Connectors;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="ENation"/> enumeration. </summary>
    public static class ENationExtensions
    {
        /// <summary> Checks whether the nation is valid. </summary>
        /// <param name="nation"> The nation to check. </param>
        /// <returns></returns>
        public static bool IsValid(this ENation nation) =>
            nation.EnumerationItemValueIsPositive();

        /// <summary> Returns the base country of the given playable <paramref name="nation"/>. </summary>
        /// <param name="nation"> The playable nation whose base country to return. </param>
        /// <returns></returns>
        public static ECountry GetBaseCountry(this ENation nation)
        {
            var presetCountry = nation switch
            {
                ENation.Usa => ECountry.Usa,
                ENation.Germany => ECountry.NaziGermany,
                ENation.Ussr => ECountry.Ussr,
                ENation.GreatBritain => ECountry.GreatBritain,
                ENation.Japan => ECountry.EmpireOfJapan,
                ENation.China => ECountry.China,
                ENation.Italy => ECountry.Italy,
                ENation.France => ECountry.France,
                ENation.Sweden => ECountry.Sweden,
                ENation.Israel => ECountry.Israel,
                _ => ECountry.None,
            };

            if (presetCountry == ECountry.None && nation.ToString().TryParseEnumeration<ECountry>(out var parsedCountry))
                return parsedCountry;

            return presetCountry;
        }

        /// <summary> Returns all countries that have vehicles serving with the given nation. </summary>
        /// <param name="nation"> The nation to search by. </param>
        /// <returns></returns>
        public static IEnumerable<ECountry> GetCountries(this ENation nation) =>
            EReference.CountriesByNation.TryGetValue(nation, out var countries)
                ? countries
                : new List<ECountry>();

        public static IEnumerable<NationCountryPair> GetNationCountryPairs(this ENation nation) =>
            EReference.CountriesByNation.TryGetValue(nation, out var countries)
                ? countries.Select(country => new NationCountryPair(nation, country))
                : new List<NationCountryPair>();

        /// <summary> Returns the enumeration item representing selection of all countries serving with the given nation. </summary>
        /// <param name="nation"> The nation whose item to get. </param>
        /// <returns></returns>
        public static ECountry GetAllCountriesItem(this ENation nation) =>
            nation switch
            {
                ENation.Usa => ECountry.AllUsa,
                ENation.Germany => ECountry.AllGermany,
                ENation.Ussr => ECountry.AllUssr,
                ENation.GreatBritain => ECountry.AllBritain,
                ENation.Japan => ECountry.AllJapan,
                ENation.China => ECountry.AllChina,
                ENation.Italy => ECountry.AllItaly,
                ENation.France => ECountry.AllFrance,
                ENation.Sweden => ECountry.AllSweden,
                ENation.Israel => ECountry.AllIsrael,
                _ => ECountry.None,
            };
    }
}