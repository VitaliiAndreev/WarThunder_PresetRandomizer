using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;

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
                var hash = 101;

                hash = hash * 103 + Branch.GetHashCode();
                hash = hash * 107 + Country.GetHashCode();

                return hash;
            }
        }

        #endregion Methods: Equality Comparison

        public override string ToString() => $"{Branch}{ECharacter.Underscore}{Country}";
    }
}