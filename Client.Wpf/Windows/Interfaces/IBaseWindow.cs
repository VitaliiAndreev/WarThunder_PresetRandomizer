using Client.Wpf.Windows.Interfaces.Base;
using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace Client.Wpf.Windows.Interfaces
{
    public interface IBaseWindow : IWindowWithLogger, IWindowWithPresenter, ILocalisedWindow, ISupportInitialize
    {
        void Invoke(DispatcherPriority priority, Action method);
        object Invoke(DispatcherPriority priority, Func<object> method);

        void CloseSafely();
    }
}