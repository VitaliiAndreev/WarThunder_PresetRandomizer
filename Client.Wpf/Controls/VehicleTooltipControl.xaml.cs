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
        #region Methods: Overrides

        public override void Localise()
        {
            base.Localise();

            static string localise(string key) => ApplicationHelpers.LocalisationManager.GetLocalisedString(key);

            _repairCostHeader.Text = localise(ELocalisationKey.RepairCost);
            _researchMultiplierHeader.Text = localise(ELocalisationKey.ResearchGainMultiplier);
            _silverMultiplierHeader.Text = localise(ELocalisationKey.SilverGainMultiplier);
        }

        #endregion Methods: Overrides
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

            _fullName.Text = _vehicle.FullName?.GetLocalisation(WpfSettings.LocalizationLanguage) ?? _vehicle.GaijinId;
            _class.Text = _displayStrategy.GetVehicleCardClassRow(_vehicle);
            _tags.Text = _displayStrategy.GetVehicleCardTagRow(_vehicle);
            _countryRankAndBattleRating.Text = _displayStrategy.GetVehicleCardCountryRow(_vehicle);
            _requirements.Text = _displayStrategy.GetVehicleCardRequirementsRow(_vehicle);

            _regularCrewRequirements.Text = _displayStrategy.GetVehicleCardRegularCrewRequirements(_vehicle);
            _expertCrewRequirements.Text = _displayStrategy.GetVehicleCardExpertCrewRequirements(_vehicle);
            _aceCrewRequirements.Text = _displayStrategy.GetVehicleCardAceCrewRequirements(_vehicle);
            _researchMultiplier.Text = _vehicle.EconomyData.ResearchGainMultiplier.ToString("0.00");

            if (string.IsNullOrWhiteSpace(_tags.Text))
                _tags.Visibility = Visibility.Collapsed;
        }

        private void SetPortrait()
        {
            if (_vehicle.Images?.PortraitBytes is byte[])
                _portrait.Source = ApplicationHelpers.Manager.GetPortraitBitmapSource(_vehicle);
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
                _tooltipPanel.Children.Insert(_tooltipPanel.Children.IndexOf(_gridBelowPortrait), requiredVehiclePanel);
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

            AddRequiredVehicle();

            IsInitialised = true;
        }

        #endregion Methods: Initialisation
        #region Methods: Updating

        public void UpdateFor(EGameMode gameMode)
        {
            if (gameMode != _gameMode)
            {
                _gameMode = gameMode;

                _tooltipBattleRating.Text = _displayStrategy.GetBattleRating(_gameMode, _vehicle);
                _repairCost.Text = _displayStrategy.GetVehicleCardRepairCost(_vehicle, gameMode);

                if (_vehicle.EconomyData.RewardMultiplier[gameMode] is decimal a )
                    _silverMultiplier.Text = a.ToString(EFormat.Multiplier);

                _requiredVehicle?.UpdateFor(_gameMode);
            }
        }

        #endregion Methods: Updating
    }
}