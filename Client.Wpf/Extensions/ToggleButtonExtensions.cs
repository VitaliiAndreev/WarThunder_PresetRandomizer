using System.Windows.Controls.Primitives;

namespace Client.Wpf.Extensions
{
    /// <summary> Methods extending the <see cref="ToggleButton"/> class. </summary>
    public static class ToggleButtonExtensions
    {
        /// <summary> Checks whether the <paramref name="toggleButton"/> is checked. </summary>
        /// <param name="toggleButton"> The toggle button to check. </param>
        /// <returns></returns>
        public static bool IsChecked(this ToggleButton toggleButton) =>
            toggleButton.IsChecked.HasValue && toggleButton.IsChecked.Value;
    }
}