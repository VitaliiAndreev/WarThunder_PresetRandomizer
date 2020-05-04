using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.Enumerations;
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
            MouseButtonEventHandler mouseDownHandler,
            double iconColumnWidth,
            double countColumnWidth,
            double iconFontSize,
            HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left
        ) : base(count, margin, textHorizontalAlignment)
        {
            InitializeComponent();

            _iconStyle = this.GetStyle(EStyleKey.TextBlock.TextBlockWithSkyQuakeUncondensed);
            _iconColumnDefinition.Width = new GridLength(iconColumnWidth, GridUnitType.Pixel);
            _countColumnDefinition.Width = new GridLength(countColumnWidth, GridUnitType.Pixel);

            var iconControl = new TextBlock
            {
                Style = _iconStyle,
                Margin = new Thickness(EInteger.Number.Zero, -EInteger.Number.Ten, EInteger.Number.Three, -EInteger.Number.Ten),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                FontSize = iconFontSize,
                Text = icon.ToString(),
                ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(iconTag.ToString()),
            };

            _grid.Add(iconControl, EInteger.Number.Zero, EInteger.Number.Zero);
            _grid.Add(_label, EInteger.Number.One, EInteger.Number.Zero);

            if (!(mouseDownHandler is null))
                MouseDown += mouseDownHandler;
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