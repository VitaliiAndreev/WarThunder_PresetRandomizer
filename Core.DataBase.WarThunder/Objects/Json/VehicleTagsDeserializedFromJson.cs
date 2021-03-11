using Newtonsoft.Json;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary> A mapping entity used for automated deserialization of JSON data before passing it on into persistent objects. </summary>
    public class VehicleTagsDeserializedFromJson : DeserializedFromJsonWithGaijinId
    {
        [JsonProperty("not_in_dynamic_campaign")]
        public bool NotAvailableInDynamicCampaign { get; set; }

        [JsonProperty("air")]
        public bool IsAirVehicle { get; set; }

        [JsonProperty("ally")]
        public bool IsAllied { get; set; }

        [JsonProperty("type_fighter")]
        public bool IsFighter { get; set; }

        [JsonProperty("type_jet_fighter")]
        public bool IsJetFighter { get; set; }

        [JsonProperty("country_usa")]
        public bool IsAmerican { get; set; }

        [JsonProperty("pacific")]
        public bool UsedInPacific { get; set; }

        [JsonProperty("country_ussr")]
        public bool IsSoviet { get; set; }

        [JsonProperty("korea")]
        public bool UsedInKorea { get; set; }

        [JsonProperty("western_front")]
        public bool UsedOnWesternFront { get; set; }

        [JsonProperty("eastern_front")]
        public bool UsedOnEasternFront { get; set; }

        [JsonProperty("country_france")]
        public bool IsFrench { get; set; }

        [JsonProperty("country_britain")]
        public bool IsBritish { get; set; }

        [JsonProperty("country_germany")]
        public bool IsGerman { get; set; }

        [JsonProperty("axis")]
        public bool IsAxis { get; set; }

        [JsonProperty("type_interceptor")]
        public bool IsInterceptor { get; set; }

        [JsonProperty("mediterranean")]
        public bool UsedInMediterranean { get; set; }

        [JsonProperty("berlin")]
        public bool UsedAtBerlin { get; set; }

        [JsonProperty("far_eastern_front")]
        public bool UsedOnFarEasternFront { get; set; }

        [JsonProperty("korean_front")]
        public bool UsedOnKoreanFront { get; set; }

        [JsonProperty("type_utility_helicopter")]
        public bool IsUtilityHelicopter { get; set; }

        [JsonProperty("type_attack_helicopter")]
        public bool IsAttackHelicopter { get; set; }

        [JsonProperty("country_italy")]
        public bool IsItalian { get; set; }

        [JsonProperty("country_japan")]
        public bool IsJapanese { get; set; }

        [JsonProperty("type_strike_aircraft")]
        public bool IsStrikeAircraft { get; set; }

        [JsonProperty("carrier_take_off")]
        public bool CanTakeOffFromCarrier { get; set; }

        [JsonProperty("type_bomber")]
        public bool IsBomber { get; set; }

        [JsonProperty("type_medium_bomber")]
        public bool IsMediumBomber { get; set; }

        [JsonProperty("type_frontline_bomber")]
        public bool IsFrontlineBomber { get; set; }

        [JsonProperty("bomberview")]
        public bool HasBomberView { get; set; }

        [JsonProperty("britain")]
        public bool UsedInBritain { get; set; }

        [JsonProperty("malta")]
        public bool UsedAtMalta { get; set; }

        [JsonProperty("sicily")]
        public bool UsedAtSicily { get; set; }

        [JsonProperty("bulge")]
        public bool UsedInBattleOfBulge { get; set; }

        [JsonProperty("ruhr")]
        public bool UsedInRuhr { get; set; }

        [JsonProperty("type_torpedo")]
        public bool CanCarryTorpedoes { get; set; }

        [JsonProperty("type_assault")]
        public bool IsAttacker { get; set; }

        [JsonProperty("type_jet_bomber")]
        public bool IsJetBomber { get; set; }

        [JsonProperty("type_longrange_bomber")]
        public bool IsLongRangeBomber { get; set; }

        [JsonProperty("type_heavy_bomber")]
        public bool IsHeavyBomber { get; set; }

        [JsonProperty("iwo_jima")]
        public bool UsedAtIwoJima { get; set; }

        [JsonProperty("midway")]
        public bool UsedInBattleOfMidway { get; set; }

        [JsonProperty("korsun")]
        public bool UsedAtKorsun { get; set; }

        [JsonProperty("type_light_bomber")]
        public bool IsLightBomber { get; set; }

        [JsonProperty("fw_190_series")]
        public bool IsFw190 { get; set; }

        [JsonProperty("stalingrad")]
        public bool UsedAtStalingrad { get; set; }

        [JsonProperty("stalingrad_w")]
        public bool UsedAtStalingrad_ { get; set; }

        [JsonProperty("krymsk")]
        public bool UsedAtKrymsk { get; set; }

        [JsonProperty("country_china")]
        public bool IsChinese { get; set; }

        [JsonProperty("type_dive_bomber")]
        public bool IsDiveBomber { get; set; }

        [JsonProperty("type_aa_fighter")]
        public bool IsAirDefenceFighter { get; set; }

        [JsonProperty("type_naval_aircraft")]
        public bool IsNavalAircraft { get; set; }

        [JsonProperty("guadalcanal")]
        public bool UsedAtGuadalcanal { get; set; }

        [JsonProperty("type_hydroplane")]
        public bool IsHydroplane { get; set; }

        [JsonProperty("cannot_takeoff")]
        public bool CannotTakeoff { get; set; }

        [JsonProperty("honolulu")]
        public bool UsedAtHonolulu { get; set; }

        [JsonProperty("wake_island")]
        public bool UsedAtWakeIsland { get; set; }

        [JsonProperty("khalkin_gol")]
        public bool UsedAtKhalkinGol { get; set; }

        [JsonProperty("country_australia")]
        public bool IsAustralian { get; set; }

        [JsonProperty("country_sweden")]
        public bool IsSwedish { get; set; }

        [JsonProperty("hydroplane")]
        public bool IsHydroplane_ { get; set; }

        [JsonProperty("china")]
        public bool UsedInChina { get; set; }

        [JsonProperty("port_moresby")]
        public bool UsedAtPortMoresby { get; set; }

        [JsonProperty("guam")]
        public bool UsedInGuam { get; set; }

        [JsonProperty("tank")]
        public bool IsTank { get; set; }

        [JsonProperty("type_medium_tank")]
        public bool IsMediumTank { get; set; }

        [JsonProperty("type_light_tank")]
        public bool IsLightTank { get; set; }

        [JsonProperty("scout")]
        public bool CanScout { get; set; }

        [JsonProperty("canRepairAnyAlly")]
        public bool CanRepairTeammates { get; set; }

        [JsonProperty("type_heavy_tank")]
        public bool IsHeavyTank { get; set; }

        [JsonProperty("type_tank_destroyer")]
        public bool IsTankDestroyer { get; set; }

        [JsonProperty("type_missile_tank")]
        public bool IsAtgmCarrier { get; set; }

        [JsonProperty("type_spaa")]
        public bool IsSpaa { get; set; }

        [JsonProperty("has_proximityFuse_rocket")]
        public bool HasProximityFuseRocket { get; set; }

        [JsonProperty("mediterran")]
        public bool UsedInMediterranean_ { get; set; }

        [JsonProperty("westernfront")]
        public bool UsedOnWesternFront_ { get; set; }

        [JsonProperty("easternfront")]
        public bool UsedOnEasternFront_ { get; set; }

        [JsonProperty("ship")]
        public bool IsShip { get; set; }

        [JsonProperty("carrier")]
        public bool IsCarrier { get; set; }

        [JsonProperty("max_ratio")]
        public bool HasMaximumRatio { get; set; }

        #region Fleet

        #region Bluewater

        [JsonProperty("type_battlecruiser")]
        public bool IsBattlecruiser { get; set; }

        [JsonProperty("type_battleship")]
        public bool IsBattleship { get; set; }

        [JsonProperty("type_cruiser")]
        public bool IsCruiser { get; set; }

        [JsonProperty("cruiser")]
        public bool IsCruiser_ { get; set; }

        [JsonProperty("type_destroyer")]
        public bool IsDestroyer { get; set; }

        [JsonProperty("destroyer")]
        public bool IsDestroyer_ { get; set; }

        [JsonProperty("type_heavy_cruiser")]
        public bool IsHeavyCruiser { get; set; }

        [JsonProperty("type_light_cruiser")]
        public bool IsLightCruiser { get; set; }

        [JsonProperty("light_cruiser")]
        public bool IsLightCruiser_ { get; set; }

        #endregion Bluewater
        #region Coastal

        [JsonProperty("type_naval_aa_ferry")]
        public bool IsAaFerry { get; set; }

        [JsonProperty("type_armored_boat")]
        public bool IsArmoredBoat { get; set; }

        [JsonProperty("type_barge")]
        public bool IsBarge { get; set; }

        [JsonProperty("type_boat")]
        public bool IsBoat { get; set; }

        [JsonProperty("boat")]
        public bool IsBoat_ { get; set; }

        [JsonProperty("type_naval_ferry_barge")]
        public bool IsFerry { get; set; }

        [JsonProperty("type_frigate")]
        public bool IsFrigate { get; set; }

        [JsonProperty("type_gun_boat")]
        public bool IsGunBoat { get; set; }

        [JsonProperty("type_heavy_boat")]
        public bool IsHeavyBoat { get; set; }

        [JsonProperty("type_heavy_gun_boat")]
        public bool IsHeavyGunBoat { get; set; }

        [JsonProperty("type_minelayer")]
        public bool IsMinelayer { get; set; }

        [JsonProperty("type_submarine_chaser")]
        public bool IsSubmarineChaser { get; set; }

        [JsonProperty("type_torpedo_boat")]
        public bool IsTorpedoBoat { get; set; }

        [JsonProperty("type_torpedo_gun_boat")]
        public bool IsTorpedoGunBoat { get; set; }

        #endregion Coastal

        #endregion Fleet
    }
}