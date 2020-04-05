using Core.DataBase.WarThunder.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Enumerations
{
    /// <summary>
    /// <see cref="IVehicle"/> sub-classes.
    /// Valid values must consist of a digit designating the parent <see cref="EBranch"/>, <see cref="EVehicleClass"/>, and a digit designating an ID of the subclass within its parent class.
    /// <see cref="None"/> is an exception.
    /// </summary>
    public enum EVehicleSubclass
    {
        None = -1,
        All = 0,
        AllLightTanks = 10,
        AllMediumTanks = 11,
        AllHeavyTanks = 12,
        AllTankDestroyers = 13,
        TankDestroyer = 131,
        AntiTankMissileCarrier = 132,
        AllSpaas = 14,
        AllAttackHelicopters = 20,
        AllUtilityHelicopters = 21,
        AllFighters = 30,
        Fighter = 301,
        Interceptor = 302,
        AirDefenceFighter = 303,
        StrikeFighter = 304,
        JetFighter = 305,
        AllAttackers = 31,
        AllBombers = 32,
        LightBomber = 321,
        DiveBomber = 322,
        Bomber = 323,
        FrontlineBomber = 324,
        LongRangeBomber = 325,
        JetBomber = 326,
        AllBoats = 40,
        MotorGunBoat = 401,
        MotorTorpedoBoat = 402,
        AllHeavyBoats = 41,
        ArmoredGunBoat = 411,
        MotorTorpedoGunBoat = 412,
        SubChaser = 413,
        AllBarges = 42,
        AllFrigates = 43,
        AllDestroyers = 44,
        AllLightCruisers = 45,
        AllHeavyCruisers = 46,
    }
}