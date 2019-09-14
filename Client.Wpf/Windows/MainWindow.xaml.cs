using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Client.Wpf.Windows.Interfaces.Base;
using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.WarThunderExtractionToolsIntegration;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Windows
{
    /// <summary> Interaction logic for MainWindow.xaml. </summary>
    public partial class MainWindow : BaseWindow, IMainWindow
    {
        #region Properties

        /// <summary> An instance of a presenter. </summary>
        new public IMainWindowPresenter Presenter { get; private set; }

        /// <summary> An instance of a presenter. </summary>
        IPresenter IWindowWithPresenter.Presenter => Presenter;

        #endregion Properties
        #region Constructor

        /// <summary> Creates a new main window. </summary>
        /// <param name="presenter"> The presenter to attach. </param>
        public MainWindow(IMainWindowPresenter presenter)
            : base(EWpfClientLogCategory.MainWindow)
        {
            Presenter = presenter;
            Presenter.SetParentWindow(this);

            InitializeComponent();
            Localize();

            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;

            _gameModeSelectionControl.ArcadeButtonClick += OnGameModeButtonClick;
            _gameModeSelectionControl.RealisticButtonClick += OnGameModeButtonClick;
            _gameModeSelectionControl.SimulatorButtonClick += OnGameModeButtonClick;

            if (!string.IsNullOrWhiteSpace(WpfSettings.CurrentGameMode))
                SelectGameMode(WpfSettings.CurrentGameMode.ParseEnumeration<EGameMode>(), true);
        }

        #endregion Constructor
        #region Methods: Event Handlers

        /// <summary> Logs showing of the window. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnLoaded(object sender, RoutedEventArgs eventArguments) =>
            Log.Debug(ECoreLogMessage.Shown);

        /// <summary> Logs closing of the window. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnClosed(object sender, EventArgs eventArguments) =>
            Log.Debug(ECoreLogMessage.Closed);

        /// <summary> Selects the game mode whose button is pressed. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="Button"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnGameModeButtonClick(object sender, RoutedEventArgs eventArguments)
        {
            if (!(sender is Button button))
                return;

            if (!(button.Tag is EGameMode buttonGameMode))
                return;

            SelectGameMode(buttonGameMode);
        }

        #endregion Methods: Event Handlers

        /// <summary> Applies localization to visible text in the window. </summary>
        public override void Localize()
        {
            Title = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.ApplicationName);

            _generatePresetButton.Content = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.GeneratePreset);

            _gameModeSelectionControl.Localize();
        }

        /// <summary> Selects the specified game mode. </summary>
        /// <param name="gameMode"> The game mode to select. </param>
        /// <param name="simulateClick"> Whether to simulate a click on the appropriate button. </param>
        private void SelectGameMode(EGameMode gameMode, bool simulateClick = false)
        {
            if (simulateClick)
                _gameModeSelectionControl.OnClick(_gameModeSelectionControl.GetButton(gameMode), new RoutedEventArgs());

            Presenter.CurrentGameMode = gameMode;
            Presenter.ExecuteCommand(ECommandName.SelectGameMode);
        }
    }
}