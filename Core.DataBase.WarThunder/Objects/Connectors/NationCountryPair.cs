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

            Initialise(strings.First(), strings.Last());
        }

        /// <summary> Parses given strings into a new instance of a nation-country pair. </summary>
        /// <param name="nationString"> The string to parse a nation from. </param>
        /// <param name="countryString"> The string to parse a country from. </param>
        public NationCountryPair(string nationString, string countryString)
        {
            Initialise(nationString, countryString);
        }

        /// <summary> Creates a new nation-country pair. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="country"> The country. </param>
        public NationCountryPair(ENation nation, ECountry country)
        {
            Initialise(nation, country);
        }

        /// <summary> Creates a new nation-country pair by copying the given. </summary>
        /// <param name="nationCountryPair"> The nation-country pair to copy. </param>
        public NationCountryPair(NationCountryPair nationCountryPair)
        {
            Initialise(nationCountryPair.Nation, nationCountryPair.Country);
        }

        #endregion Constructors
        #region Methods: Initialisation

        /// <summary> Initializes the class with the given values. </summary>
        /// <param name="nationString"> The string to parse a nation from. </param>
        /// <param name="countryString"> The string to parse a country from. </param>
        private void Initialise(string nationString, string countryString)
        {
            Initialise(nationString.ParseEnumeration<ENation>(), countryString.ParseEnumeration<ECountry>());
        }

        /// <summary> Initializes the class with the given values. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="country"> The country. </param>
        private void Initialise(ENation nation, ECountry country)
        {
            Nation = nation;
            Country = country;
        }

        #endregion Methods: Initialisation
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
                var hash = EInteger.Number.PrimesAboveHundred.First();

                hash = hash * EInteger.Number.PrimesAboveHundred.Second() + Nation.GetHashCode();
                hash = hash * EInteger.Number.PrimesAboveHundred.Third() + Country.GetHashCode();

                return hash;
            }
        }

        #endregion Methods: Equality Comparison

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString() => $"{Nation}{ECharacter.Underscore}{Country}";
    }
}