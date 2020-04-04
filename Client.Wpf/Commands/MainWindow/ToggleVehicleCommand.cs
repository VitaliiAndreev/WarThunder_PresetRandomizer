using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.Extensions;
using Core.WarThunderExtractionToolsIntegration;
using System.Linq;

namespace Client.Wpf.Commands.MainWindow
{
    /// <summary> A command for toggling vehicle classes. </summary>
    public class ToggleVehicleCommand : Command
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public ToggleVehicleCommand()
            : base(ECommandName.ToggleVehicle)
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
                if (!WpfSettings.EnabledVehiclesCollection.SequenceEqual(presenter.EnabledVehicleGaijinIds))
                    ApplicationHelpers.SettingsManager.Save(nameof(WpfSettings.EnabledVehicles), presenter.EnabledVehicleGaijinIds.StringJoin(Settings.Separator));
            }
        }
    }
}