using Client.Wpf.Presenters.Interfaces;

namespace Client.Wpf.Windows.Interfaces
{
    /// <summary> The loading window. </summary>
    public interface ILoadingWindow : IBaseWindow
    {
        /// <summary> An instance of a presenter. </summary>
        new ILoadingWindowPresenter Presenter { get; }

        /// <summary> Indicates whether the initialization has been completed. </summary>
        public bool InitializationComplete { get; set; }
    }
}