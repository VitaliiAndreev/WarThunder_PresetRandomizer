using Client.Wpf.Presenters.Interfaces;

namespace Client.Wpf.Windows.Interfaces.Base
{
    /// <summary> A window with an <see cref="IPresenter"/> attached. </summary>
    public interface IWindowWithPresenter : IWindow
    {
        /// <summary> An instance of a presenter. </summary>
        IPresenter Presenter { get; }
    }
}