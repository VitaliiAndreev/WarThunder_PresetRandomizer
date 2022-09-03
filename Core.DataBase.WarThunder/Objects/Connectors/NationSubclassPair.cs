using Core.DataBase.WarThunder.Enumerations;

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

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != this.GetType())
                return false;

            return Equals((NationSubclassPair)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Nation * 397) ^ (int)Subclass;
            }
        }

        public override string ToString() => $"{Nation}_{Subclass}";

        protected bool Equals(NationSubclassPair other)
        {
            return
                Nation == other.Nation &&
                Subclass == other.Subclass;
        }
    }
}