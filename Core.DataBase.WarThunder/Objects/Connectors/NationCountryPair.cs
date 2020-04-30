using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Enumerations.Logger;
using Core.Enumerations;
using Core.Extensions;
using System;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects.Connectors
{
    /// <summary> A pair representing a nation and a contry that has vehicles in service with that nation. </summary>
    public class NationCountryPair
    {
        #region Properties

        /// <summary> The nation. </summary>
        public ENation Nation { get; private set; }

        /// <summary> The country. </summary>
        public ECountry Country { get; private set; }

        #endregion Properties
        #region Constructors

        public NationCountryPair(string nationCountryString)
        {
            var strings = nationCountryString.Split(ECharacter.Underscore, StringSplitOptions.RemoveEmptyEntries);

            if (strings.Count() != EInteger.Number.Two)
                throw new ArgumentException(EDatabaseWarThunderLogMessage.NationCountryFormatIsInvalid.FormatFluently(nationCountryString));

            Initialize(strings.First(), strings.Last());
        }

        /// <summary> Parses given strings into a new instance of a nation-country pair. </summary>
        /// <param name="nationString"> The string to parse a nation from. </param>
        /// <param name="countryString"> The string to parse a country from. </param>
        public NationCountryPair(string nationString, string countryString)
        {
            Initialize(nationString, countryString);
        }

        /// <summary> Creates a new nation-country pair. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="country"> The country. </param>
        public NationCountryPair(ENation nation, ECountry country)
        {
            Initialize(nation, country);
        }

        /// <summary> Creates a new nation-country pair by copying the given. </summary>
        /// <param name="nationCountryPair"> The nation-country pair to copy. </param>
        public NationCountryPair(NationCountryPair nationCountryPair)
        {
            Initialize(nationCountryPair.Nation, nationCountryPair.Country);
        }

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Initializes the class with the given values. </summary>
        /// <param name="nationString"> The string to parse a nation from. </param>
        /// <param name="countryString"> The string to parse a country from. </param>
        private void Initialize(string nationString, string countryString)
        {
            Initialize(nationString.ParseEnumeration<ENation>(), countryString.ParseEnumeration<ECountry>());
        }

        /// <summary> Initializes the class with the given values. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="country"> The country. </param>
        private void Initialize(ENation nation, ECountry country)
        {
            Nation = nation;
            Country = country;
        }

        #endregion Methods: Initialization
        #region Methods: Equality Comparison

        /// <summary> Determines whether the specified object is equal to the current object. </summary>
        /// <param name="obj"> The object to compare with the current object. </param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is NationCountryPair otherPair))
                return false;

            return Nation.Equals(otherPair.Nation)
                && Country.Equals(otherPair.Country);
        }

        /// <summary> Serves as the default hash function. </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 101;

                hash = hash * 103 + Nation.GetHashCode();
                hash = hash * 107 + Country.GetHashCode();

                return hash;
            }
        }

        #endregion Methods: Equality Comparison

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString() => $"{Nation}{ECharacter.Underscore}{Country}";
    }
}