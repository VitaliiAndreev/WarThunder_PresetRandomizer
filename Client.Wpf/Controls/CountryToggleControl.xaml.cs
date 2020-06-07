using Client.Shared.Wpf.Extensions;
using Client.Wpf.Controls.Base;
using Client.Wpf.Controls.Base.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Connectors;
using Core.Organization.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for CountryToggleControl.xaml. </summary>
    public partial class CountryToggleControl : ColumnToggleGroupControl<ENation, NationCountryPair>, IControlWithToggleColumns<ENation, NationCountryPair>
    {
        #region Properties

        /// <summary> The main panel of the control. </summary>
        internal override Panel MainPanel => _grid;

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public CountryToggleControl()
        {
            AttachKeyConverter(nationCountryPair => nationCountryPair.Nation);

            var nations = typeof(ENation).GetEnumValues().Cast<ENation>().Where(nation => nation.IsValid());

            InitializeComponent();
            CreateToggleColumns(nations);
        }

        #endregion Constructors

        /// <summary> Creates columns of toggle buttons for given <paramref name="nations"/>, with character icons. </summary>
        /// <param name="nations"> Nations to create columns of toggle buttons for. </param>
        public void CreateToggleColumns(IEnumerable<ENation> nations) =>
            CreateToggleColumnsBase(typeof(CountryColumnToggleControl), nations);

        /// <summary> Removes countries of nations that have no vehicles. </summary>
        public void RemoveCountriesForUnavailableNations()
        {
            foreach (var columnKeyValuePair in ToggleColumns)
            {
                var nation = columnKeyValuePair.Key;
                var column = columnKeyValuePair.Value;

                if (!ApplicationHelpers.Manager.ResearchTrees.Has(nation))
                {
                    if (column.Parent is Grid grid)
                        grid.Remove(column);
                }
            }
        }
    }
}