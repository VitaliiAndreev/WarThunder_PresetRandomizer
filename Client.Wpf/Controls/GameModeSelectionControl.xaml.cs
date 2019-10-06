using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for GameModeSelectionControl.xaml. </summary>
    public partial class GameModeSelectionControl : LocalizedUserControl
    {
        #region Properties

        /// <summary> The map of the game mode enumeration onto corresponding buttons. </summary>
        public IDictionary<EGameMode, ToggleButton> Buttons { get; }

        #endregion Properties
        #region Events

        /// <summary> A routed event for <see cref="Click"/>. </summary>
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(nameof(Click), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GameModeSelectionControl));

        /// <summary> An event for clicking toggle buttons. </summary>
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        #endregion Events
        #region Constuctors

        /// <summary> Creates a new control. </summary>
        public GameModeSelectionControl()
        {
            InitializeComponent();

            Buttons = new Dictionary<EGameMode, ToggleButton>
            {
                { EGameMode.Arcade, _arcadeButton.EmbeddedButton },
                { EGameMode.Realistic, _realisticButton.EmbeddedButton },
                { EGameMode.Simulator, _simulatorButton.EmbeddedButton },
            };

            foreach(var buttonKeyValuePair in Buttons)
            {
                var gameMode = buttonKeyValuePair.Key;
                var button = buttonKeyValuePair.Value;

                button.Tag = gameMode;
                button.Click += OnClick;
            }
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
            if (!clickedButton.IsChecked ?? false)
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

            RaiseClickEvent(clickedButton);
        }

        #endregion Methods: Event Handlers

        /// <summary> Raises the <see cref="ClickEvent"/> for the specified toggle button. </summary>
        /// <param name="toggleButton"> The toggle button to raise the event for. </param>
        private void RaiseClickEvent(ToggleButton toggleButton) =>
            RaiseEvent(new RoutedEventArgs(ClickEvent, toggleButton));

        /// <summary> Applies localization to visible text on the control. </summary>
        public override void Localize()
        {
            base.Localize();

            _arcadeButton.Caption = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Arcade);
            _realisticButton.Caption = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Realistic);
            _simulatorButton.Caption = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Simulator);
        }
    }
}