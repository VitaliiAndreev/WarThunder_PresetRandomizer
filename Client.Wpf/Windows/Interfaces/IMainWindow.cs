using Client.Wpf.Presenters.Interfaces;

namespace Client.Wpf.Windows.Interfaces
{
    /// <summary> The main window. </summary>
    public interface IMainWindow : IBaseWindow
    {
        /// <summary> An instance of a presenter. </summary>
        new IMainWindowPresenter Presenter { get; }
    }
}