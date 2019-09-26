using Client.Wpf.Windows.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.Organization.Collections;
using Core.Organization.Enumerations;
using System.Collections.Generic;

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

        /// <summary> Generated presets. </summary>
        IDictionary<EPreset, Preset> GeneratedPresets { get; }

        /// <summary> The preset to display. </summary>
        EPreset CurrentPreset { get; set; }

        #endregion Properties

        /// <summary> Resets preset control to their default states. </summary>
        void ResetPresetControls();

        /// <summary> Loads <see cref="GeneratedPresets"/>. </summary>
        void LoadPresets();

        /// <summary> Displays the specified preset from <see cref="GeneratedPresets"/>. </summary>
        /// <param name="preset"> The preset to display. </param>
        void DisplayPreset(EPreset preset);
    }
}