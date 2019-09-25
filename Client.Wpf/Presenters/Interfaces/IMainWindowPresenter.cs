using Client.Wpf.Windows.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
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

        #endregion Properties
    }
}