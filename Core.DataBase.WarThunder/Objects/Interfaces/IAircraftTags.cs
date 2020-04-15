namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A set of aircraft tags. </summary>
    public interface IAircraftTags : IVehicleTags
    {
        #region Persistent Properties

        bool IsUntagged { get; }

        bool IsNavalAircraft { get; }

        bool IsHydroplane { get; }

        bool IsTorpedoBomber { get; }

        #endregion Persistent Properties
    }
}