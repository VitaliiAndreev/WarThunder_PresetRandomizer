using Client.Wpf.Presenters.Interfaces;

namespace Client.Wpf.Windows.Interfaces
{
    /// <summary> The "About" window. </summary>
    public interface IAboutWindow : IBaseWindow
    {
        /// <summary> An instance of a presenter. </summary>
        new IAboutWindowPresenter Presenter { get; }
    }
}