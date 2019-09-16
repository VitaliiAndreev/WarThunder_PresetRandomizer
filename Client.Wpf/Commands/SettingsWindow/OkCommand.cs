using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.WarThunderExtractionToolsIntegration;

namespace Client.Wpf.Commands.SettingsWindow
{
    /// <summary> A command for saving changes. </summary>
    public class OkCommand : Command
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public OkCommand()
            : base(ECommandName.Ok)
        {
        }

        #endregion Constructors

        public override bool CanExecute(object parameter)
        {
            if (!base.CanExecute(parameter))
                return false;

            if (!(parameter is ISettingsWindowPresenter presenter))
                return false;

            return presenter.WarThunderLocationIsValid
                && presenter.KlensysWarThunderToolsLocationIsValid;
        }

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="ILoadingWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is ISettingsWindowPresenter presenter)
            {
                ApplicationHelpers.SettingsManager.Save(nameof(Settings.WarThunderLocation), presenter.WarThunderLocation);
                ApplicationHelpers.SettingsManager.Save(nameof(Settings.KlensysWarThunderToolsLocation), presenter.WarThunderLocation);

                presenter.CloseParentWindow();
            }
        }
    }
}