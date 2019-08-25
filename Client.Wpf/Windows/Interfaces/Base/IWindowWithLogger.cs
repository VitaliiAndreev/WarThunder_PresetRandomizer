using Core.Helpers.Logger.Interfaces;

namespace Client.Wpf.Windows.Interfaces.Base
{
    /// <summary> A window with an <see cref="IActiveLogger"/> attached. </summary>
    public interface IWindowWithLogger : IWindow
    {
        /// <summary> An instance of an active logger. </summary>
        IActiveLogger Log { get; }
    }
}