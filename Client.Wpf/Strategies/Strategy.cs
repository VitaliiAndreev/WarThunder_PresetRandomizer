using Client.Wpf.Commands.Interfaces;
using Client.Wpf.Enumerations;
using Client.Wpf.Strategies.Interfaces;
using System.Collections.Generic;

namespace Client.Wpf.Strategies
{
    /// <summary> A strategy that is used to set up available commands. </summary>
    public abstract class Strategy : IStrategy
    {
        #region Fields

        /// <summary> Commands, available by their names. </summary>
        protected readonly IDictionary<ECommandName, ICustomCommand> _commands;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new strategy. </summary>
        public Strategy()
        {
            _commands = new Dictionary<ECommandName, ICustomCommand>();

            InitializeCommands();
        }

        #endregion Constructors

        /// <summary> Initializes and assigns commands to be used with this strategy. </summary>
        protected virtual void InitializeCommands()
        {
        }

        /// <summary> Returns a command with the given name, if available. </summary>
        /// <param name="commandName"> The name of the command. </param>
        /// <returns></returns>
        public ICustomCommand GetCommand(ECommandName commandName) => _commands.TryGetValue(commandName, out var command) ? command : null;
    }
}