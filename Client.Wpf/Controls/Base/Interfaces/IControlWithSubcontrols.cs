using System.Windows;

namespace Client.Wpf.Controls.Base.Interfaces
{
    /// <summary> A control with subcontrols. </summary>
    /// <typeparam name="T"> The type of keys by which to group subcontrols. </typeparam>
    public interface IControlWithSubcontrols<T>
    {
        /// <summary> Changes the <see cref="UIElement.IsEnabled"/> status of the control corresponding to the specified key. </summary>
        /// <param name="key"> The key by which to access the control. </param>
        /// <param name="enable"> Whether to enable or disable the control. </param>
        void Enable(T key, bool enable);
    }
}