using Core.DataBase.WarThunder.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Enumerations
{
    /// <summary>
    /// <see cref="IVehicle"/> sub-classes.
    /// Valid values must consist of a digit designating the parent <see cref="EBranch"/>, <see cref="EVehicleClass"/>, and a digit designating an ID of the class within its parent branch.
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
        AllSpaas = 14,
        AllAttackHelicopters = 20,
        AllUtilityHelicopters = 21,
        AllFighters = 30,
        Fighter = 301,
        Interceptor = 302,
        NightFighter = 303,
        StrikeFighter = 304,
        JetFighter = 305,
        AllAttackers = 31,
        AllBombers = 32,
        AllBoats = 40,
        AllHeavyBoats = 41,
        AllBarges = 42,
        AllFrigates = 43,
        AllDestroyers = 44,
        AllLightCruisers = 45,
        AllHeavyCruisers = 46,
    }
}