using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects.Connectors
{
    public class BranchClassPair
    {
        #region Properties

        public EBranch Branch { get; }

        public EVehicleClass Class { get; }

        #endregion Properties
        #region Constructors

        public BranchClassPair(EBranch branch, EVehicleClass vehicleClass)
        {
            Branch = branch;
            Class = vehicleClass;
        }

        #endregion Constructors
        #region Methods: Equality Comparison

        public override bool Equals(object obj)
        {
            if (!(obj is BranchClassPair otherPair))
                return false;

            return Branch.Equals(otherPair.Branch)
                && Class.Equals(otherPair.Class);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Integer.Number.PrimesAboveHundred.First();

                hash = hash * Integer.Number.PrimesAboveHundred.Second() + Branch.GetHashCode();
                hash = hash * Integer.Number.PrimesAboveHundred.Third() + Class.GetHashCode();

                return hash;
            }
        }

        #endregion Methods: Equality Comparison

        public override string ToString() => $"{Branch}{Character.Underscore}{Class}";
    }
}