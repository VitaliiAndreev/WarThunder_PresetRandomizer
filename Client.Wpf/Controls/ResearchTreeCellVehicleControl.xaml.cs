using Client.Wpf.Controls.Strategies.Interfaces;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeCellVehicleControl.xaml. </summary>
    public partial class ResearchTreeCellVehicleControl : UserControl
    {
        #region Fields

        /// <summary> The research type of the <see cref="Vehicle"/> in the cell. </summary>
        private readonly EVehicleResearchType _reseachType;

        /// <summary> A strategy for generating a formatted string with <see cref="IVehicle"/> information for the given <see cref="EGameMode"/>. </summary>
        private readonly IDisplayVehicleInformationStrategy _displayVehicleInformationStrategy;

        /// <summary> Whether to display the <see cref="Vehicle"/>'s <see cref="IVehicle.Country"/> flag. </summary>
        private readonly bool _useCountryFlag;

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
        /// <param name="vehicle"> The vehicle positioned in the cell. </param>
        /// <param name="displayVehicleInformationStrategy"> The strategy for generating a formatted string with <see cref="IVehicle"/> information for the given <see cref="EGameMode"/>. </param>
        /// <param name="type"> The type of the vehicle card. </param>
        /// <param name="isToggled"> Whether the vehicles is toggled on, i.e. participating in randomisation. </param>
        public ResearchTreeCellVehicleControl(IVehicle vehicle, IDisplayVehicleInformationStrategy displayVehicleInformationStrategy, EVehicleCard type, bool isToggled)
            : this()
        {
            Type = type;
            _displayVehicleInformationStrategy = displayVehicleInformationStrategy;

            Vehicle = vehicle;
            _name.Text = Vehicle.ResearchTreeName.GetLocalization(WpfSettings.LocalizationLanguage);
            _useCountryFlag = Vehicle.Country != Vehicle.Nation.AsEnumerationItem.GetBaseCountry();

            if (Vehicle.IsSquadronVehicle)
                _reseachType = EVehicleResearchType.Squadron;
            else if (Vehicle.IsPremium)
                _reseachType = EVehicleResearchType.Premium;
            else
                _reseachType = EVehicleResearchType.Regular;

            IsToggled = isToggled;

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
        private void OnClick(object sender, MouseButtonEventArgs eventArguments) =>
            HandleClick();

        #endregion Methods: Event Handlers

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

        /// <summary> Displays vehicle information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode for which to display the information. </param>
        public void DisplayVehicleInformation(EGameMode gameMode)
        {
            _informationTextBlock.Text = _displayVehicleInformationStrategy.GetFormattedVehicleInformation(gameMode, Vehicle);

            if (_useCountryFlag && _countryFlag.Source is null)
            {
                _countryFlag.Source = Application.Current.MainWindow.FindResource(EReference.CountryIconKeys[Vehicle.Country]) as ImageSource;

                // The size and margin are being set here to avoid breaking white-space when there is no source provided.
                _countryFlag.SetSize(14);
                _countryFlag.Margin = new Thickness(5, 0, 0, 0);
            }
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
            _border.Opacity = IsToggled ? EDouble.Number.One : EDouble.Number.Quarter;
    }
}