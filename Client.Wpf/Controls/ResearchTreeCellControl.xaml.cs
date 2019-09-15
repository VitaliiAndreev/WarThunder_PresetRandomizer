using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Linq;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeCellControl.xaml. </summary>
    public partial class ResearchTreeCellControl : UserControl
    {
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public ResearchTreeCellControl()
        {
            InitializeComponent();
        }

        #endregion Constructors

        /// <summary> Adds a new control for the specified vehicle and adds it to the the cell. </summary>
        /// <param name="vehicle"> The vehicle to add. </param>
        public void AddVehicle(IVehicle vehicle)
        {
            _stackPanel.Children.Add(new ResearchTreeCellVehicleControl(vehicle));
        }

        /// <summary> Displays <see cref="IVehicle.BattleRating"/> value for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode for which to display the battle rating. </param>
        public void DisplayBattleRatingFor(EGameMode gameMode)
        {
            foreach (var vehicleCell in _stackPanel.Children.OfType<ResearchTreeCellVehicleControl>())
                vehicleCell.DisplayBattleRatingFor(gameMode);
        }
    }
}