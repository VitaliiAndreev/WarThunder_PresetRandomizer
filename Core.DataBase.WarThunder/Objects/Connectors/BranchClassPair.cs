using Core.DataBase.WarThunder.Enumerations;

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

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((BranchClassPair)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Branch * 397) ^ (int)Class;
            }
        }

        public override string ToString() => $"{Branch}_{Class}";

        protected bool Equals(BranchClassPair other)
        {
            return
                Branch == other.Branch &&
                Class == other.Class;
        }
    }
}