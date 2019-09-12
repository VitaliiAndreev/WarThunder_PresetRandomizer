using Client.Wpf.Strategies.Interfaces;
using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Strategies
{
    /// <summary> A strategy that is used to set commands available in the <see cref="IMainWindow"/>. </summary>
    public class MainWindowStrategy : Strategy, IMainWindowStrategy
    {
        /// <summary> Initializes and assigns commands to be used with this strategy. </summary>
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
        }
    }
}