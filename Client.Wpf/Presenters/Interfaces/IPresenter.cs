using Client.Wpf.Commands.Interfaces;
using Client.Wpf.Enumerations;
using Client.Wpf.Strategies.Interfaces;
using Client.Wpf.Windows.Interfaces;
using System;

namespace Client.Wpf.Presenters.Interfaces
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. </summary>
    public interface IPresenter : IDisposable
    {
        #region Properties

        /// <summary> The parent window. </summary>
        IBaseWindow Owner { get; }

        /// <summary> An instance of a strategy. </summary>
        IStrategy Strategy { get; }

        #endregion Properties

        /// <summary> Sets the value of the <see cref="Owner"/> property. </summary>
        /// <param name="owner"> The window to set a the owner. </param>
        void SetParentWindow(IBaseWindow owner);

        /// <summary> Closes the window set as the <see cref="Owner"/>. </summary>
        void CloseParentWindow();

        /// <summary> Gets a command with the given name. </summary>
        /// <param name="commandName"> The name of the command (see <see cref="ECommandName"/>). </param>
        ICustomCommand GetCommand(ECommandName commandName);

        /// <summary> Executes a command with the given name. </summary>
        /// <param name="commandName"> The name of the command (see <see cref="ECommandName"/>). </param>
        void ExecuteCommand(ECommandName commandName);
    }
}