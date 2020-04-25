using Client.Wpf.Commands.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;

namespace Client.Wpf.Commands.MainWindow.Interfaces
{
    public interface ISwitchToResearchTreeCommand : ICustomCommand
    {
        #region Properties

        IVehicle FocusedVehicle { get; }

        #endregion Properties
        #region Methods: Initialisation

        ISwitchToResearchTreeCommand With(IVehicle focusedVehicle);

        #endregion Methods: Initialisation
    }
}