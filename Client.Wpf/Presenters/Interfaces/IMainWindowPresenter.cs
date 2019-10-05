using Client.Wpf.Windows.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Objects;
using Core.Organization.Collections;
using Core.Organization.Enumerations;
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

        /// <summary> The currently selected game mode. </summary>
        EGameMode CurrentGameMode { get; set; }

        /// <summary> Branches enabled for preset generation. </summary>
        IList<EBranch> EnabledBranches { get; }

        /// <summary> Nations enabled for preset generation. </summary>
        IList<ENation> EnabledNations { get; }

        /// <summary> <see cref="IVehicle.EconomicRank"/> intervals enabled for preset generation. </summary>
        IDictionary<ENation, Interval<int>> EnabledEconomicRankIntervals { get; }

        /// <summary> Generated presets. </summary>
        IDictionary<EPreset, Preset> GeneratedPresets { get; }

        /// <summary> The preset to display. </summary>
        EPreset CurrentPreset { get; set; }

        #endregion Properties

        /// <summary> Gets all empty branches (their tabs should be disabled). </summary>
        /// <returns></returns>
        IDictionary<ENation, IEnumerable<EBranch>> GetEmptyBranches();

        /// <summary> Gets all valid branches. </summary>
        /// <returns></returns>
        IEnumerable<EBranch> GetValidBraches();

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

        /// <summary> Scrolls the research tree to bring the specified vehicle into view. </summary>
        /// <param name="vehicle"> The vehicle to bring into view. </param>
        void BringIntoView(IVehicle vehicle);
    }
}