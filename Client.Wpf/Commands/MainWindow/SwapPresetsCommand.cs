using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.Extensions;
using Core.Organization.Enumerations;
using System.Linq;

namespace Client.Wpf.Commands.MainWindow
{
    /// <summary> A command for swapping primary and fallback presets. </summary>
    public class SwapPresetsCommand : Command
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public SwapPresetsCommand()
            : base(ECommandName.SwapPresets)
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

            return presenter.GeneratedPresets.HasSeveral(preset => preset.Value.Any());
        }

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is IMainWindowPresenter presenter)
            {
                presenter.CurrentPreset = presenter.CurrentPreset == EPreset.Primary ? EPreset.Fallback : EPreset.Primary;
                presenter.DisplayPreset(presenter.CurrentPreset);
            }
        }
    }
}