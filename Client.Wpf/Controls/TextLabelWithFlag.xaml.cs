using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects;
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

        public TextLabelWithFlag(NationCountryPair nationCountryPair, string text, Thickness margin, MouseButtonEventHandler mouseDownHandler, bool isBold = false, bool useNationFlags = false)
            : base(text, margin, isBold)
        {
            InitializeComponent();

            _flagStyle = this.GetStyle(EStyleKey.Image.FlagIcon16px);

            Tag = nationCountryPair;

            _panel.Children.Add(nationCountryPair.CreateFlag(_flagStyle, new Thickness(EInteger.Number.Zero, EInteger.Number.Zero, EInteger.Number.Five, EInteger.Number.Zero), useNationFlags));
            _panel.Children.Add(_label);

            MouseDown += mouseDownHandler;
        }

        public TextLabelWithFlag(ECountry country, string text, Thickness margin, MouseButtonEventHandler mouseDownHandler, bool isBold = false, bool useNationFlags = false)
            : this(new NationCountryPair(ENation.None, country), text, margin, mouseDownHandler, isBold, useNationFlags)
        {
            Tag = country;
        }

        #endregion Constructors
    }
}