using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Extensions;
using Core.Organization.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeNationControl.xaml. </summary>
    public partial class ResearchTreeNationControl : LocalizedUserControl
    {
        #region Fields

        /// <summary> The map of the branch enumeration onto corresponding controls. </summary>
        private readonly IDictionary<EBranch, ResearchTreeBranchControl> _branchControls;

        #endregion Fields
        #region Properties

        /// <summary> The embedded tab control. </summary>
        internal TabControl TabControl => _tabControl;

        /// <summary> The map of the branch enumeration onto corresponding tabs. </summary>
        internal IDictionary<EBranch, TabItem> BranchTabs { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public ResearchTreeNationControl()
        {
            InitializeComponent();

            _armyTab.Tag = EBranch.Army;
            _helicoptersTab.Tag = EBranch.Helicopters;
            _aviationTab.Tag = EBranch.Aviation;
            _fleetTab.Tag = EBranch.Fleet;

            BranchTabs = new Dictionary<EBranch, TabItem>
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

        /// <summary> Returns the string representation of the object. </summary>
        /// <returns></returns>
        public override string ToString() => $"[{base.ToString()}] {(Parent as FrameworkElement)?.Tag}";

        /// <summary> Applies localization to visible text on the control. </summary>
        public override void Localize()
        {
            base.Localize();

            _armyTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Army);
            _helicoptersTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Helicopters);
            _aviationTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Planes);
            _fleetTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Fleet);
        }

        /// <summary> Populates tabs with appropriate research trees. </summary>
        internal void Populate(ResearchTree researchTree)
        {
            foreach (var branchTabKeyValuePair in BranchTabs)
            {
                if (researchTree.TryGetValue(branchTabKeyValuePair.Key, out var branch))
                {
                    _branchControls[branchTabKeyValuePair.Key].Populate(branch);
                    continue;
                }
                branchTabKeyValuePair.Value.IsEnabled = false;
            }
        }

        /// <summary> Gets all empty branches (their tabs should be disabled). </summary>
        /// <returns></returns>
        internal IEnumerable<EBranch> GetEmptyBranches() => _branchControls
            .ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => !keyValuePair.Value.IsPopulated)
            .Where(keyValuePair => keyValuePair.Value)
            .Select(keyValuePair => keyValuePair.Key)
        ;

        /// <summary> Displays <see cref="IVehicle.BattleRating"/> values for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode for which to display the battle rating. </param>
        internal void DisplayBattleRatingFor(EGameMode gameMode)
        {
            foreach (var vehicleCell in _branchControls.Values)
                vehicleCell.DisplayBattleRatingFor(gameMode);
        }

        /// <summary> Resets <see cref="UIElement.IsEnabled"/> statuses of branch tabs. </summary>
        internal void ResetTabRestrictions()
        {
            foreach (var branchTab in BranchTabs.Values)
            {
                if (branchTab.Tag is EBranch branch && _branchControls.TryGetValue(branch, out var branchControl))
                    branchTab.IsEnabled = branchControl.IsPopulated;
            }
        }

        /// <summary> Disables all branch tabs not specified in the parameters. </summary>
        /// <param name="branches"> Branch tabs to keep enabled. </param>
        public void EnableOnly(IEnumerable<EBranch> branches)
        {
            foreach (var branchTab in BranchTabs.Values)
            {
                if (branchTab.Tag is EBranch tabBranch)
                    branchTab.IsEnabled = tabBranch.IsIn(branches);
            }
        }

        /// <summary> Focuses on a research tree by given parameters. </summary>
        /// <param name="branch"> The branch to put into focus. </param>
        public void FocusResearchTree(EBranch branch)
        {
            if (BranchTabs.TryGetValue(branch, out var branchTab))
            {
                if (!branchTab.IsEnabled)
                {
                    _tabControl.SelectedItem = BranchTabs.Values.First(tab => tab.IsEnabled);
                    return;
                }

                _tabControl.SelectedItem = branchTab;
            }
        }

        /// <summary> Get the research tree branch control appropriate to the given vehicle. </summary>
        /// <param name="vehicle"> The vehicle whose research tree branch control to look for. </param>
        /// <returns></returns>
        private ResearchTreeBranchControl GetBranchControl(IVehicle vehicle) =>
            _branchControls.TryGetValue(vehicle.Branch.AsEnumerationItem, out var branchControl) ? branchControl : null;

        /// <summary> Scrolls the research tree to bring the specified vehicle into view. </summary>
        /// <param name="vehicle"> The vehicle to bring into view. </param>
        internal void BringIntoView(IVehicle vehicle) => GetBranchControl(vehicle)?.BringIntoView(vehicle);

        /// <summary> Highlights the specified vehicle in the reseatch tree. </summary>
        /// <param name="vehicle"> The vehicle to highlight. </param>
        internal void Highlight(IVehicle vehicle) => GetBranchControl(vehicle)?.Highlight(vehicle);

        /// <summary> Removes the highlight from the specified vehicle in the reseatch tree. </summary>
        /// <param name="vehicle"> The vehicle to remove highlight from. </param>
        internal void RemoveHighlight(IVehicle vehicle) => GetBranchControl(vehicle)?.RemoveHighlight(vehicle);
    }
}