namespace Core.DataBase.WarThunder.Enumerations
{
    /// <summary> Vehicle tags that span <see cref="EBranch"/>es. </summary>
    public enum EVehicleBranchTag
    {
        None = -1,
        All = 0,

        AllGroundVehicles = 100,
        UntaggedGroundVehicle = 101,
        Wheeled = 102,
        Scout = 103,

        AllHelicopters = 200,

        AllAircraft = 300,
        UntaggedAircraft = 301,
        NavalAircraft = 302,
        Hydroplane = 303,
        TorpedoBomber = 304,

        AllFleet = 400,

        AllBluewaterFleet = 410,

        AllCoastalFleet = 420,
    }
}