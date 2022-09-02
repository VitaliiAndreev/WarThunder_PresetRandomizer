using Client.Shared.Wpf.Extensions;
using Client.Wpf.Controls.Strategies;
using Client.Wpf.Controls.Strategies.Interfaces;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeCellVehicleControl.xaml. </summary>
    public partial class ResearchTreeCellVehicleControl : UserControl
    {
        private const double controlOpacityWhenOff = 0.25;
        private const double controlOpacityWhenOn = 1.0;
        private const double vehicleIconOpacity = 0.9;

        #region Fields

        private readonly IMainWindowPresenter _presenter;

        /// <summary> The research type of the <see cref="Vehicle"/> in the cell. </summary>
        private readonly EVehicleResearchType _reseachType;

        /// <summary> A strategy for generating a formatted string with <see cref="IVehicle"/> information for the given <see cref="EGameMode"/>. </summary>
        private readonly IDisplayVehicleInformationStrategy _displayVehicleInformationStrategy;

        /// <summary> Whether to display the <see cref="Vehicle"/>'s <see cref="IVehicle.Country"/> flag. </summary>
        private readonly bool _useCountryFlag;

        private bool _initialised;
        private bool _tooltipInitialised;

        #endregion Fields
        #region Properties

        /// <summary> The type of the vehicle card. </summary>
        internal EVehicleCard Type { get; }

        /// <summary> The vehicle in the cell. </summary>
        internal IVehicle Vehicle { get; }

        internal bool IsToggled { get; private set; }

        #endregion Properties
        #region Events

        /// <summary> A routed event for <see cref="Click"/>. </summary>
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(nameof(Click), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ResearchTreeCellVehicleControl));

        /// <summary> An event for clicking toggle buttons. </summary>
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        #endregion Events
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public ResearchTreeCellVehicleControl()
        {
            InitializeComponent();
        }

        /// <summary> Creates a new control. </summary>
        /// <param name="presenter"> An instance of a main window presenter. </param>
        /// <param name="vehicle"> The vehicle positioned in the cell. </param>
        /// <param name="displayVehicleInformationStrategy"> The strategy for generating a formatted string with <see cref="IVehicle"/> information for the given <see cref="EGameMode"/>. </param>
        /// <param name="type"> The type of the vehicle card. </param>
        /// <param name="isToggled"> Whether the vehicles is toggled on, i.e. participating in randomisation. </param>
        public ResearchTreeCellVehicleControl(IMainWindowPresenter presenter, IVehicle vehicle, IDisplayVehicleInformationStrategy displayVehicleInformationStrategy, EVehicleCard type, bool isToggled)
            : this()
        {
            Type = type;
            _displayVehicleInformationStrategy = displayVehicleInformationStrategy;
            _presenter = presenter;

            Vehicle = vehicle;
            _name.Text = Vehicle?.ResearchTreeName?.GetLocalisation(WpfSettings.LocalizationLanguage) ?? Vehicle.GaijinId;
            _useCountryFlag = Vehicle.Country != Vehicle.Nation.AsEnumerationItem.GetBaseCountry();

            if (Vehicle.IsSquadronVehicle)
                _reseachType = EVehicleResearchType.Squadron;
            else if (Vehicle.IsPremium)
                _reseachType = EVehicleResearchType.Premium;
            else
                _reseachType = EVehicleResearchType.Regular;

            IsToggled = isToggled;

            Initialise();
            ApplyIdleStyle();
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Applies the highlighting style to the <see cref="_border"/>. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="Border"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnMouseEnter(object sender, MouseEventArgs eventArguments)
        {
            if (sender is Border)
                ApplyHighlightStyle();
        }

        /// <summary> Applies the idle style to the <see cref="_border"/>. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="Border"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnMouseLeave(object sender, MouseEventArgs eventArguments)
        {
            if (sender is Border)
                ApplyIdleStyle();
        }

        /// <summary> Calls <see cref="HandleClick"/>. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnClick(object sender, MouseButtonEventArgs eventArguments)
        {
            if (eventArguments.LeftButton == MouseButtonState.Pressed)
                HandleClick();
        }

        private void OnContextMenuItemClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is IVehicle vehicle)
                _presenter.ReferencedVehicle = vehicle;
        }

        #endregion Methods: Event Handlers
        #region Methods: Initialisation

        private void InitialiseContextMenu()
        {
            var contextMenu = new ContextMenu();
            var wikiLinkMenuItem = new MenuItem
            {
                IsCheckable = false,
                StaysOpenOnClick = false,
                Header = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.GoToWiki),
                Tag = Vehicle,
                CommandParameter = _presenter,
                Command = _presenter.GetCommand(ECommandName.GoToWiki),
            };

            wikiLinkMenuItem.Click += OnContextMenuItemClick;

            contextMenu.Items.Add(wikiLinkMenuItem);

            _border.ContextMenu = contextMenu;
        }

        private void Initialise()
        {
            if (_initialised) return;

            if (_useCountryFlag)
                _countryFlag.SetFlag(Vehicle.Country, 14, new Thickness(5, 0, 0, 0));

            if (Vehicle.Images?.IconBytes is byte[])
            {
                _innerGrid.Background = new ImageBrush(ApplicationHelpers.Manager.GetIconBitmapSource(Vehicle))
                {
                    Stretch = Stretch.Uniform,
                    AlignmentX = AlignmentX.Left,
                    AlignmentY = AlignmentY.Center,
                    Opacity = vehicleIconOpacity,
                };
            }

            InitialiseContextMenu();

            _initialised = true;
        }

        #endregion Methods: Initialisation

        /// <summary> Raises the <see cref="ClickEvent"/> for the specified toggle button. </summary>
        public void RaiseClickEvent() =>
            RaiseEvent(new RoutedEventArgs(ClickEvent, this));

        /// <summary> Toggles the control on/off and updates its opacity. </summary>
        internal void HandleClick()
        {
            if (Type == EVehicleCard.ResearchTree)
            {
                IsToggled = !IsToggled;

                UpdateOpacity();
            }
            RaiseClickEvent();
        }

        public void UpdateFor(EGameMode gameMode)
        {
            _informationTextBlock.Text = _displayVehicleInformationStrategy.GetVehicleInfoBottomRow(gameMode, Vehicle);

            if (_tooltipInitialised)
                _tooltip.UpdateFor(gameMode);
        }

        /// <summary> Applies the idle style to the <see cref="_border"/>. </summary>
        internal void ApplyIdleStyle()
        {
            _border.Style = _reseachType switch
            {
                EVehicleResearchType.Squadron => this.GetStyle(EStyleKey.Border.SquadronResearchTreeCell),
                EVehicleResearchType.Premium => this.GetStyle(EStyleKey.Border.PremiumResearchTreeCell),
                _ => this.GetStyle(EStyleKey.Border.ResearchTreeCell),
            };
            UpdateOpacity();
        }

        /// <summary> Applies the highlighting style to the <see cref="_border"/>. </summary>
        internal void ApplyHighlightStyle()
        {
            _border.Style = _reseachType switch
            {
                EVehicleResearchType.Squadron => this.GetStyle(EStyleKey.Border.SquadronResearchTreeCellHighlighted),
                EVehicleResearchType.Premium => this.GetStyle(EStyleKey.Border.PremiumResearchTreeCellHighlighted),
                _ => this.GetStyle(EStyleKey.Border.ResearchTreeCellHighlighted),
            };
            UpdateOpacity();
        }

        /// <summary> Updates the control's opacity according to its <see cref="IsToggled"/> state. </summary>
        private void UpdateOpacity() =>
            _border.Opacity = IsToggled ? controlOpacityWhenOn : controlOpacityWhenOff;

        private void OnTooltipOpening(object sender, ToolTipEventArgs e)
        {
            if (!_tooltipInitialised)
            {
                _tooltip.Initialise(_presenter, Vehicle, new DisplayVehicleInformationStandaloneStrategy());
                _tooltip.Localise();
                _tooltip.UpdateFor(_presenter.CurrentGameMode);
                _tooltipInitialised = true;
            }
        }
    }
}