using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Strategies.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Extensions;
using Core.Objects;
using Core.Organization.Collections;
using Core.Organization.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Wpf.Presenters
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="IMainWindow"/>. </summary>
    public class MainWindowPresenter : Presenter, IMainWindowPresenter
    {
        #region Properties

        /// <summary> The parent window. </summary>
        new public IMainWindow Owner => base.Owner as IMainWindow;

        /// <summary> The currently selected randomisation method. </summary>
        public ERandomisation Randomisation { get; set; }

        /// <summary> The currently selected game mode. </summary>
        public EGameMode CurrentGameMode { get; set; }

        /// <summary> Branches enabled for preset generation. </summary>
        public IList<EBranch> EnabledBranches { get; }

        /// <summary> Vehicles classes enabled for preset generation. </summary>
        public IList<EVehicleClass> EnabledVehicleClasses { get; }

        /// <summary> Vehicles subclasses enabled for preset generation. </summary>
        public IList<EVehicleSubclass> EnabledVehicleSubclasses { get; }

        /// <summary> Vehicles classes enabled for preset generation, grouped by branches. </summary>
        public IDictionary<EBranch, IEnumerable<EVehicleClass>> EnabledVehicleClassesByBranches =>
            EnabledVehicleClasses
                .GroupBy(vehicleClass => vehicleClass.GetBranch())
                .ToDictionary(group => group.Key, group => group.AsEnumerable())
            ;

        /// <summary> Vehicles classes enabled for preset generation, grouped by branches. </summary>
        public IDictionary<EVehicleClass, IEnumerable<EVehicleSubclass>> EnabledVehicleSubclassesByClasses =>
            EnabledVehicleSubclasses
                .GroupBy(vehicleSubclass => vehicleSubclass.GetVehicleClass())
                .ToDictionary(group => group.Key, group => group.AsEnumerable())
            ;

        /// <summary> Nations enabled for preset generation. </summary>
        public IList<ENation> EnabledNations { get; }

        /// <summary> Countries enabled for preset generation. </summary>
        public IList<NationCountryPair> EnabledCountries { get; }

        /// <summary> Countries enabled for preset generation, grouped by nations. </summary>
        public IDictionary<ENation, IEnumerable<ECountry>> EnabledCountriesByNations =>
            EnabledCountries
                .GroupBy(nationCountryPair => nationCountryPair.Nation)
                .ToDictionary(group => group.Key, group => group.Select(countryCollection => countryCollection.Country))
            ;

        /// <summary> <see cref="IVehicle.EconomicRank"/> intervals enabled for preset generation. </summary>
        public IDictionary<ENation, Interval<int>> EnabledEconomicRankIntervals { get; }

        /// <summary> Gaijin ID's of vehicle enabled for preset generation. </summary>
        public IList<string> EnabledVehicleGaijinIds { get; }

        /// <summary> Generated presets. </summary>
        public IDictionary<EPreset, Preset> GeneratedPresets { get; }

        /// <summary> The preset to display. </summary>
        public EPreset CurrentPreset { get; set; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new presenter. </summary>
        /// <param name="strategy"> An instance of a strategy. </param>
        public MainWindowPresenter(IMainWindowStrategy strategy)
            : base(strategy)
        {
            Randomisation = WpfSettings.RandomisationAsEnumerationItem;
            CurrentGameMode = WpfSettings.CurrentGameModeAsEnumerationItem;
            EnabledBranches = new List<EBranch>(WpfSettings.EnabledBranchesCollection);
            EnabledVehicleClasses = new List<EVehicleClass>(WpfSettings.EnabledVehicleClassesCollection);
            EnabledVehicleSubclasses = new List<EVehicleSubclass>(WpfSettings.EnabledVehicleSubclassesCollection);
            EnabledNations = new List<ENation>(WpfSettings.EnabledNationsCollection);
            EnabledCountries = new List<NationCountryPair>(WpfSettings.EnabledCountriesCollection);
            EnabledEconomicRankIntervals = new Dictionary<ENation, Interval<int>>(WpfSettings.EnabledEconomicRankIntervals);
            EnabledVehicleGaijinIds = new List<string>(WpfSettings.EnabledVehiclesCollection);
            GeneratedPresets = new Dictionary<EPreset, Preset>();
            CurrentPreset = EPreset.Primary;
        }

        #endregion Constructors

        /// <summary> Gets all empty branches (their tabs should be disabled). </summary>
        /// <returns></returns>
        public IDictionary<ENation, IEnumerable<EBranch>> GetEmptyBranches() => Owner.GetEmptyBranches();

        /// <summary> Gets all valid branches. </summary>
        /// <returns></returns>
        public IEnumerable<EBranch> GetValidBraches()
        {
            var allBranches = Enum.GetValues(typeof(EBranch)).OfType<EBranch>().Where(branch => branch.IsValid());
            var allEmptyBranches = GetEmptyBranches();
            var validBranches = new HashSet<EBranch>();

            foreach (var nation in EnabledNations)
                if (allEmptyBranches.TryGetValue(nation, out var emptyBranches))
                    validBranches.AddRange(allBranches.Except(emptyBranches));

            return validBranches;
        }

        /// <summary> Checks whether the given branch has any vehicle classes enabled or not. </summary>
        /// <param name="branch"> The branch to check. </param>
        /// <returns></returns>
        public bool BranchHasVehicleClassesEnabled(EBranch branch) =>
            EnabledVehicleClassesByBranches.TryGetValue(branch, out var vehicleClasses) && vehicleClasses.Any();

        /// <summary> Checks whether the given <paramref name="vehicleClass"/> has any <see cref="EVehicleSubclass"/> items enabled or not. </summary>
        /// <param name="vehicleClass"> The vehicle class to check. </param>
        /// <returns></returns>
        public bool VehicleClassHasSubclassesEnabled(EVehicleClass vehicleClass) =>
            EnabledVehicleSubclassesByClasses.TryGetValue(vehicleClass, out var vehicleSubclasses) && vehicleSubclasses.Any();

        /// <summary> Checks whether the given <paramref name="nation"/> has any <see cref="ECountry"/> items enabled or not. </summary>
        /// <param name="nation"> The nation to check. </param>
        /// <returns></returns>
        public bool NationHasCountriesEnabled(ENation nation) =>
            EnabledCountriesByNations.TryGetValue(nation, out var countries) && countries.Any();

        /// <summary> Resets preset control to their default states. </summary>
        public void ResetPresetControls() => Owner.ResetPresetControls();

        /// <summary> Loads <see cref="GeneratedPresets"/>. </summary>
        public void LoadPresets() => Owner.LoadPresets();

        /// <summary> Displays a message that no vehicles suit the criteria. </summary>
        public void ShowNoResults() => Owner.ShowNoResults();

        /// <summary> Displays a message that no vehicles suit the criteria with additional information. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="mainBranch"> The branch. </param>
        public void ShowNoVehicles(ENation nation, EBranch mainBranch) => Owner.ShowNoVehicles(nation, mainBranch);

        /// <summary> Displays the specified preset from <see cref="GeneratedPresets"/>. </summary>
        /// <param name="preset"> The preset to display. </param>
        public void DisplayPreset(EPreset preset) => Owner.DisplayPreset(preset);

        /// <summary> Resets <see cref="UIElement.IsEnabled"/> statuses of nation and branch tabs of the research tree. </summary>
        public void ResetResearchTreeTabRestrictions() => Owner.ResetResearchTreeTabRestrictions();

        /// <summary> Disables all nation and branch tabs of the research tree not specified in the parameters. </summary>
        /// <param name="nation"> The nation tab to keep enabled. </param>
        /// <param name="branches"> Branch tabs to keep enabled. </param>
        public void EnableOnly(ENation nation, IEnumerable<EBranch> branches) => Owner.EnableOnly(nation, branches);

        /// <summary> Focuses on a research tree by given parameters. </summary>
        /// <param name="nation"> The nation whose <paramref name="branch"/> to put into focus. </param>
        /// <param name="branch"> The branch to put into focus. </param>
        public void FocusResearchTree(ENation nation, EBranch branch) => Owner.FocusResearchTree(nation, branch);

        /// <summary> Scrolls the research tree to bring the specified vehicle into view. </summary>
        /// <param name="vehicle"> The vehicle to bring into view. </param>
        public void BringIntoView(IVehicle vehicle) => Owner.BringIntoView(vehicle);
    }
}