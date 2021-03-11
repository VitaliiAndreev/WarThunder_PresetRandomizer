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

        AllGroundVehicles = 1000,

        AllLightTanks = 1010,

        AllMediumTanks = 1020,

        AllHeavyTanks = 1030,

        AllTankDestroyers = 1040,
        TankDestroyer = 1041,
        AntiTankMissileCarrier = 1042,

        AllSpaas = 1050,

        AllHelicopters = 2000,

        AllAttackHelicopters = 2010,

        AllUtilityHelicopters = 2020,

        AllAircraft = 3000,

        AllFighters = 3010,
        Fighter = 3011,
        Interceptor = 3012,
        AirDefenceFighter = 3013,
        JetFighter = 3014,

        AllAttackers = 3020,
        StrikeAircraft = 3021,

        AllBombers = 3030,
        LightBomber = 3031,
        DiveBomber = 3032,
        Bomber = 3033,
        FrontlineBomber = 3034,
        LongRangeBomber = 3035,
        JetBomber = 3036,

        AllFleet = 4000,

        AllBluewaterFleet = 4100,

        AllDestroyers = 4110,

        AllLightCruisers = 4120,

        AllHeavyCruisers = 4130,

        AllBattlecruisers = 4140,

        AllBattleships = 4150,

        AllCoastalFleet = 4200,

        AllBoats = 4210,
        MotorGunboat = 4211,
        MotorTorpedoBoat = 4212,
        Minelayer = 4213,

        AllHeavyBoats = 4220,
        ArmoredGunboat = 4221,
        MotorTorpedoGunboat = 4222,
        SubChaser = 4223,

        AllBarges = 4230,
        AntiAirFerry = 4231,
        NavalFerryBarge = 4232,

        AllFrigates = 4240,
        HeavyGunboat = 4241,
        Frigate = 4242,
    }
}