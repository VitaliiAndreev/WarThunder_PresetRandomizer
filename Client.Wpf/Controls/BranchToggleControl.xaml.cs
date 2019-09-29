using Client.Wpf.Controls.Base;
using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for BranchToggleControl.xaml. </summary>
    public partial class BranchToggleControl : ToggleButtonGroupControl<EBranch>
    {
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public BranchToggleControl()
        {
            InitializeComponent();

            _armyButton.Tag = EBranch.Army;
            _aviationButton.Tag = EBranch.Aviation;
            _helicopterButton.Tag = EBranch.Helicopters;
            _fleetButton.Tag = EBranch.Fleet;

            _buttons.AddRange(new List<ToggleButton> { _armyButton, _aviationButton, _helicopterButton, _fleetButton }.ToDictionary(button => button.Tag.CastTo<EBranch>()));

            _armyButton.Click += OnClick;
            _aviationButton.Click += OnClick;
            _helicopterButton.Click += OnClick;
            _fleetButton.Click += OnClick;
        }

        #endregion Constructors
    }
}