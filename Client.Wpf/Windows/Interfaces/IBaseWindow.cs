using Client.Wpf.Windows.Interfaces.Base;

namespace Client.Wpf.Windows.Interfaces
{
    public interface IBaseWindow : IWindowWithLogger, IWindowWithPresenter, ILocalizedWindow
    {
    }
}