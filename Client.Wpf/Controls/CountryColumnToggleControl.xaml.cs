using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects;
using Core.Extensions;
using System.Linq;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for CountryColumnToggleControl.xaml. </summary>
    public partial class CountryColumnToggleControl : ColumnToggleControlWithResources<ENation, NationCountryPair>
    {
        #region Fields

        /// <summary> The nation to which countries belong to. </summary>
        private ENation _nation;

        #endregion Fields
        #region Properties

        /// <summary> The nation to which countries belong to. </summary>
        public override ENation Owner
        {
            get => _nation;
            set
            {
                _nation = value;
                _panel.Children.Clear();

                CreateToggleButtons
                (
                    _panel,
                    _groupedItems[value],
                    _groupedItems
                        .Values
                        .SelectMany(nationCountryPairs => nationCountryPairs)
                        .Where(nationCountryPair => nationCountryPair.Country.IsKeyIn(EReference.CountryIconsKeys))
                        .ToDictionary(nationCountryPair => nationCountryPair, nationCountryPair => EReference.CountryIconsKeys[nationCountryPair.Country]),
                    EStyleKey.ToggleButton.CountryToggle, false
                );
            }
        }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        /// <param name="branch"> The nation to which countries belong to. </param>
        public CountryColumnToggleControl(ENation nation)
            : this()
        {
            Owner = nation;
        }

        /// <summary> Creates a new control. </summary>
        public CountryColumnToggleControl()
        {
            var countriesByNations = typeof(ENation)
                .GetEnumValues()
                .Cast<ENation>()
                .Except(ENation.None)
                .ToDictionary(nation => nation, nation => nation.GetCountries().Select(country => new NationCountryPair(nation, country)))
            ;

            _groupedItems.AddRange(countriesByNations);
            
            InitializeComponent();
        }

        #endregion Constructors
    }
}