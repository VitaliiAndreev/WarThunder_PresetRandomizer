using Core.Helpers.Logger.Interfaces;

namespace Client.Wpf.Windows.Interfaces.Base
{
    /// <summary> The WPF client application. </summary>
    public interface IWpfClient
    {
        /// <summary> An instance of an active logger. </summary>
        IActiveLogger Log { get; }
    }
}