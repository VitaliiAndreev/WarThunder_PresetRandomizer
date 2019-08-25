using Client.Wpf.Commands.Interfaces;
using Client.Wpf.Enumerations;
using System;

namespace Client.Wpf.Commands
{
    /// <summary> A command that encapsulates a transactional operation with the client. </summary>
    public class Command : ICustomCommand
    {
        #region Properties

        /// <summary> The name of the command. </summary>
        public ECommandName Name { get; }

        #endregion Properties
        #region Events

        /// <summary> Occurs when changes occur that affect whether or not the command should execute. </summary>
        public event EventHandler CanExecuteChanged;

        #endregion Events
        #region Constructors

        /// <summary> Creates a new command. </summary>
        /// <param name="name"> The name of the command. </param>
        public Command(ECommandName name)
        {
            Name = name;
        }

        #endregion Constructors

        /// <summary> Raises <see cref="CanExecuteChanged"/>. </summary>
        /// <param name="presenterAsObject"></param>
        public void RaiseCanExecuteChanged(object presenterAsObject) =>
            CanExecuteChanged?.Invoke(presenterAsObject, new EventArgs());

        /// <summary> Defines the method that determines whether the command can execute in its current state. </summary>
        /// <param name="parameter"> Data used by the command. If the command does not require data to be passed, this object can be set to null. </param>
        /// <returns></returns>
        public virtual bool CanExecute(object parameter) => true;

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. If the command does not require data to be passed, this object can be set to null. </param>
        public virtual void Execute(object parameter)
        {
        }
    }
}