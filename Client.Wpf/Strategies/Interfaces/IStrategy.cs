using Client.Wpf.Commands.Interfaces;
using Client.Wpf.Enumerations;

namespace Client.Wpf.Strategies.Interfaces
{
    /// <summary> A strategy that is used to set up available commands. </summary>
    public interface IStrategy
    {
        /// <summary> Returns a command with the given name, if available. </summary>
        /// <param name="commandName"> The name of the command. </param>
        /// <returns></returns>
        ICustomCommand GetCommand(ECommandName commandName);
    }
}