using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using System.Linq;

namespace Client.Wpf.Commands.MainWindow
{
    /// <summary> A command for deleting generated presets. </summary>
    public class DeletePresetsCommand : Command
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public DeletePresetsCommand()
            : base(ECommandName.DeletePresets)
        {
        }

        #endregion Constructors

        /// <summary> Defines the method that determines whether the command can execute in its current state. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            if (!base.CanExecute(parameter))
                return false;

            if (!(parameter is IMainWindowPresenter presenter))
                return false;

            return presenter.GeneratedPresets.Any(preset => preset.Value.Any()); ;
        }

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is IMainWindowPresenter presenter)
            {
                presenter.GeneratedPresets.Clear();
                presenter.ResetPresetControls();
                presenter.ResetResearchTreeTabRestrictions();
            }
        }
    }
}