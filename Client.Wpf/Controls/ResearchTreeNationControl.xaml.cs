using Client.Wpf.Controls.Base;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using Core.Organization.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeNationControl.xaml. </summary>
    public partial class ResearchTreeNationControl : LocalisedUserControl
    {
        #region Fields

        /// <summary> The map of the branch enumeration onto corresponding controls. </summary>
        internal readonly IDictionary<EBranch, ResearchTreeBranchControl> _branchControls;

        private bool _initialised;

        private IMainWindowPresenter _presenter;

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
        #region Methods: Overrides

        /// <summary> Returns the string representation of the object. </summary>
        /// <returns></returns>
        public override string ToString() => $"[{base.ToString()}] {(Parent as FrameworkElement)?.Tag}";

        /// <summary> Applies localisation to visible text on the control. </summary>
        public override void Localise()
        {
            base.Localise();

            static void localiseTabHeader(TabItem tab)
            {
                if (tab.Header is WrapPanel panel && panel.Children.OfType<TextBlock>().LastOrDefault() is TextBlock textBlock)
                    textBlock.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(tab.Tag.ToString());
            }

            localiseTabHeader(_armyTab);
            localiseTabHeader(_helicoptersTab);
            localiseTabHeader(_aviationTab);
            localiseTabHeader(_fleetTab);

            foreach (var control in _branchControls.Values)
                control.Localise();
        }

        #endregion Methods: Overrides
        #region Methods: Initialisation

        public void Initialise(IMainWindowPresenter presenter)
        {
            if (!_initialised && presenter is IMainWindowPresenter)
            {
                _presenter = presenter;

                foreach (var control in _branchControls.Values)
                    control.Initialise(_presenter);

                _initialised = true;
            }
        }

        /// <summary> Populates tabs with appropriate research trees. </summary>
        /// <param name="researchTree"> The research tree to create cells with. </param>
        /// <param name="enabledVehicleGaijinIds"> Gaijin IDs of vehicles enabled by dafault. </param>
        /// <param name="loadingTracker"> An instance of a presenter to communicate with the GUI loading window. </param>
        internal void Populate(ResearchTree researchTree, IEnumerable<string> enabledVehicleGaijinIds, IGuiLoadingWindowPresenter loadingTracker)
        {
            loadingTracker.BranchesPopulated = EInteger.Number.Zero;
            loadingTracker.BranchesToPopulate = BranchTabs.Count;

            foreach (var branchTabKeyValuePair in BranchTabs)
            {
                var branch = branchTabKeyValuePair.Key;
                var tab = branchTabKeyValuePair.Value;

                loadingTracker.CurrentlyPopulatedBranch = ApplicationHelpers.LocalisationManager.GetLocalisedString(branch.ToString());

                if (researchTree.TryGetValue(branch, out var researchTreeBranch))
                {
                    _branchControls[branch].Populate(researchTreeBranch, enabledVehicleGaijinIds, loadingTracker);
                }
                else
                {
                    tab.IsEnabled = false;
                }
                loadingTracker.CurrentlyPopulatedBranch = string.Empty;
                loadingTracker.BranchesPopulated++;
            }
        }

        #endregion Methods: Initialisation
        #region Methods: Checks

        /// <summary> Checks whether all vehicles in the nation are toggled on. </summary>
        /// <returns></returns>
        internal bool AllVehiclesAreToggled(bool onlyNonResearchable = false) =>
            _branchControls.Values.All(control => control.AllVehiclesAreToggled(onlyNonResearchable));

        #endregion Methods: Checks

        /// <summary> Gets all empty branches (their tabs should be disabled). </summary>
        /// <returns></returns>
        internal IEnumerable<EBranch> GetEmptyBranches() => _branchControls
            .ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => !keyValuePair.Value.IsPopulated)
            .Where(keyValuePair => keyValuePair.Value)
            .Select(keyValuePair => keyValuePair.Key)
        ;

        /// <summary> Displays vehicle information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode for which to display the battle rating. </param>
        internal void DisplayVehicleInformation(EGameMode gameMode)
        {
            foreach (var vehicleCell in _branchControls.Values)
                vehicleCell.DisplayVehicleInformation(gameMode);
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

        internal IEnumerable<ResearchTreeCellVehicleControl> GetVehicleControls() => _branchControls.Values.SelectMany(control => control.GetVehicleControls());
    }
}