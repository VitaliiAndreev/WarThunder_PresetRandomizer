using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Presenters.Interfaces
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="IMainWindow"/>. </summary>
    public interface IMainWindowPresenter : IPresenter
    {
        #region Properties

        /// <summary> The parent window. </summary>
        new IMainWindow Owner { get; }

        #endregion Properties
    }
}