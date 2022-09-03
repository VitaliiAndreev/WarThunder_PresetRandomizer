using Core.DataBase.WarThunder.Enumerations;

namespace Core.DataBase.WarThunder.Objects.Connectors
{
    public class NationBranchPair
    {
        #region Properties

        /// <summary> The nation. </summary>
        public ENation Nation { get; }

        /// <summary> The branch. </summary>
        public EBranch Branch { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new nation-branch pair. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="branch"> The branch. </param>
        public NationBranchPair(ENation nation, EBranch branch)
        {
            Nation = nation;
            Branch = branch;
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

            return Equals((NationBranchPair)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Nation * 397) ^ (int)Branch;
            }
        }

        public override string ToString() => $"{Nation}_{Branch}";

        protected bool Equals(NationBranchPair other)
        {
            return
                Nation == other.Nation &&
                Branch == other.Branch;
        }
    }
}