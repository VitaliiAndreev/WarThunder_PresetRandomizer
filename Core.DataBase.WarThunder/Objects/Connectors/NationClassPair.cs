using Core.DataBase.WarThunder.Enumerations;

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
        
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != this.GetType())
                return false;

            return Equals((NationClassPair)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Nation * 397) ^ (int)Class;
            }
        }

        public override string ToString() => $"{Nation}_{Class}";

        protected bool Equals(NationClassPair other)
        {
            return
                Nation == other.Nation &&
                Class == other.Class;
        }
    }
}