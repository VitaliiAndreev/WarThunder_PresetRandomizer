using Client.Wpf.Windows.Interfaces;
using Core.DataBase.WarThunder.Enumerations;

namespace Client.Wpf.Presenters.Interfaces
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="IMainWindow"/>. </summary>
    public interface IMainWindowPresenter : IPresenter
    {
        #region Properties

        /// <summary> The parent window. </summary>
        new IMainWindow Owner { get; }

        /// <summary> The currently selected game mode. </summary>
        public EGameMode CurrentGameMode { get; set; }

        #endregion Properties
    }
}