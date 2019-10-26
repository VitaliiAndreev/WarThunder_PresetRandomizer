using Client.Wpf.Controls.Base;
using Client.Wpf.Controls.Base.Interfaces;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using Core.Organization.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for UpDownBattleRatingGroupControl.xaml. </summary>
    public partial class UpDownBattleRatingGroupControl : LocalizedUserControl, IControlWithSubcontrols<ENation>
    {
        #region Properties

        /// <summary> Controls for battle rating values. </summary>
        public IDictionary<ENation, UpDownBattleRatingPairControl> BattleRatingControls { get; }

        #endregion Properties
        #region Events

        /// <summary> A routed event for <see cref="ValueChanged"/>. </summary>
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UpDownBattleRatingGroupControl));

        /// <summary> Occurs when <see cref="Value"/> changes. </summary>
        public event RoutedEventHandler ValueChanged;

        #endregion Events
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public UpDownBattleRatingGroupControl()
        {
            InitializeComponent();

            BattleRatingControls = new Dictionary<ENation, UpDownBattleRatingPairControl>();

            CreateBattleRatingPairControls();
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Raises the <see cref="ValueChangedEvent"/>. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnValueChanged(object sender, RoutedEventArgs eventArguments) =>
            RaiseValueChanged();

        #endregion Methods: Event Handlers

        /// <summary> Raises the <see cref="ValueChangedEvent"/>. </summary>
        public void RaiseValueChanged() =>
            RaiseEvent(new RoutedEventArgs(ValueChangedEvent, this));

        /// <summary> Applies localization to visible text on the control. </summary>
        public override void Localize()
        {
            base.Localize();

            _header.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.BattleRatings);
        }

        private void CreateBattleRatingPairControls()
        {
            foreach (var nation in typeof(ENation).GetEnumValues().OfType<ENation>().Except(ENation.None))
            {
                var battleRatingControl = new UpDownBattleRatingPairControl()
                {
                    Tag = nation,
                };

                // For whatever reason assigning OnValueChanged() directly to battleRatingControl.ValueChanged doesn't make it detect when UpDownBattleRatingPairControl.ValueChangedEvent is raised.
                // Assigning the handler through XAML works fine, so does direct assigning on other controls.
                battleRatingControl.AddHandler(UpDownBattleRatingPairControl.ValueChangedEvent, new RoutedEventHandler(OnValueChanged));
                battleRatingControl.AddToGrid(_grid, true);

                BattleRatingControls.Add(nation, battleRatingControl);
            }
        }

        /// <summary> Initializes <see cref="BattleRatingControls"/>. </summary>
        public void InitializeControls()
        {
            foreach (var control in BattleRatingControls.Values)
                control.Initialize(WpfSettings.EnabledEconomicRankIntervals[control.Tag.CastTo<ENation>()]);
        }

        /// <summary> Changes the <see cref="UIElement.IsEnabled"/> status of the of the up-down control pair corresponding to the specified nation. </summary>
        /// <param name="nation"> The nation whose control pair's state to change. </param>
        /// <param name="enable"> Whether to enable or disable the nation's control control pair. </param>
        public void Enable(ENation nation, bool enable)
        {
            if (BattleRatingControls.TryGetValue(nation, out var toggleButton) && toggleButton.IsEnabled != enable)
                toggleButton.IsEnabled = enable;
        }
    }
}