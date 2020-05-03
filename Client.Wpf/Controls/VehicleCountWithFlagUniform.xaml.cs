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
    /// <summary> Interaction logic for VehicleCountWithFlagUniform.xaml. </summary>
    public partial class VehicleCountWithFlagUniform : VehicleCount
    {
        #region Fields

        private readonly Style _flagStyle;

        #endregion Fields
        #region Constructors

        public VehicleCountWithFlagUniform()
        {
            InitializeComponent();
        }

        public VehicleCountWithFlagUniform
        (
            NationCountryPair nationCountryPair,
            int count,
            Thickness margin,
            MouseButtonEventHandler mouseDownHandler,
            double flagColumnWidth,
            double countColumnWidth,
            HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left,
            bool useNationFlags = false
        ) : base(count, margin, textHorizontalAlignment)
        {
            InitializeComponent();

            _flagStyle = this.GetStyle(EStyleKey.Image.FlagIcon16px);
            _flagColumnDefinition.Width = new GridLength(flagColumnWidth, GridUnitType.Pixel);
            _countColumnDefinition.Width = new GridLength(countColumnWidth, GridUnitType.Pixel);

            Tag = nationCountryPair;

            _grid.Add(nationCountryPair.CreateFlag(_flagStyle, new Thickness(EInteger.Number.Zero, EInteger.Number.Zero, EInteger.Number.Five, EInteger.Number.Zero), useNationFlags), EInteger.Number.Zero, EInteger.Number.Zero);
            _grid.Add(_label, EInteger.Number.One, EInteger.Number.Zero);

            MouseDown += mouseDownHandler;
        }

        public VehicleCountWithFlagUniform
        (
            ECountry country,
            int count,
            Thickness margin,
            MouseButtonEventHandler mouseDownHandler,
            double flagColumnWidth,
            double countColumnWidth,
            HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left,
            bool useNationFlags = false
        ) : this(new NationCountryPair(ENation.None, country), count, margin, mouseDownHandler, flagColumnWidth, countColumnWidth, textHorizontalAlignment, useNationFlags)
        {
            Tag = country;
        }

        #endregion Constructors
    }
}