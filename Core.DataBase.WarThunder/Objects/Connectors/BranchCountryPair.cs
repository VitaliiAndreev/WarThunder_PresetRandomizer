using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects.Connectors
{
    public class BranchCountryPair
    {
        #region Properties

        public EBranch Branch { get; }

        public ECountry Country { get; }

        #endregion Properties
        #region Constructors

        public BranchCountryPair(EBranch branch, ECountry country)
        {
            Branch = branch;
            Country = country;
        }

        #endregion Constructors
        #region Methods: Equality Comparison

        public override bool Equals(object obj)
        {
            if (!(obj is BranchCountryPair otherPair))
                return false;

            return Branch.Equals(otherPair.Branch)
                && Country.Equals(otherPair.Country);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Integer.Number.PrimesAboveHundred.First();

                hash = hash * Integer.Number.PrimesAboveHundred.Second() + Branch.GetHashCode();
                hash = hash * Integer.Number.PrimesAboveHundred.Third() + Country.GetHashCode();

                return hash;
            }
        }

        #endregion Methods: Equality Comparison

        public override string ToString() => $"{Branch}{Character.Underscore}{Country}";
    }
}