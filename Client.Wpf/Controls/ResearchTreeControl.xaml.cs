using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Extensions;
using NHibernate.Mapping;
using NHibernate.Util;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeControl.xaml. </summary>
    public partial class ResearchTreeControl : LocalizedUserControl
    {
        #region Fields

        /// <summary> The map of the nation enumeration onto corresponding tabs. </summary>
        private readonly IDictionary<ENation, TabItem> _nationTabs;
        /// <summary> The map of the nation enumeration onto corresponding controls. </summary>
        private readonly IDictionary<ENation, ResearchTreeNationControl> _nationControls;

        /// <summary> The currently selected nation. </summary>
        private ENation _currentNation;
        /// <summary> The currently selected branch. </summary>
        private EBranch _currentBranch;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public ResearchTreeControl()
        {
            InitializeComponent();

            _usaTab.Tag = ENation.Usa;
            _germanyTab.Tag = ENation.Germany;
            _ussrTab.Tag = ENation.Ussr;
            _britainTab.Tag = ENation.Commonwealth;
            _japanTab.Tag = ENation.Japan;
            _chinaTab.Tag = ENation.China;
            _italyTab.Tag = ENation.Italy;
            _franceTab.Tag = ENation.France;

            _tabControl.SelectionChanged += OnTabChange;

            _nationTabs = new Dictionary<ENation, TabItem>
            {
                { ENation.Usa, _usaTab },
                { ENation.Germany, _germanyTab },
                { ENation.Ussr, _ussrTab },
                { ENation.Commonwealth, _britainTab },
                { ENation.Japan, _japanTab },
                { ENation.China, _chinaTab },
                { ENation.Italy, _italyTab },
                { ENation.France, _franceTab },
            };
            _nationControls = new Dictionary<ENation, ResearchTreeNationControl>
            {
                { ENation.Usa, _usaTree },
                { ENation.Germany, _germanyTree },
                { ENation.Ussr, _ussrTree },
                { ENation.Commonwealth, _britainTree },
                { ENation.Japan, _japanTree },
                { ENation.China, _chinaTree },
                { ENation.Italy, _italyTree },
                { ENation.France, _franceTree },
            };
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Maintains branch selection when switching between nations, unless the branch is not implemented in which case selection is reset to the first available branch. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="routedEventArguments"> Event arguments. <see cref="SelectionChangedEventArgs"/> are expected. </param>
        private void OnTabChange(object sender, RoutedEventArgs routedEventArguments)
        {
            if (routedEventArguments.OriginalSource is TabControl && routedEventArguments is SelectionChangedEventArgs selectionChangedEventArguments && selectionChangedEventArguments.AddedItems.OfType<TabItem>().First() is TabItem newTabItem)
            {
                if (newTabItem.Tag is ENation selectedNation)
                {
                    _currentNation = selectedNation;

                    if (_currentBranch != EBranch.None)
                    {
                        var nationControl = _nationControls[_currentNation];
                        var branchTab = nationControl.BranchTabs[_currentBranch];

                        if (branchTab.IsEnabled)
                            nationControl.TabControl.SelectedItem = branchTab;
                        else
                            nationControl.TabControl.SelectedItem = nationControl.BranchTabs.Values.First(branch => branch.IsEnabled);
                    }
                }
                if (newTabItem.Tag is EBranch selectedBranch)
                {
                    _currentBranch = selectedBranch;
                }
            }
        }

        #endregion Methods: Event Handlers

        /// <summary> Applies localization to visible text on the control. </summary>
        public override void Localize()
        {
            base.Localize();

            _usaTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Usa);
            _germanyTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Germany);
            _ussrTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Ussr);
            _britainTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Britain);
            _japanTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Japan);
            _chinaTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.China);
            _italyTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Italy);
            _franceTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.France);
        }

        /// <summary> Populates tabs with appropriate research trees. </summary>
        public void Populate()
        {
            foreach (var nationTabKeyValuePair in _nationTabs)
            {
                if (ApplicationHelpers.Manager.ResearchTrees.TryGetValue(nationTabKeyValuePair.Key, out var researchTree))
                {
                    _nationControls[nationTabKeyValuePair.Key].Populate(researchTree);
                    continue;
                }
                nationTabKeyValuePair.Value.IsEnabled = false;
            }
        }

        /// <summary> Gets all empty branches (their tabs should be disabled). </summary>
        /// <returns></returns>
        public IDictionary<ENation, IEnumerable<EBranch>> GetEmptyBranches() => _nationControls.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value.GetEmptyBranches());

        /// <summary> Displays <see cref="IVehicle.BattleRating"/> values for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode for which to display the battle rating. </param>
        public void DisplayBattleRatingFor(EGameMode gameMode)
        {
            foreach (var vehicleCell in _nationControls.Values)
                vehicleCell.DisplayBattleRatingFor(gameMode);
        }

        /// <summary> Resets <see cref="UIElement.IsEnabled"/> statuses of nation and branch tabs. </summary>
        public void ResetTabRestrictions()
        {
            foreach (var nationTab in _nationTabs.Values)
            {
                if (nationTab.Tag is ENation nation && _nationControls.TryGetValue(nation, out var nationControl))
                {
                    nationTab.IsEnabled = true;
                    nationControl.ResetTabRestrictions();
                }
            }
        }

        /// <summary> Disables all nation and branch tabs not specified in the parameters. </summary>
        /// <param name="nations"> Nation tabs to keep enabled. </param>
        /// <param name="branches"> Branch tabs to keep enabled. </param>
        public void EnableOnly(IEnumerable<ENation> nations, IEnumerable<EBranch> branches)
        {
            foreach (var nationTab in _nationTabs.Values)
            {
                if (nationTab.Tag is ENation tabNation && _nationControls.TryGetValue(tabNation, out var nationControl))
                {
                    nationControl.EnableOnly(branches);
                    nationTab.IsEnabled = tabNation.IsIn(nations);
                }
            }
        }

        /// <summary> Disables all nation and branch tabs not specified in the parameters. </summary>
        /// <param name="nation"> The nation tab to keep enabled. </param>
        /// <param name="branches"> Branch tabs to keep enabled. </param>
        public void EnableOnly(ENation nation, IEnumerable<EBranch> branches) => EnableOnly(new List<ENation> { nation }, branches);

        /// <summary> Focuses on a research tree by given parameters. </summary>
        /// <param name="nation"> The nation whose <paramref name="branch"/> to put into focus. </param>
        /// <param name="branch"> The branch to put into focus. </param>
        public void FocusResearchTree(ENation nation, EBranch branch)
        {
            if (_nationTabs.TryGetValue(nation, out var nationTab))
            {
                if (!nationTab.IsEnabled)
                    return;

                _tabControl.SelectedItem = nationTab;
            }

            if (_nationControls.TryGetValue(nation, out var nationControl))
                nationControl.FocusResearchTree(branch);
        }

        /// <summary> Get the research tree nation control appropriate to the given vehicle. </summary>
        /// <param name="vehicle"> The vehicle whose research tree nation control to look for. </param>
        /// <returns></returns>
        private ResearchTreeNationControl GetNationControl(IVehicle vehicle) =>
            _nationControls.TryGetValue(vehicle.Nation.AsEnumerationItem, out var nationControl) ? nationControl : null;

        /// <summary> Scrolls the research tree to bring the specified vehicle into view. </summary>
        /// <param name="vehicle"> The vehicle to bring into view. </param>
        /// <param name="changeTabs"> Whether to switch tabs to . </param>
        public void BringIntoView(IVehicle vehicle, bool changeTabs = false)
        {
            if (changeTabs)
                FocusResearchTree(vehicle.Nation.AsEnumerationItem, vehicle.Branch.AsEnumerationItem);

            GetNationControl(vehicle)?.BringIntoView(vehicle);
        }

        /// <summary> Highlights the specified vehicle in the reseatch tree. </summary>
        /// <param name="vehicle"> The vehicle to highlight. </param>
        public void Highlight(IVehicle vehicle) => GetNationControl(vehicle)?.Highlight(vehicle);

        /// <summary> Removes the highlight from the specified vehicle in the reseatch tree. </summary>
        /// <param name="vehicle"> The vehicle to remove highlight from. </param>
        public void RemoveHighlight(IVehicle vehicle) => GetNationControl(vehicle)?.RemoveHighlight(vehicle);
    }
}