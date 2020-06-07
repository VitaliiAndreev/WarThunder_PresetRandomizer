using Client.Shared.Wpf.Extensions;
using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeControl.xaml. </summary>
    public partial class ResearchTreeControl : LocalisedUserControl
    {
        #region Fields

        /// <summary> The map of the nation enumeration onto corresponding tabs. </summary>
        private readonly IDictionary<ENation, TabItem> _nationTabs;
        /// <summary> The map of the nation enumeration onto corresponding controls. </summary>
        private readonly IDictionary<ENation, ResearchTreeNationControl> _nationControls;
        /// <summary> Whether the control is disabled by default, e.g. when the underlying nation is empty. </summary>
        private readonly IDictionary<ENation, bool> _isEnabledByDefault;

        private bool _initialised;

        private IMainWindowPresenter _presenter;

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

        private void ToggleAllVehicles(ToggleButton toggleButton, bool onlyNonResearchable = false)
        {
            var buttonOriginalState = toggleButton.IsChecked;

            foreach (var vehicleCellControl in GetVehicleControls())
            {
                if (vehicleCellControl.IsToggled != buttonOriginalState)
                {
                    if (onlyNonResearchable && vehicleCellControl.Vehicle.IsResearchable)
                        continue;

                    vehicleCellControl.HandleClick();
                }
            }
            _toggleAllVehiclesButton.IsChecked = AllVehiclesAreToggled();
            _toggleAllNonResearchableVehiclesButton.IsChecked = AllVehiclesAreToggled(true);
        }

        /// <summary> Toggles all vehicles on/off. </summary>
        /// <param name="sender"> The event sender. <see cref="ToggleButton"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnToggleAllClick(object sender, EventArgs eventArguments)
        {
            if (sender is ToggleButton toggleButton)
                ToggleAllVehicles(toggleButton);
        }

        /// <summary> Toggles all non-researchable vehicles on/off. </summary>
        /// <param name="sender"> The event sender. <see cref="ToggleButton"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnToggleAllNonResearchableClick(object sender, EventArgs eventArguments)
        {
            if (sender is ToggleButton toggleButton)
                ToggleAllVehicles(toggleButton, true);
        }

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
        #region Methods: Overrides

        /// <summary> Applies localisation to visible text on the control. </summary>
        public override void Localise()
        {
            base.Localise();

            static string localise(string localisationKey) => ApplicationHelpers.LocalisationManager.GetLocalisedString(localisationKey);

            foreach (var tab in _tabControl.Items.OfType<TabItem>())
            {
                if (tab.Header is WrapPanel panel && panel.Children.OfType<TextBlock>().FirstOrDefault() is TextBlock textBlock)
                    textBlock.Text = localise(tab.Tag.ToString());
            }
            foreach (var nationControl in _nationControls.Values)
            {
                nationControl.Localise();
            }

            _toggleAllVehiclesButtonHeader.Text = localise(ELocalisationKey.All);
            _toggleAllVehiclesButton.ToolTip = localise(ELocalisationKey.SelectAllVehicles);

            _toggleAllNonResearchableVehiclesButtonHeader.Text = localise(ELocalisationKey.AllNonResearchable);
            _toggleAllNonResearchableVehiclesButton.ToolTip = localise(ELocalisationKey.SelectAllNonResearchableVehicles);
        }

        #endregion Methods: Overrides
        #region Methods: Initialisation

        public void Initialise(IMainWindowPresenter presenter)
        {
            if (!_initialised && presenter is IMainWindowPresenter)
            {
                _presenter = presenter;

                foreach (var control in _nationControls.Values)
                    control.Initialise(_presenter);

                _initialised = true;
            }
        }

        private void UpdateButtonStates()
        {
            _toggleAllVehiclesButton.IsChecked = AllVehiclesAreToggled();
            _toggleAllNonResearchableVehiclesButton.IsChecked = AllVehiclesAreToggled(true);
        }

        /// <summary> Populates tabs with appropriate research trees. </summary>
        /// <param name="enabledVehicleGaijinIds"> Gaijin IDs of vehicles enabled by dafault. </param>
        /// <param name="loadingTracker"> An instance of a presenter to communicate with the GUI loading window. </param>
        public void Populate(IEnumerable<string> enabledVehicleGaijinIds, IGuiLoadingWindowPresenter loadingTracker)
        {
            loadingTracker.NationsPopulated = EInteger.Number.Zero;
            loadingTracker.NationsToPopulate = _nationTabs.Count;

            foreach (var nationTabKeyValuePair in _nationTabs)
            {
                var nation = nationTabKeyValuePair.Key;
                var tab = nationTabKeyValuePair.Value;

                loadingTracker.CurrentlyPopulatedNation = ApplicationHelpers.LocalisationManager.GetLocalisedString(nation.ToString());

                if (ApplicationHelpers.Manager.ResearchTrees.TryGetValue(nation, out var researchTree))
                {
                    _isEnabledByDefault[nation] = true;
                    _nationControls[nation].Populate(researchTree, enabledVehicleGaijinIds, loadingTracker);
                }
                else
                {
                    _isEnabledByDefault[nation] = false;
                    tab.IsEnabled = false;
                }
                loadingTracker.CurrentlyPopulatedNation = string.Empty;
                loadingTracker.NationsPopulated++;
            }
            UpdateButtonStates();
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
                Style = this.GetStyle(EStyleKey.TextBlock.TextBlockFontSize16),
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

                nationControl.Initialise(_presenter);
            }
        }

        #endregion Methods: Initialisation
        #region Methods: Checks

        /// <summary> Checks whether all vehicles in the nation are toggled on. </summary>
        /// <returns></returns>
        internal bool AllVehiclesAreToggled(bool onlyNonResearchable = false) =>
            _nationControls.Values.All(control => control.AllVehiclesAreToggled(onlyNonResearchable));

        #endregion Methods: Checks

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

        private IEnumerable<ResearchTreeCellVehicleControl> GetVehicleControls() => _nationControls.Values.SelectMany(control => control.GetVehicleControls());

        private void OnButtonBubbledClick(object sender, RoutedEventArgs e)
        {
            UpdateButtonStates();
        }
    }
}