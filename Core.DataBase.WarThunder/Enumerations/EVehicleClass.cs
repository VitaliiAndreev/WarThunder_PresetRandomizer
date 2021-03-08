using Core.DataBase.WarThunder.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Enumerations
{
    /// <summary>
    /// Broadly defined <see cref="IVehicle"/> classes with distict icons.
    /// Valid values must consist of a digit designating the parent <see cref="EBranch"/> and a digit designating an ID of the class within its parent branch.
    /// <see cref="None"/> is an exception.
    /// </summary>
    public enum EVehicleClass
    {
        None = -1,
        All = 0,

        AllGroundVehicles = 100,
        LightTank = 101,
        MediumTank = 102,
        HeavyTank = 103,
        TankDestroyer = 104,
        Spaa = 105,

        AllHelicopters = 200,
        AttackHelicopter = 201,
        UtilityHelicopter = 202,

        AllAircraft = 300,
        Fighter = 301,
        Attacker = 302,
        Bomber = 303,

        AllFleet = 400,

        AllBluewaterFleet = 410,
        Destroyer = 411,
        LightCruiser = 412,
        HeavyCruiser = 413,
        Battlecruiser = 414,
        Battleship = 415,

        AllCoastalFleet = 420,
        Boat = 421,
        HeavyBoat = 422,
        Barge = 423,
        Frigate = 424,
    }
}