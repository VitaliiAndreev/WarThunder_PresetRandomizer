using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;

namespace Client.Wpf.Commands.MainWindow
{
    /// <summary> A command for displaying the current version and contributor data. </summary>
    public class AboutCommand : Command
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public AboutCommand()
            : base(ECommandName.About)
        {
        }

        #endregion Constructors

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is IMainWindowPresenter presenter)
                ApplicationHelpers.WindowFactory.CreateAboutWindow(presenter.Owner).ShowDialog();
        }
    }
}