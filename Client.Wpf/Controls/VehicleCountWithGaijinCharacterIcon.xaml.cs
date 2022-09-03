using Client.Shared.Wpf.Extensions;
using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleCountWithGaijinCharacterIcon.xaml. </summary>
    public partial class VehicleCountWithGaijinCharacterIcon : VehicleCount
    {
        #region Fields

        private readonly Style _iconStyle;

        #endregion Fields
        #region Constructors

        public VehicleCountWithGaijinCharacterIcon()
        {
            InitializeComponent();
        }

        public VehicleCountWithGaijinCharacterIcon
        (
            char icon,
            object iconTag,
            int count,
            Thickness margin,
            MouseButtonEventHandler mouseButtonHandler,
            double iconColumnWidth,
            double countColumnWidth,
            double iconFontSize,
            HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left,
            bool iconIsBold = false
        ) : this(icon.ToString(), iconTag, count, margin, mouseButtonHandler, iconColumnWidth, countColumnWidth, iconFontSize, textHorizontalAlignment, iconIsBold)
        {
        }

        public VehicleCountWithGaijinCharacterIcon
        (
            string icon,
            object iconTag,
            int count,
            Thickness margin,
            MouseButtonEventHandler mouseButtonHandler,
            double iconColumnWidth,
            double? countColumnWidth,
            double iconFontSize,
            HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left,
            bool iconIsBold = false
        ) : base(count, margin, textHorizontalAlignment)
        {
            InitializeComponent();

            _iconStyle = this.GetStyle(EStyleKey.TextBlock.TextBlockWithSkyQuakeUncondensed);
            _iconColumnDefinition.Width = new GridLength(iconColumnWidth, GridUnitType.Pixel);

            if (countColumnWidth.HasValue)
                _countColumnDefinition.Width = new GridLength(countColumnWidth.Value, GridUnitType.Pixel);

            var iconControl = new TextBlock
            {
                Style = _iconStyle,
                Margin = Wpf.Margin.GaijinIconWithLabelMargin,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                FontWeight = iconIsBold ? FontWeights.Bold : FontWeights.Normal,
                FontSize = iconFontSize,
                Text = icon,
                ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(iconTag.ToString()),
            };

            _grid.Add(iconControl, columnIndex: 0, rowIndex: 0);
            _grid.Add(_label, columnIndex: 1, rowIndex: 0);

            if (!(mouseButtonHandler is null))
                MouseDown += mouseButtonHandler;
        }

        #endregion Constructors
        #region Methods: Initialisation

        public VehicleCountWithGaijinCharacterIcon WithTag<T>(T controlTag)
        {
            Tag = controlTag;

            return this;
        }

        #endregion Methods: Initialisation
    }
}