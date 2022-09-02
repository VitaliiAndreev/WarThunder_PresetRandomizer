using Client.Shared.Wpf.Extensions;
using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Connectors;
using System.Windows;
using System.Windows.Input;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleCounterWithFlag.xaml. </summary>
    public partial class VehicleCountWithFlag : VehicleCount
    {
        #region Fields

        private readonly Style _flagStyle;

        #endregion Fields
        #region Constructors

        public VehicleCountWithFlag()
        {
            InitializeComponent();
        }

        public VehicleCountWithFlag
        (
            NationCountryPair nationCountryPair,
            int count,
            Thickness margin,
            MouseButtonEventHandler mouseDownHandler,
            HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left,
            bool useNationFlags = false
        ) : base(count, margin, textHorizontalAlignment)
        {
            InitializeComponent();

            _flagStyle = this.GetStyle(EStyleKey.Image.FlagIcon16px);

            Tag = nationCountryPair;

            _grid.Children.Add(nationCountryPair.CreateFlag(_flagStyle, new Thickness(Integer.Number.Zero, Integer.Number.Zero, Integer.Number.Five, Integer.Number.Zero), useNationFlags));
            _grid.Children.Add(_label);

            MouseDown += mouseDownHandler;
        }

        public VehicleCountWithFlag
        (
            ECountry country,
            int count,
            Thickness margin,
            MouseButtonEventHandler mouseDownHandler,
            HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left,
            bool useNationFlags = false
        ) : this(new NationCountryPair(ENation.None, country), count, margin, mouseDownHandler, textHorizontalAlignment, useNationFlags)
        {
            Tag = country;
        }

        #endregion Constructors
    }
}