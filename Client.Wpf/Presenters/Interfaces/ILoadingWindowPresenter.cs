using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Presenters.Interfaces
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="ILoadingWindow"/>. </summary>
    public interface ILoadingWindowPresenter : IPresenter
    {
        /// <summary> The parent window. </summary>
        new ILoadingWindow Owner { get; }

        /// <summary> Indicates whether the initialization has been completed. </summary>
        bool InitializationComplete { get; set; }
    }
}