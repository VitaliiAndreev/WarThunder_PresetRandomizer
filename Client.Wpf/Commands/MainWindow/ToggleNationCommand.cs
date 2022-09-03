using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core;
using Core.WarThunderExtractionToolsIntegration;
using System.Linq;

namespace Client.Wpf.Commands.MainWindow
{
    /// <summary> A command for toggling nations. </summary>
    public class ToggleNationCommand : Command
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public ToggleNationCommand()
            : base(ECommandName.ToggleNation)
        {
        }

        #endregion Constructors

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is IMainWindowPresenter presenter)
            {
                if (!WpfSettings.EnabledNationsCollection.SequenceEqual(presenter.EnabledNations))
                    ApplicationHelpers.SettingsManager.Save(nameof(WpfSettings.EnabledNations), presenter.EnabledNations.StringJoin(Settings.Separator));
            }
        }
    }
}