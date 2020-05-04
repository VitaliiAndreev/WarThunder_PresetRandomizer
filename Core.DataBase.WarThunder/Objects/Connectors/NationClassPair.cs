using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects.Connectors
{
    public class NationClassPair
    {
        #region Properties

        public ENation Nation { get; }

        public EVehicleClass Class { get; }

        #endregion Properties
        #region Constructors

        public NationClassPair(ENation nation, EVehicleClass vehicleClass)
        {
            Nation = nation;
            Class = vehicleClass;
        }

        #endregion Constructors
        #region Methods: Equality Comparison

        public override bool Equals(object obj)
        {
            if (!(obj is NationClassPair otherPair))
                return false;

            return Nation.Equals(otherPair.Nation)
                && Class.Equals(otherPair.Class);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = EInteger.Number.PrimesAboveHundred.First();

                hash = hash * EInteger.Number.PrimesAboveHundred.Second() + Nation.GetHashCode();
                hash = hash * EInteger.Number.PrimesAboveHundred.Third() + Class.GetHashCode();

                return hash;
            }
        }

        #endregion Methods: Equality Comparison

        public override string ToString() => $"{Nation}{ECharacter.Underscore}{Class}";
    }
}