using Client.Wpf.Strategies.Interfaces;

namespace Client.Wpf.Strategies
{
    public class MainWindowStrategy : Strategy, IMainWindowStrategy
    {
        /// <summary> Initializes and assigns commands to be used with this strategy. </summary>
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
        }
    }
}