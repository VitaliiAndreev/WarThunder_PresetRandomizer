using Client.Wpf.Controls.Base;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for CountryToggleControl.xaml. </summary>
    public partial class CountryToggleControl : ColumnToggleGroupControl<ENation, CountryColumnToggleControl, NationCountryPair>
    {
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public CountryToggleControl()
        {
            var nations = typeof(ENation).GetEnumValues().Cast<ENation>().Except(ENation.None);

            InitializeComponent();
            CreateToggleColumns(nations);
        }

        #endregion Constructors

        /// <summary> Creates columns of toggle buttons for given <paramref name="nations"/>, with character icons. </summary>
        /// <param name="nations"> Nations to create columns of toggle buttons for. </param>
        public override void CreateToggleColumns(IEnumerable<ENation> nations)
        {
            foreach (var nation in nations)
            {
                var columnControl = new CountryColumnToggleControl(nation)
                {
                    Tag = nation,
                };

                columnControl.Click += OnClick;
                columnControl.AddToPanel(_grid, true);

                ToggleClassColumns.Add(nation, columnControl);
            }
        }

        /// <summary> Toggles a button corresponding to the specified <paramref name="nationCountryPair"/>. </summary>
        /// <param name="nationCountryPair"> The country whose button to toggle. </param>
        /// <param name="newState"> Whether to toggle the button on or off. </param>
        public override void Toggle(NationCountryPair nationCountryPair, bool newState)
        {
            if (ToggleClassColumns.TryGetValue(nationCountryPair.Nation, out var column))
                column.Toggle(nationCountryPair, newState);
        }
    }
}