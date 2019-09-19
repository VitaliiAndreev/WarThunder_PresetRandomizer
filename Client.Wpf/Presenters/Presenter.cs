using Client.Wpf.Commands.Interfaces;
using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Strategies.Interfaces;
using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Presenters
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. </summary>
    public abstract class Presenter : IPresenter
    {
        #region Properties

        /// <summary> The parent window. </summary>
        public IBaseWindow Owner { get; private set; }

        /// <summary> An instance of a strategy. </summary>
        public IStrategy Strategy { get; private set; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new presenter. </summary>
        /// <param name="strategy"> An instance of a strategy. </param>
        public Presenter(IStrategy strategy)
        {
            Strategy = strategy;
        }

        #endregion Constructors

        /// <summary> Sets the value of the <see cref="Owner"/> property. </summary>
        /// <param name="owner"> The window to set a the owner. </param>
        public void SetParentWindow(IBaseWindow owner) => Owner = owner;

        /// <summary> Closes the window set as the <see cref="Owner"/>. </summary>
        public void CloseParentWindow() => Owner.Close();

        /// <summary> Gets a command with the given name. </summary>
        /// <param name="commandName"> The name of the command (see <see cref="ECommandName"/>). </param>
        public ICustomCommand GetCommand(ECommandName commandName) => Strategy?.GetCommand(commandName);

        /// <summary> Executes a command with the given name. </summary>
        /// <param name="commandName"> The name of the command (see <see cref="ECommandName"/>). </param>
        public void ExecuteCommand(ECommandName commandName) => GetCommand(commandName)?.Execute(this);

        /// <summary> Removes references to instances on adjacent levels. </summary>
        public void Dispose()
        {
            Owner = null;
            Strategy = null;
        }
    }
}