using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.Organization.Enumerations;
using System.Collections.Generic;
using System.Windows;

namespace Client.Wpf.Windows.Interfaces
{
    /// <summary> The main window. </summary>
    public interface IMainWindow : IBaseWindow
    {
        /// <summary> An instance of a presenter. </summary>
        new IMainWindowPresenter Presenter { get; }

        /// <summary> Resets preset control to their default states. </summary>
        void ResetPresetControls();

        /// <summary> Loads <see cref="IMainWindowPresenter.GeneratedPresets"/>. </summary>
        void LoadPresets();

        /// <summary> Displays the specified preset from <see cref="IMainWindowPresenter.GeneratedPresets"/>. </summary>
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
        public void FocusResearchTree(ENation nation, EBranch branch);
    }
}