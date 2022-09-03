using Core.DataBase.WarThunder.Enumerations;

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

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((BranchCountryPair)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Branch * 397) ^ (int)Country;
            }
        }

        public override string ToString() => $"{Branch}_{Country}";

        protected bool Equals(BranchCountryPair other)
        {
            return
                Branch == other.Branch &&
                Country == other.Country;
        }
    }
}