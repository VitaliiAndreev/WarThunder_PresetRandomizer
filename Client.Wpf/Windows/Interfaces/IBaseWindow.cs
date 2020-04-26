using Client.Wpf.Windows.Interfaces.Base;
using System.ComponentModel;

namespace Client.Wpf.Windows.Interfaces
{
    public interface IBaseWindow : IWindowWithLogger, IWindowWithPresenter, ILocalizedWindow, ISupportInitialize
    {
    }
}