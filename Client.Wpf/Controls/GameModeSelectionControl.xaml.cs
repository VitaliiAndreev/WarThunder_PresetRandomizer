using Core.DataBase.WarThunder.Enumerations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for GameModeSelectionControl.xaml. </summary>
    public partial class GameModeSelectionControl : UserControl
    {
        #region Events

        /// <summary> Occurs when the "Arcade" button is clicked. </summary>
        public event RoutedEventHandler ArcadeButtonClick
        {
            add => _arcadeButton.Click += value;
            remove => _arcadeButton.Click -= value;
        }

        /// <summary> Occurs when the "Realistic" button is clicked. </summary>
        public event RoutedEventHandler RealisticButtonClick
        {
            add => _realisticButton.Click += value;
            remove => _realisticButton.Click -= value;
        }

        /// <summary> Occurs when the "Realistic" button is clicked. </summary>
        public event RoutedEventHandler SimulatorButtonClick
        {
            add => _simulatorButton.Click += value;
            remove => _simulatorButton.Click -= value;
        }

        #endregion Events
        #region Constuctors

        /// <summary> Creates a new control. </summary>
        public GameModeSelectionControl()
        {
            InitializeComponent();

            _arcadeButton.EmbeddedButton.Tag = EGameMode.Arcade;
            _realisticButton.EmbeddedButton.Tag = EGameMode.Realistic;
            _simulatorButton.EmbeddedButton.Tag = EGameMode.Simulator;

            ArcadeButtonClick += OnClick;
            RealisticButtonClick += OnClick;
            SimulatorButtonClick += OnClick;
        }

        #endregion Constuctors
        #region Methods: Event Handlers

        /// <summary> Adjust font weights to indicate the button pressed last. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="Button"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        public void OnClick(object sender, RoutedEventArgs eventArguments)
        {
            if (!(sender is ToggleButton clickedButton))
            {
                return;
            }
            if (clickedButton.IsChecked.HasValue && !clickedButton.IsChecked.Value)
            {
                clickedButton.IsChecked = true;
                return;
            }

            var buttons = _buttonGrid.Children.OfType<DropCapToggleButton>().Select(dropCapButton => dropCapButton.EmbeddedButton);

            buttons
                .Except(new ToggleButton[] { clickedButton })
                .ToList()
                .ForEach(button => button.IsChecked = false)
            ;
        }

        #endregion Methods: Event Handlers

        /// <summary> Gets the button related to the given enumeration item. </summary>
        /// <param name="gameMode"> The game mode whose button to get. </param>
        /// <returns></returns>
        public ToggleButton GetButton(EGameMode gameMode) =>
            _buttonGrid
                .Children
                .OfType<DropCapToggleButton>()
                .Select(dropCapButton => dropCapButton.EmbeddedButton)
                .First(button => button.Tag is EGameMode buttonGameMode && buttonGameMode == gameMode)
            ;
    }
}