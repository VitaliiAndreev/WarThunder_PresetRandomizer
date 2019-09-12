using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.WarThunderExtractionToolsIntegration;

namespace Client.Wpf.Commands.MainWindow
{
    public class SelectGameModeCommand : Command
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public SelectGameModeCommand()
            : base(ECommandName.SelectGameMode)
        {
        }

        #endregion Constructors

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is IMainWindowPresenter presenter)
                ApplicationHelpers.SettingsManager.Save(nameof(WpfSettings.CurrentGameMode), presenter.CurrentGameMode.ToString());
        }
    }
}