using Client.Wpf.Commands.MainWindow.Interfaces;
using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Extensions;

namespace Client.Wpf.Commands.MainWindow
{
    /// <summary> A command for generating vehicle presets. </summary>
    public class SwitchToResearchTreeCommand : Command, ISwitchToResearchTreeCommand
    {
        #region Properties

        public IVehicle FocusedVehicle { get; private set; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public SwitchToResearchTreeCommand()
            : base(ECommandName.SwitchToResearchTree)
        {
        }

        #endregion Constructors
        #region Methods: Initialisation

        public ISwitchToResearchTreeCommand With(IVehicle focusedVehicle)
        {
            FocusedVehicle = focusedVehicle;

            return this;
        }

        #endregion Methods: Initialisation

        /// <summary> Defines the method that determines whether the command can execute in its current state. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            if (!base.CanExecute(parameter))
                return false;

            if (!(parameter is IMainWindowPresenter presenter))
                return false;

            return presenter.GeneratedPresets.IsEmpty() || FocusedVehicle is IVehicle && presenter.CanBeBroughtIntoView(FocusedVehicle);
        }

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is IMainWindowPresenter presenter && presenter.ReferencedVehicle is IVehicle)
            {
                presenter.BringIntoView(presenter.ReferencedVehicle);
                presenter.ReferencedVehicle = null;
            }
        }
    }
}