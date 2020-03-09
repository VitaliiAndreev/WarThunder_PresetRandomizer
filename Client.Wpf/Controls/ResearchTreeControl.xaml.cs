using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Extensions;
using NHibernate.Util;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        /// <summary> Whether the control is disabled by default, e.g. when the underlying nation is empty. </summary>
        private readonly IDictionary<ENation, bool> _isEnabledByDefault;

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

            _tabControl.SelectionChanged += OnTabChange;

            _nationTabs = new Dictionary<ENation, TabItem>();
            _nationControls = new Dictionary<ENation, ResearchTreeNationControl>();
            _isEnabledByDefault = new Dictionary<ENation, bool>();

            CreateControls();
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

                    if (_currentBranch.IsValid())
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

            foreach (var tab in _tabControl.Items.OfType<TabItem>())
            {
                if (tab.Header is WrapPanel panel && panel.Children.OfType<TextBlock>().FirstOrDefault() is TextBlock textBlock)
                    textBlock.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(tab.Tag.ToString());
            }
            foreach (var nationControl in _nationControls.Values)
            {
                nationControl.Localize();
            }
        }

        /// <summary> Creates a header for a tab control of the given <paramref name="nation"/>. </summary>
        /// <param name="nation"> The nation for whose tab to create a header for. </param>
        /// <returns></returns>
        private object CreateHeader(ENation nation)
        {
            var headerImage = new Image()
            {
                Style = this.GetStyle(EStyleKey.Image.FlagIcon16px),
            };
            var headerText = new TextBlock
            {
                Margin = new Thickness(5, 0, 0, 0),
                Text = nation.ToString(), // For the designer.
            };
            var headerPanel = new WrapPanel();

            if (!DesignerProperties.GetIsInDesignMode(this))
                headerImage.Source = Application.Current.MainWindow.FindResource(EReference.CountryIconKeys.TryGetValue(nation.GetBaseCountry(), out var iconKey) ? iconKey : string.Empty) as ImageSource;

            headerPanel.Children.Add(headerImage);
            headerPanel.Children.Add(headerText);

            return headerPanel;
        }

        /// <summary> Creates controls. </summary>
        private void CreateControls()
        {
            foreach (var nation in typeof(ENation).GetEnumValues().OfType<ENation>().Where(nation => nation.IsValid()))
            {
                var nationControl = new ResearchTreeNationControl()
                {
                    Tag = nation,
                };
                var tabItem = new TabItem()
                {
                    Tag = nation,
                    Header = CreateHeader(nation),
                    Content = nationControl,
                };
                _tabControl.Items.Add(tabItem);

                _nationControls.Add(nation, nationControl);
                _nationTabs.Add(nation, tabItem);
            }
        }

        /// <summary> Populates tabs with appropriate research trees. </summary>
        /// <param name="enabledVehicleGaijinIds"> Gaijin IDs of vehicles enabled by dafault. </param>
        public void Populate(IEnumerable<string> enabledVehicleGaijinIds)
        {
            foreach (var nationTabKeyValuePair in _nationTabs)
            {
                if (ApplicationHelpers.Manager.ResearchTrees.TryGetValue(nationTabKeyValuePair.Key, out var researchTree))
                {
                    _isEnabledByDefault[nationTabKeyValuePair.Key] = true;
                    _nationControls[nationTabKeyValuePair.Key].Populate(researchTree, enabledVehicleGaijinIds);

                    continue;
                }

                _isEnabledByDefault[nationTabKeyValuePair.Key] = false;
                nationTabKeyValuePair.Value.IsEnabled = false;
            }
        }

        /// <summary> Gets all empty branches (their tabs should be disabled). </summary>
        /// <returns></returns>
        public IDictionary<ENation, IEnumerable<EBranch>> GetEmptyBranches() => _nationControls.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value.GetEmptyBranches());

        /// <summary> Displays vehicle information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode for which to display the battle rating. </param>
        public void DisplayVehicleInformation(EGameMode gameMode)
        {
            foreach (var vehicleCell in _nationControls.Values)
                vehicleCell.DisplayVehicleInformation(gameMode);
        }

        /// <summary> Resets <see cref="UIElement.IsEnabled"/> statuses of nation and branch tabs. </summary>
        public void ResetTabRestrictions()
        {
            foreach (var nationTab in _nationTabs.Values)
            {
                if (nationTab.Tag is ENation nation && _nationControls.TryGetValue(nation, out var nationControl))
                {
                    nationTab.IsEnabled = _isEnabledByDefault[nation];
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