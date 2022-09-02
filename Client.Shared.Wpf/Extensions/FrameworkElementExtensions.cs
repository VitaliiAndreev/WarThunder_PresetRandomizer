using Core;
using Core.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Client.Shared.Wpf.Extensions
{
    /// <summary> Methods extending the <see cref="FrameworkElement"/> class. </summary>
    public static class FrameworkElementExtensions
    {
        #region Methods: Getting

        public static BitmapSource GetBitmapSource(this FrameworkElement frameworkElement, string resourceKey) =>
            frameworkElement.FindResource(resourceKey) as BitmapSource;

        public static T GetChild<T>(FrameworkElement parent) where T : Visual
        {
            if (parent is null) return null;

            var target = default(T);

            for (var childIndex = Integer.Number.Zero; childIndex < VisualTreeHelper.GetChildrenCount(parent); childIndex++)
            {
                var child = VisualTreeHelper.GetChild(parent, childIndex) as FrameworkElement;

                target = child as T;

                if (target is null)
                    target = GetChild<T>(child);

                if (target is T)
                    return target;
            }
            return target;
        }

        /// <summary> Searches for a style with the specified key defined in <see cref="WpfClient"/>, and throws an exception if the requested style is not found. </summary>
        /// <param name="frameworkElement"> The framework element for which to search. </param>
        /// <param name="styleKey"> The string key of the style. </param>
        /// <returns></returns>
        public static Style GetStyle(this FrameworkElement frameworkElement, string styleKey) =>
            styleKey is null ? null : frameworkElement.FindResource(styleKey) as Style;

        /// <summary> Gets the tag of the given <paramref name="frameworkElement"/> and casts it to <typeparamref name="T"/>. </summary>
        /// <typeparam name="T"> The type to cast the tag to. </typeparam>
        /// <param name="frameworkElement"> The framework element for which to search. </param>
        /// <returns></returns>
        public static T GetTag<T>(this FrameworkElement frameworkElement) =>
            frameworkElement.Tag.CastTo<T>();

        #endregion Methods: Getting

        public static void PutInto(this FrameworkElement element, Border borderControl) => borderControl.Child = element;

        #region Methods: Working with Size

        /// <summary> Sets the size of the <paramref name="frameworkElement"/> along both axes. </summary>
        /// <param name="frameworkElement"> The framework element whose size to set. </param>
        /// <param name="size"> The new size of <paramref name="frameworkElement"/> along both axes. </param>
        public static void SetSize(this FrameworkElement frameworkElement, double size) =>
            frameworkElement.SetSize(size, size);

        /// <summary> Sets the size of the <paramref name="frameworkElement"/>. </summary>
        /// <param name="frameworkElement"> The framework element whose size to set. </param>
        /// <param name="height"> The new height of <paramref name="frameworkElement"/>. </param>
        /// <param name="width"> The new width of <paramref name="frameworkElement"/>. </param>
        public static void SetSize(this FrameworkElement frameworkElement, double height, double width)
        {
            frameworkElement.Height = height;
            frameworkElement.Width = width;
        }

        #endregion Methods: Working with Size
    }
}