using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Windows;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for UpDownBattleRatingGroupControl.xaml. </summary>
    public partial class UpDownBattleRatingGroupControl : LocalizedUserControl
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

            _usaBattleRatingControl.Tag = ENation.Usa;
            _germanyBattleRatingControl.Tag = ENation.Germany;
            _ussrBattleRatingControl.Tag = ENation.Ussr;
            _commonwealthBattleRatingControl.Tag = ENation.Britain;
            _japanBattleRatingControl.Tag = ENation.Japan;
            _chinaBattleRatingControl.Tag = ENation.China;
            _italyBattleRatingControl.Tag = ENation.Italy;
            _franceBattleRatingControl.Tag = ENation.France;

            BattleRatingControls = new Dictionary<ENation, UpDownBattleRatingPairControl>
            {
                { ENation.Usa, _usaBattleRatingControl},
                { ENation.Germany, _germanyBattleRatingControl},
                { ENation.Ussr, _ussrBattleRatingControl},
                { ENation.Britain, _commonwealthBattleRatingControl},
                { ENation.Japan, _japanBattleRatingControl},
                { ENation.China, _chinaBattleRatingControl},
                { ENation.Italy, _italyBattleRatingControl},
                { ENation.France, _franceBattleRatingControl},
            };
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

        /// <summary> Initializes <see cref="BattleRatingControls"/>. </summary>
        public void InitializeControls()
        {
            foreach (var control in BattleRatingControls.Values)
                control.Initialize(WpfSettings.EnabledEconomicRankIntervals[control.Tag.CastTo<ENation>()]);
        }
    }
}