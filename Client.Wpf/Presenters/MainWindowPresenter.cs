using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Strategies.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using Core.WarThunderExtractionToolsIntegration;
using System.Collections.Generic;

namespace Client.Wpf.Presenters
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="IMainWindow"/>. </summary>
    public class MainWindowPresenter : Presenter, IMainWindowPresenter
    {
        #region Properties

        /// <summary> The parent window. </summary>
        new public IMainWindow Owner => base.Owner as IMainWindow;

        /// <summary> The currently selected game mode. </summary>
        public EGameMode CurrentGameMode { get; set; }

        /// <summary> Branches enabled for preset generation. </summary>
        public IList<EBranch> EnabledBranches { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new presenter. </summary>
        /// <param name="strategy"> An instance of a strategy. </param>
        public MainWindowPresenter(IMainWindowStrategy strategy)
            : base(strategy)
        {
            EnabledBranches = new List<EBranch>(WpfSettings.EnabledBranchesCollection);
        }

        #endregion Constructors
    }
}