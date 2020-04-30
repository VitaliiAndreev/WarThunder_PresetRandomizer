using Client.Wpf.Windows.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Connectors;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Objects;
using Core.Organization.Collections;
using Core.Organization.Enumerations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace Client.Wpf.Presenters.Interfaces
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="IMainWindow"/>. </summary>
    public interface IMainWindowPresenter : IPresenter
    {
        #region Properties

        /// <summary> The parent window. </summary>
        new IMainWindow Owner { get; }

        /// <summary> The currently selected randomisation method. </summary>
        ERandomisation Randomisation { get; set; }

        /// <summary> The currently selected game mode. </summary>
        EGameMode CurrentGameMode { get; set; }

        /// <summary> Branches enabled for preset generation. </summary>
        IList<EBranch> EnabledBranches { get; }

        /// <summary> Vehicle branch tags enabled for preset generation. </summary>
        IList<EVehicleBranchTag> EnabledVehicleBranchTags { get; }

        /// <summary> Vehicles classes enabled for preset generation. </summary>
        IList<EVehicleClass> EnabledVehicleClasses { get; }

        /// <summary> Vehicles subclasses enabled for preset generation. </summary>
        IList<EVehicleSubclass> EnabledVehicleSubclasses { get; }

        /// <summary> Vehicles branch tags enabled for preset generation, grouped by branches. </summary>
        IDictionary<EBranch, IEnumerable<EVehicleBranchTag>> EnabledVehicleBranchTagsByBranches { get; }

        /// <summary> Vehicles classes enabled for preset generation, grouped by branches. </summary>
        IDictionary<EBranch, IEnumerable<EVehicleClass>> EnabledVehicleClassesByBranches { get; }

        /// <summary> Nations enabled for preset generation. </summary>
        IList<ENation> EnabledNations { get; }

        /// <summary> Countries enabled for preset generation. </summary>
        IList<NationCountryPair> EnabledCountries { get; }

        /// <summary> Countries enabled for preset generation, grouped by nations. </summary>
        IDictionary<ENation, IEnumerable<ECountry>> EnabledCountriesByNations { get; }

        /// <summary> Ranks enabled for preset generation. </summary>
        IList<ERank> EnabledRanks { get; }

        /// <summary> <see cref="IVehicle.EconomicRank"/> intervals enabled for preset generation. </summary>
        IDictionary<ENation, Interval<int>> EnabledEconomicRankIntervals { get; }

        /// <summary> Gaijin ID's of vehicle enabled for preset generation. </summary>
        IList<string> EnabledVehicleGaijinIds { get; }

        /// <summary> Generated presets. </summary>
        IDictionary<EPreset, Preset> GeneratedPresets { get; }

        /// <summary> The preset to display. </summary>
        EPreset CurrentPreset { get; set; }

        #region Properties: Vehicle List

        IEnumerable VehicleListSource { get; set; }

        IVehicle ReferencedVehicle { get; set; }

        bool IncludeHeadersOnRowCopy { get; set; }

        #endregion Properties: Vehicle List

        #endregion Properties
        #region Methods: Event Raisers

        void RaiseSwitchToResearchTreeCommandCanExecuteChanged();

        #endregion Methods: Event Raisers
        #region Methods: Status Bar

        void ToggleLongOperationIndicator(bool show);

        void PerformLongOperation(Action doOperation);

        #endregion Methods: Status Bar
        #region Methods: Queries

        /// <summary> Gets all empty branches (their tabs should be disabled). </summary>
        /// <returns></returns>
        IDictionary<ENation, IEnumerable<EBranch>> GetEmptyBranches();

        /// <summary> Gets all valid branches. </summary>
        /// <returns></returns>
        IEnumerable<EBranch> GetValidBraches();

        #endregion Methods: Queries

        /// <summary> Checks whether the given <paramref name="branch"/> has any <see cref="EVehicleBranchTag"/> items enabled or not. </summary>
        /// <param name="branch"> The branch to check. </param>
        /// <returns></returns>
        bool BranchHasVehicleBranchTagsEnabled(EBranch branch);

        /// <summary> Checks whether the given <paramref name="branch"/> has any <see cref="EVehicleClass"/> items enabled or not. </summary>
        /// <param name="branch"> The branch to check. </param>
        /// <returns></returns>
        bool BranchHasVehicleClassesEnabled(EBranch branch);

        /// <summary> Checks whether the given <paramref name="vehicleClass"/> has any <see cref="EVehicleSubclass"/> items enabled or not. </summary>
        /// <param name="vehicleClass"> The vehicle class to check. </param>
        /// <returns></returns>
        bool VehicleClassHasSubclassesEnabled(EVehicleClass vehicleClass);

        /// <summary> Checks whether the given <paramref name="nation"/> has any <see cref="ECountry"/> items enabled or not. </summary>
        /// <param name="nation"> The nation to check. </param>
        /// <returns></returns>
        bool NationHasCountriesEnabled(ENation nation);

        /// <summary> Resets preset control to their default states. </summary>
        void ResetPresetControls();

        /// <summary> Loads <see cref="GeneratedPresets"/>. </summary>
        void LoadPresets();

        /// <summary> Displays a message that no vehicles suit the criteria. </summary>
        void ShowNoResults();

        /// <summary> Displays a message that no vehicles suit the criteria with additional information. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="mainBranch"> The branch. </param>
        void ShowNoVehicles(ENation nation, EBranch mainBranch);

        /// <summary> Displays the specified preset from <see cref="GeneratedPresets"/>. </summary>
        /// <param name="preset"> The preset to display. </param>
        void DisplayPreset(EPreset preset);

        /// <summary> Resets <see cref="UIElement.IsEnabled"/> statuses of nation and branch tabs of the research tree. </summary>
        void ResetResearchTreeTabRestrictions();

        /// <summary> Disables all nation and branch tabs of the research tree not specified in the parameters. </summary>
        /// <param name="nation"> The nation tab to keep enabled. </param>
        /// <param name="branches"> Branch tabs to keep enabled. </param>
        void EnableOnly(ENation nation, IEnumerable<EBranch> branches);

        /// <summary> Focuses on a research tree by given parameters. </summary>
        /// <param name="nation"> The nation whose <paramref name="branch"/> to put into focus. </param>
        /// <param name="branch"> The branch to put into focus. </param>
        void FocusResearchTree(ENation nation, EBranch branch);

        #region Methods: CanBeBroughtIntoView()

        bool CanBeBroughtIntoView(IVehicle vehicle);

        bool CanBeBroughtIntoView(string vehicleGaijinId);

        #endregion Methods: CanBeBroughtIntoView()
        #region Methods: BringIntoView()

        /// <summary> Scrolls the research tree to bring the specified vehicle into view. </summary>
        /// <param name="vehicle"> The vehicle to bring into view. </param>
        void BringIntoView(IVehicle vehicle);

        void BringIntoView(string vehicleGaijinId);

        #endregion Methods: BringIntoView()
    }
}