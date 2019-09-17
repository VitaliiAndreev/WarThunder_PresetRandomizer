using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Presenters.Interfaces
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="IAboutWindow"/>. </summary>
    public interface IAboutWindowPresenter : IPresenter
    {
        /// <summary> The parent window. </summary>
        new IAboutWindow Owner { get; }
    }
}