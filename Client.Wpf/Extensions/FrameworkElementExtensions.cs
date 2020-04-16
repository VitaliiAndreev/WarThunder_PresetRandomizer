using Core.Extensions;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Client.Wpf.Extensions
{
    /// <summary> Methods extending the <see cref="FrameworkElement"/> class. </summary>
    public static class FrameworkElementExtensions
    {
        public static BitmapSource GetBitmapSource(this FrameworkElement frameworkElement, string resourceKey) =>
            frameworkElement.FindResource(resourceKey) as BitmapSource;

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
    }
}