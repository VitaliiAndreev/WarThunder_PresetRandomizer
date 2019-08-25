using System.Windows.Input;

namespace Client.Wpf.Commands.Interfaces
{
    public interface ICustomCommand : ICommand
    {
        /// <summary> Raises <see cref="ICommand.CanExecuteChanged"/>. </summary>
        /// <param name="presenterAsObject"></param>
        void RaiseCanExecuteChanged(object presenterAsObject);
    }
}