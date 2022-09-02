using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects.Connectors
{
    public class NationSubclassPair
    {
        #region Properties

        public ENation Nation { get; }

        public EVehicleSubclass Subclass { get; }

        #endregion Properties
        #region Constructors

        public NationSubclassPair(ENation nation, EVehicleSubclass subclass)
        {
            Nation = nation;
            Subclass = subclass;
        }

        #endregion Constructors
        #region Methods: Equality Comparison

        public override bool Equals(object obj)
        {
            if (!(obj is NationSubclassPair otherPair))
                return false;

            return Nation.Equals(otherPair.Nation)
                && Subclass.Equals(otherPair.Subclass);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Integer.Number.PrimesAboveHundred.First();

                hash = hash * Integer.Number.PrimesAboveHundred.Second() + Nation.GetHashCode();
                hash = hash * Integer.Number.PrimesAboveHundred.Third() + Subclass.GetHashCode();

                return hash;
            }
        }

        #endregion Methods: Equality Comparison

        public override string ToString() => $"{Nation}{Character.Underscore}{Subclass}";
    }
}