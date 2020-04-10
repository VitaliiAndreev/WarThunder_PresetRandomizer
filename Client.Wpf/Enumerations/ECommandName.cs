using Client.Wpf.Commands.LoadingWindow;
using Client.Wpf.Commands.LocalizationWindow;
using Client.Wpf.Commands.MainWindow;
using Client.Wpf.Commands.SettingsWindow;

namespace Client.Wpf.Enumerations
{
    /// <summary> Names of available commands. </summary>
    public enum ECommandName
    {
        /// <summary> See <see cref="AboutCommand"/>. </summary>
        About,
        /// <summary> See <see cref="CancelCommand"/>. </summary>
        Cancel,
        /// <summary> See <see cref="ChangeBattleRatingCommand"/>. </summary>
        ChangeBattleRating,
        /// <summary> See <see cref="ChangeLocalizationCommand"/>. </summary>
        ChangeLocalization,
        /// <summary> See <see cref="DeletePresetsCommand"/>. </summary>
        DeletePresets,
        /// <summary> See <see cref="GeneratePresetCommand"/>. </summary>
        GeneratePreset,
        /// <summary> See <see cref="InitializeCommand"/>. </summary>
        Initialize,
        /// <summary> See <see cref="OkCommand"/>. </summary>
        Ok,
        /// <summary> See <see cref="OpenSettingsCommand"/>. </summary>
        OpenSettings,
        /// <summary> See <see cref="SelectLocalizationCommand"/>. </summary>
        SelectLocalization,
        /// <summary> See <see cref="SelectGameModeCommand"/>. </summary>
        SelectGameMode,
        /// <summary> See <see cref="SwapPresetsCommand"/>. </summary>
        SwapPresets,
        /// <summary> See <see cref="ToggleBranchCommand"/>. </summary>
        ToggleBranch,
        /// <summary> See <see cref="ToggleCountryCommand"/>. </summary>
        ToggleCountry,
        /// <summary> See <see cref="ToggleNationCommand"/>. </summary>
        ToggleNation,
        /// <summary> See <see cref="SelectRandomisationCommand"/>. </summary>
        SelectRandomisation,
        /// <summary> See <see cref="ToggleVehicleCommand"/>. </summary>
        ToggleVehicle,
        /// <summary> See <see cref="ToggleVehicleBranchTagCommand"/>. </summary>
        ToggleVehicleBranchTag,
        /// <summary> See <see cref="ToggleVehicleClassCommand"/>. </summary>
        ToggleVehicleClass,
        /// <summary> See <see cref="ToggleVehicleSubclassCommand"/>. </summary>
        ToggleVehicleSubclass,
    }
}