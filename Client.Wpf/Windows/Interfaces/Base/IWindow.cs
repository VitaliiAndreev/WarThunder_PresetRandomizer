using System;

namespace Client.Wpf.Windows.Interfaces.Base
{
    /// <summary> A window. </summary>
    public interface IWindow : IDisposable
    {
        /// <summary> Opens a window and returns only when the newly opened window is closed. </summary>
        /// <returns></returns>
        bool? ShowDialog();
 
        /// <summary> Opens a window and returns without waiting for the newly opened window to close. </summary>
        void Show();

        /// <summary> Manually closes the window. </summary>
        void Close();
    }
}