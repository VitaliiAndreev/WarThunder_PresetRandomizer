using Client.Wpf.Controls.Base;
using Client.Wpf.Controls.Strategies.Interfaces;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleTooltipControl.xaml. </summary>
    public partial class VehicleTooltipControl : LocalisedUserControl
    {
        #region Fields

        private IMainWindowPresenter _presenter;
        private IVehicle _vehicle;
        private EGameMode _gameMode;
        private IDisplayVehicleInformationStrategy _displayStrategy;
        private ResearchTreeCellVehicleControl _requiredVehicle;

        #endregion Fields
        #region Properties

        public bool IsInitialised { get; private set; }

        #endregion Properties
        #region Constructors

        public VehicleTooltipControl()
        {
            InitializeComponent();
        }

        #endregion Constructors
        #region Methods: Initialisation

        private void SetBackground()
        {
            if (_vehicle.IsPremium)
                _underlay.Style = this.GetStyle(EStyleKey.StackPanel.StackPanelPremium);

            else if (_vehicle.IsSquadronVehicle)
                _underlay.Style = this.GetStyle(EStyleKey.StackPanel.StackPanelSquadron);
        }

        private void SetMainText()
        {
            _flag.SetFlag(_vehicle.Country, 16, new Thickness(0, 0, 7, 0));

            _fullName.Text = _vehicle.FullName?.GetLocalization(WpfSettings.LocalizationLanguage) ?? _vehicle.GaijinId;
            _class.Text = _displayStrategy.GetVehicleCardClassRow(_vehicle);
            _countryRankAndBattleRating.Text = _displayStrategy.GetVehicleCardCountryRow(_vehicle);
            _requirements.Text = _displayStrategy.GetVehicleCardRequirementsRow(_vehicle);
        }

        private void SetPortrait()
        {
            if (_vehicle.Images?.PortraitBytes is byte[])
                _portrait.Source = ApplicationHelpers.Manager.GetPortraitBitmapSource(_vehicle);
        }

        private void AddPremiumTag()
        {
            if (_vehicle.IsPremium)
            {
                var premiumTag = new TextBlock
                {
                    Style = this.GetStyle(EStyleKey.TextBlock.TextBlockWithSkyQuake12pxCenterDefault),
                    Text = $"{EGaijinCharacter.Premium}{ECharacter.Space}{ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.Premium)}",
                };
                //_tooltipPanel.Children.Add(premiumTag);
                _tooltipPanel.Children.Insert(_tooltipPanel.Children.IndexOf(_portrait), premiumTag);
            }
        }

        private void AddRequiredVehicle()
        {
            if (_requiredVehicle is ResearchTreeCellVehicleControl)
            {
                var requiredVehiclePanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                var prefix = new TextBlock
                {
                    Style = this.GetStyle(EStyleKey.TextBlock.TextBlockWithSkyQuake12pxRightCenter),
                    Margin = new Thickness(0, 0, 5, 0),
                    Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.Requires),
                };

                requiredVehiclePanel.Children.Add(prefix);
                requiredVehiclePanel.Children.Add(_requiredVehicle);

                _requiredVehicle.Margin = new Thickness(5, 0, 0, 0);
                _tooltipPanel.Children.Add(requiredVehiclePanel);
            }
        }

        public void Initialise(IMainWindowPresenter presenter, IVehicle vehicle, IDisplayVehicleInformationStrategy displayStrategy)
        {
            if (IsInitialised)
                return;

            _presenter = presenter;
            _vehicle = vehicle;
            _displayStrategy = displayStrategy;

            if (!string.IsNullOrWhiteSpace(_vehicle.RequiredVehicleGaijinId) && ApplicationHelpers.Manager.PlayableVehicles.TryGetValue(_vehicle.RequiredVehicleGaijinId, out var requiredVehicle))
                _requiredVehicle = new ResearchTreeCellVehicleControl(_presenter, requiredVehicle, _displayStrategy, EVehicleCard.Preset, true) { ToolTip = null };

            SetBackground();
            SetMainText();
            SetPortrait();

            AddPremiumTag();
            AddRequiredVehicle();

            IsInitialised = true;
        }

        #endregion Methods: Initialisation
        #region Methods: Updating

        public void UpdateFor(EGameMode gameMode)
        {
            if (gameMode != _gameMode)
            {
                _tooltipBattleRating.Text = _displayStrategy.GetBattleRating(gameMode, _vehicle);
                _requiredVehicle?.UpdateFor(gameMode);
                _gameMode = gameMode;
            }
        }

        #endregion Methods: Updating
    }
}