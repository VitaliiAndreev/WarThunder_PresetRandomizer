using Client.Wpf.Commands.MainWindow;
using Client.Wpf.Enumerations;
using Client.Wpf.Strategies.Interfaces;
using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Strategies
{
    /// <summary> A strategy that is used to set commands available in the <see cref="IAboutWindow"/>. </summary>
    public class AboutWindowStrategy : Strategy, IAboutWindowStrategy
    {
    }
}