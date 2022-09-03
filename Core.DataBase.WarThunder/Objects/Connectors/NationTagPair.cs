using Core.DataBase.WarThunder.Enumerations;

namespace Core.DataBase.WarThunder.Objects.Connectors
{
    public class NationTagPair
    {
        #region Properties

        public ENation Nation { get; }

        public EVehicleBranchTag Tag { get; }

        #endregion Properties
        #region Constructors

        public NationTagPair(ENation nation, EVehicleBranchTag tag)
        {
            Nation = nation;
            Tag = tag;
        }

        #endregion Constructors
        
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != this.GetType())
                return false;

            return Equals((NationTagPair)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Nation * 397) ^ (int)Tag;
            }
        }

        public override string ToString() => $"{Nation}_{Tag}";

        protected bool Equals(NationTagPair other)
        {
            return
                Nation == other.Nation &&
                Tag == other.Tag;
        }
    }
}