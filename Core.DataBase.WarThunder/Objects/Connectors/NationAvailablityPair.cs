using Core.DataBase.WarThunder.Enumerations;

namespace Core.DataBase.WarThunder.Objects.Connectors
{
    public class NationAvailablityPair
    {
        #region Properties

        public ENation Nation { get; }

        public EVehicleAvailability Availability { get; }

        #endregion Properties
        #region Constructors

        public NationAvailablityPair(ENation nation, EVehicleAvailability availability)
        {
            Nation = nation;
            Availability = availability;
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

            return Equals((NationAvailablityPair)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Nation * 397) ^ (int)Availability;
            }
        }

        public override string ToString() => $"{Nation}_{Availability}";

        protected bool Equals(NationAvailablityPair other)
        {
            return
                Nation == other.Nation &&
                Availability == other.Availability;
        }
    }
}