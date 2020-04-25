using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Objects;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleCounterWithFlag.xaml. </summary>
    public partial class VehicleCountWithFlag : VehicleCount
    {
        #region Constructors

        public VehicleCountWithFlag()
        {
            InitializeComponent();
        }

        public VehicleCountWithFlag(NationCountryPair nationCountryPair, int count, Thickness margin)
            : base(count, margin)
        {
            InitializeComponent();

            Tag = nationCountryPair;

            var image = new Image
            {
                Style = this.GetStyle(EStyleKey.Image.FlagIcon16px),
                Source = ApplicationHelpers.Manager.GetFlagImageSource(nationCountryPair.Country),
                Margin = new Thickness(0, 0, 5, 0),
                ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(nationCountryPair.Country.ToString()),
            };

            _panel.Children.Add(image);
            _panel.Children.Add(_count);
        }

        #endregion Constructors
    }
}