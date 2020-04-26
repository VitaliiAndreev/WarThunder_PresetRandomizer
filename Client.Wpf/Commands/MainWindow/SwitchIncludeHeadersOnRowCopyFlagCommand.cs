using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;

namespace Client.Wpf.Commands.MainWindow
{
    /// <summary> A command for toggling vehicle classes. </summary>
    public class SwitchIncludeHeadersOnRowCopyFlagCommand : Command
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public SwitchIncludeHeadersOnRowCopyFlagCommand()
            : base(ECommandName.SwitchIncludeHeadersOnRowCopyFlag)
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
                if (WpfSettings.IncludeHeadersOnRowCopyFlag != presenter.IncludeHeadersOnRowCopy)
                    ApplicationHelpers.SettingsManager.Save(nameof(WpfSettings.IncludeHeadersOnRowCopy), presenter.IncludeHeadersOnRowCopy.ToString());
            }
        }
    }
}