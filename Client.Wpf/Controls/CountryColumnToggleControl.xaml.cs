using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Connectors;
using Core.Extensions;
using System.Linq;
using System.Windows.Controls;

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

                CreateToggleButtonsWithImages
                (
                    _panel,
                    _groupedItems[value],
                    _groupedItems
                        .Values
                        .SelectMany(nationCountryPairs => nationCountryPairs)
                        .Where(nationCountryPair => nationCountryPair.Country.IsKeyIn(EReference.CountryIconKeys))
                        .ToDictionary(nationCountryPair => nationCountryPair, nationCountryPair => EReference.CountryIconKeys[nationCountryPair.Country]),
                    EStyleKey.ToggleButton.CountryToggle,
                    false,
                    true
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
                .Where(nation => nation.IsValid())
                .ToDictionary(nation => nation, nation => nation.GetCountries().Select(country => new NationCountryPair(nation, country)))
            ;

            _groupedItems.AddRange(countriesByNations);
            
            InitializeComponent();
        }

        #endregion Constructors
        #region Methods: Overrides

        /// <summary> Creates a toggle-all button. </summary>
        /// <param name="panel"> The panel to add the button onto. </param>
        /// <param name="enumerationItem"> The enumeration item to create the toggle button for. </param>
        /// <param name="styleKey"> The key of the style (defined in <see cref="WpfClient"/> and referenced by <see cref="EStyleKey"/>) to apply. </param>
        /// <param name="horizontal"> Whether other buttons are arranged in a row or in a column. </param>
        protected override void CreateToggleAllButton(Panel panel, NationCountryPair enumerationItem, string styleKey, bool horizontal)
        {
            static NationCountryPair getAllCountriesTag(NationCountryPair nationCountryPair) => new NationCountryPair(nationCountryPair.Nation, nationCountryPair.Nation.GetAllCountriesItem());

            base.CreateToggleAllButton(panel, getAllCountriesTag(enumerationItem), styleKey, horizontal);
        }

        /// <summary> Applies localisation to visible text on the control. </summary>
        public override void Localise()
        {
            base.Localise();

            static string getLocalizedString(string localizationKey) => ApplicationHelpers.LocalisationManager.GetLocalisedString(localizationKey);

            _toggleAllButton.ToolTip = getLocalizedString(ELocalisationKey.SelectAllCountries).FormatFluently(getLocalizedString(Owner.ToString()));
        }

        #endregion Methods: Overrides
    }
}