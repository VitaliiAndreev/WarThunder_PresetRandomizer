using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeCellVehicleControl.xaml. </summary>
    public partial class ResearchTreeCellVehicleControl : UserControl
    {
        #region Properties

        /// <summary> The vehicle in the cell. </summary>
        public IVehicle Vehicle { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        /// <param name="vehicle"> The vehicle positioned in the cell. </param>
        public ResearchTreeCellVehicleControl(IVehicle vehicle)
        {
            InitializeComponent();

            Vehicle = vehicle;
            _name.Text = Vehicle.GaijinId;
        }

        #endregion Constructors

        /// <summary> Displays the <see cref="IVehicle.BattleRating"/> for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode for which to display the battle rating. </param>
        public void DisplayBattleRatingFor(EGameMode gameMode)
        {
            _battleRating.Text = Vehicle.BattleRatingFormatted[gameMode];
        }
    }
}