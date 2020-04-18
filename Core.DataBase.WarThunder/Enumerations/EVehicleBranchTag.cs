namespace Core.DataBase.WarThunder.Enumerations
{
    /// <summary> Vehicle tags that span <see cref="EBranch"/>es. </summary>
    public enum EVehicleBranchTag
    {
        None = -1,
        All = 0,

        AllGroundVehicles = 1,
        UntaggedGroundVehicle = 11,
        Wheeled = 12,
        Scout = 13,

        AllHelicopters = 2,

        AllAircraft = 3,
        UntaggedAircraft = 31,
        NavalAircraft = 32,
        Hydroplane = 33,
        TorpedoBomber = 34,

        AllShips = 4,
    }
}