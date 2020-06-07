using System.Windows;
using System.Windows.Media;

namespace Client.Shared.Wpf.Extensions
{
    /// <summary> Methods extending the <see cref="DependencyObject"/> class. </summary>
    public static class DependencyObjectExtensions
    {
        #region Fluency

        /// <summary> Calls <see cref="DependencyObject.GetValue(DependencyProperty)"/> of the specified property and casts the result into the given type. </summary>
        /// <typeparam name="T"> The type to cast into. </typeparam>
        /// <param name="dependencyObject"> The source object. </param>
        /// <param name="dependencyProperty"> The property to get the value of. </param>
        /// <returns></returns>
        public static T GetValue<T>(this DependencyObject dependencyObject, DependencyProperty dependencyProperty) =>
            (T)dependencyObject.GetValue(dependencyProperty);

        /// <summary> Gets the parent window of the object. </summary>
        /// <param name="dependencyObject"> The object whose parent window to get. </param>
        /// <returns></returns>
        public static Window GetWindow(this DependencyObject dependencyObject) =>
            Window.GetWindow(dependencyObject);

        /// <summary> Gets the parent item of the specified type. </summary>
        /// <typeparam name="T"> The type of the parent item to look for. </typeparam>
        /// <param name="dependencyObject"> The object whose parent to search.</param>
        /// <returns></returns>
        public static T GetParent<T>(this DependencyObject dependencyObject) where T : class
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject) ?? LogicalTreeHelper.GetParent(dependencyObject);

            if (parent is null)
                return null;

            if (parent is T validParent)
                return validParent;
            
            return parent.GetParent<T>();
        }

        #endregion Fluency
    }
}