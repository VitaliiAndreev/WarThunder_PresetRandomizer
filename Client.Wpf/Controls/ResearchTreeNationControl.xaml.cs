using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Organization.Objects;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeNationControl.xaml. </summary>
    public partial class ResearchTreeNationControl : UserControl
    {
        #region Fields

        /// <summary> The map of the branch enumeration onto corresponding tabs. </summary>
        private readonly IDictionary<EBranch, TabItem> _branchTabs;

        /// <summary> The map of the branch enumeration onto corresponding controls. </summary>
        private readonly IDictionary<EBranch, ResearchTreeBranchControl> _branchControls;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public ResearchTreeNationControl()
        {
            InitializeComponent();

            _branchTabs = new Dictionary<EBranch, TabItem>
            {
                { EBranch.Army, _armyTab },
                { EBranch.Helicopters, _helicoptersTab },
                { EBranch.Aviation, _aviationTab },
                { EBranch.Fleet, _fleetTab },
            };
            _branchControls = new Dictionary<EBranch, ResearchTreeBranchControl>
            {
                { EBranch.Army, _armyBranch },
                { EBranch.Helicopters, _helicoptersBranch },
                { EBranch.Aviation, _aviationBranch },
                { EBranch.Fleet, _fleetBranch },
            };
        }

        #endregion Constructors

        /// <summary> Applies localization to visible text on the control. </summary>
        public void Localize()
        {
            _armyTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Army);
            _helicoptersTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Helicopters);
            _aviationTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Planes);
            _fleetTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Fleet);
        }

        /// <summary> Populates tabs with appropriate research trees. </summary>
        public void Populate(ResearchTree researchTree)
        {
            foreach (var branchTabKeyValuePair in _branchTabs)
            {
                if (researchTree.TryGetValue(branchTabKeyValuePair.Key, out var branch))
                {
                    _branchControls[branchTabKeyValuePair.Key].Populate(branch);
                    continue;
                }
                branchTabKeyValuePair.Value.IsEnabled = false;
            }
        }

        /// <summary> Displays <see cref="IVehicle.BattleRating"/> values for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode for which to display the battle rating. </param>
        public void DisplayBattleRatingFor(EGameMode gameMode)
        {
            foreach (var vehicleCell in _branchControls.Values)
                vehicleCell.DisplayBattleRatingFor(gameMode);
        }
    }
}