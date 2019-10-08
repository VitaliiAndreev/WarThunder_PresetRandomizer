using Core.DataBase.WarThunder.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Enumerations
{
    /// <summary>
    /// Broadly defined <see cref="IVehicle"/> classes with distict icons.
    /// Values must consist of a digit designating the parent <see cref="EBranch"/> and a digit designating an ID of the class within its parent branch.
    /// <see cref="None"/> is an exception.
    /// </summary>
    public enum EVehicleClass
    {
        None = 0,
        LightTank = 10,
        MediumTank = 11,
        HeavyTank = 12,
        TankDestroyer = 13,
        Spaa = 14,
        AttackHelicopter = 20,
        UtilityHelicopter = 21,
        Fighter = 30,
        Attacker = 31,
        Bomber = 32,
        Boat = 40,
        HeavyBoat = 41,
        Barge = 42,
        Destroyer = 43,
        LightCruiser = 44,
        HeavyCruiser = 45,
    }
}