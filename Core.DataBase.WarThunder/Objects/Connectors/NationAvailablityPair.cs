﻿using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects.Connectors
{
    public class NationAvailablityPair
    {
        #region Properties

        public ENation Nation { get; }

        public EVehicleAvailability Availability { get; }

        #endregion Properties
        #region Constructors

        public NationAvailablityPair(ENation nation, EVehicleAvailability availability)
        {
            Nation = nation;
            Availability = availability;
        }

        #endregion Constructors
        #region Methods: Equality Comparison

        public override bool Equals(object obj)
        {
            if (!(obj is NationAvailablityPair otherPair))
                return false;

            return Nation.Equals(otherPair.Nation)
                && Availability.Equals(otherPair.Availability);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = EInteger.Number.PrimesAboveHundred.First();

                hash = hash * EInteger.Number.PrimesAboveHundred.Second() + Nation.GetHashCode();
                hash = hash * EInteger.Number.PrimesAboveHundred.Third() + Availability.GetHashCode();

                return hash;
            }
        }

        #endregion Methods: Equality Comparison

        public override string ToString() => $"{Nation}{ECharacter.Underscore}{Availability}";
    }
}