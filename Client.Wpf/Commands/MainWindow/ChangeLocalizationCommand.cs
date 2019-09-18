using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Commands.MainWindow
{
    /// <summary> A command for calling an <see cref="ILocalizationWindow"/>. </summary>
    public class ChangeLocalizationCommand : Command
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public ChangeLocalizationCommand()
            : base(ECommandName.ChangeLocalization)
        {

        }

        #endregion Constructors

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is IMainWindowPresenter presenter)
                ApplicationHelpers.WindowFactory.CreateLocalizationWindow(presenter.Owner, true).ShowDialog();
        }
    }
}