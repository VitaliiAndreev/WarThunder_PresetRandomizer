using Client.Shared.Wpf.Extensions;
using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Connectors;
using Core.Enumerations;
using System.Windows;
using System.Windows.Input;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleCounterWithFlag.xaml. </summary>
    public partial class TextLabelWithFlag : TextLabel
    {
        #region Fields

        private readonly Style _flagStyle;

        #endregion Fields
        #region Constructors

        public TextLabelWithFlag()
        {
            InitializeComponent();
        }

        public TextLabelWithFlag
        (
            NationCountryPair nationCountryPair,
            string text,
            Thickness margin,
            MouseButtonEventHandler mouseDownHandler,
            HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left,
            bool isBold = false,
            bool useNationFlags = false,
            bool createTooltip = true
        ) : base(text, margin, textHorizontalAlignment, isBold)
        {
            InitializeComponent();

            _flagStyle = this.GetStyle(EStyleKey.Image.FlagIcon16px);

            Tag = nationCountryPair;

            _panel.Children.Add(nationCountryPair.CreateFlag(_flagStyle, new Thickness(Integer.Number.Zero, Integer.Number.Zero, Integer.Number.Five, Integer.Number.Zero), useNationFlags, createTooltip));
            _panel.Children.Add(_label);

            MouseDown += mouseDownHandler;
        }

        public TextLabelWithFlag
        (
            ECountry country,
            string text,
            Thickness margin,
            MouseButtonEventHandler mouseDownHandler,
            HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left,
            bool isBold = false,
            bool useNationFlags = false,
            bool createTooltip = true
        )
            : this(new NationCountryPair(ENation.None, country), text, margin, mouseDownHandler, textHorizontalAlignment, isBold, useNationFlags, createTooltip)
        {
            Tag = country;
        }

        #endregion Constructors
    }
}