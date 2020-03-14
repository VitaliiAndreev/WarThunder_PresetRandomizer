namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A set of vehicle tags. </summary>
    public interface IVehicleTagSet : IPersistentWarThunderObjectWithId
    {
        #region Persistent Properties

        bool NotAvailableInDynamicCampaign { get; }

        bool IsAirVehicle { get; }

        bool IsAllied { get; }

        bool IsFighter { get; }

        bool IsJetFighter { get; }

        bool IsAmerican { get; }

        bool UsedInPacific { get; }

        bool IsSoviet { get; }

        bool UsedInKorea { get; }

        bool UsedOnWesternFront { get; }

        bool UsedOnEasternFront { get; }

        bool IsFrench { get; }

        bool IsBritish { get; }

        bool IsGerman { get; }

        bool IsAxis { get; }

        bool IsInterceptor { get; }

        bool IsHeavyFighter { get; }

        bool UsedInMediterranean { get; }

        bool UsedAtBerlin { get; }

        bool UsedOnFarEasternFront { get; }

        bool IsNavalFighter { get; }

        bool UsedOnKoreanFront { get; }

        bool IsUtilityHelicopter { get; }

        bool IsAttackHelicopter { get; }

        bool IsItalian { get; }

        bool IsJapanese { get; }

        bool IsStrikeFighter { get; }

        bool CanTakeOffFromCarrier { get; }

        bool IsBomber { get; }

        bool IsMediumBomber { get; }

        bool IsFrontlineBomber { get; }

        bool HasBomberView { get; }

        bool UsedInBritain { get; }

        bool UsedAtMalta { get; }

        bool UsedAtSicily { get; }

        bool UsedInBattleOfBulge { get; }

        bool UsedInRuhr { get; }

        bool CanCarryTorpedoes { get; }

        bool IsAttacker { get; }

        bool IsJetBomber { get; }

        bool IsLongRangeBomber { get; }

        bool IsHeavyBomber { get; }

        bool UsedAtIwoJima { get; }

        bool UsedInBattleOfMidway { get; }

        bool IsMediumFighter { get; }

        bool UsedAtKorsun { get; }

        bool IsLightBomber { get; }

        bool IsFw190 { get; }

        bool UsedAtStalingrad { get; }

        bool IsLightFighter { get; }

        bool IsBiplane { get; }

        bool UsedAtKrymsk { get; }

        bool IsChinese { get; }

        bool IsDiveBomber { get; }

        bool IsNightFighter { get; }

        bool IsNavalBomber { get; }

        bool UsedAtGuadalcanal { get; }

        bool IsHydroplane { get; }

        bool CannotTakeoff { get; }

        bool UsedAtHonolulu { get; }

        bool UsedAtWakeIsland { get; }

        bool UsedAtKhalkinGol { get; }

        bool IsAustralian { get; }

        bool IsSwedish { get; }

        bool UsedInChina { get; }

        bool UsedAtPortMoresby { get; }

        bool UsedInGuam { get; }

        bool IsTank { get; }

        bool IsMediumTank { get; }

        bool IsLightTank { get; }

        bool CanScout { get; }

        bool CanRepairTeammates { get; }

        bool IsHeavyTank { get; }

        bool IsTankDestroyer { get; }

        bool IsMissileTank { get; }

        bool IsSpaa { get; }

        bool HasProximityFuseRocket { get; }

        bool IsShip { get; }

        bool IsCarrier { get; }

        bool HasMaximumRatio { get; }

        bool IsArmoredBoat { get; }

        bool IsHeavyBoat { get; }

        bool IsArmoredSubmarineChaser { get; }

        bool IsBoat { get; }

        bool IsTorpedoBoat { get; }

        bool IsGunBoat { get; }

        bool IsFerry { get; }

        bool IsBarge { get; }

        bool IsTorpedoGunBoat { get; }

        bool IsDestroyer { get; }

        bool IsMinelayer { get; }

        bool IsHydrofoilTorpedoBoat { get; }

        bool IsMinesweeper { get; }

        bool IsMissileBoat { get; }

        bool IsAaFerry { get; }

        bool IsSubmarineChaser { get; }

        bool IsCruiser { get; }

        bool IsHeavyCruiser { get; }

        bool IsLightCruiser { get; }

        bool IsHeavyGunBoat { get; }

        bool IsSmallSubmarineChaser { get; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle the set belongs to. </summary>
        IVehicle Vehicle { get; }

        #endregion Association Properties
    }
}