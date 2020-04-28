using Client.Wpf.Commands.MainWindow;
using Client.Wpf.Enumerations;
using Client.Wpf.Strategies.Interfaces;
using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Strategies
{
    /// <summary> A strategy that is used to set commands available in the <see cref="IMainWindow"/>. </summary>
    public class MainWindowStrategy : Strategy, IMainWindowStrategy
    {
        /// <summary> Initializes and assigns commands to be used with this strategy. </summary>
        protected override void InitializeCommands()
        {
            base.InitializeCommands();

            _commands.Add(ECommandName.GeneratePreset, new GeneratePresetCommand());
            _commands.Add(ECommandName.SelectRandomisation, new SelectRandomisationCommand());
            _commands.Add(ECommandName.SelectGameMode, new SelectGameModeCommand());
            _commands.Add(ECommandName.ToggleBranch, new ToggleBranchCommand());
            _commands.Add(ECommandName.ToggleVehicleBranchTag, new ToggleVehicleBranchTagCommand());
            _commands.Add(ECommandName.ToggleCountry, new ToggleCountryCommand());
            _commands.Add(ECommandName.ToggleNation, new ToggleNationCommand());
            _commands.Add(ECommandName.ToggleVehicleClass, new ToggleVehicleClassCommand());
            _commands.Add(ECommandName.ToggleVehicleSubclass, new ToggleVehicleSubclassCommand());
            _commands.Add(ECommandName.ToggleRank, new ToggleRankCommand());
            _commands.Add(ECommandName.ChangeBattleRating, new ChangeBattleRatingCommand());
            _commands.Add(ECommandName.SwapPresets, new SwapPresetsCommand());
            _commands.Add(ECommandName.DeletePresets, new DeletePresetsCommand());
            _commands.Add(ECommandName.ToggleVehicle, new ToggleVehicleCommand());
            _commands.Add(ECommandName.GoToWiki, new GoToWikiCommand());
            _commands.Add(ECommandName.SwitchToResearchTree, new SwitchToResearchTreeCommand());
            _commands.Add(ECommandName.SwitchIncludeHeadersOnRowCopyFlag, new SwitchIncludeHeadersOnRowCopyFlagCommand());
            _commands.Add(ECommandName.OpenSettings, new OpenSettingsCommand());
            _commands.Add(ECommandName.ChangeLocalization, new ChangeLocalizationCommand());
            _commands.Add(ECommandName.LinkToYouTube, new LinkToYouTubeCommand());
            _commands.Add(ECommandName.About, new AboutCommand());
        }
    }
}