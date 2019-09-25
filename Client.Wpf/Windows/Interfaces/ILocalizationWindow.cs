using Client.Wpf.Presenters.Interfaces;

namespace Client.Wpf.Windows.Interfaces
{
    /// <summary> The localization window. </summary>
    public interface ILocalizationWindow : IBaseWindow
    {
        /// <summary> An instance of a presenter. </summary>
        new ILocalizationWindowPresenter Presenter { get; }
    }
}