using Core.Extensions;
using System.Windows;

namespace Client.Wpf.Extensions
{
    /// <summary> Methods extending the <see cref="FrameworkElement"/> class. </summary>
    public static class FrameworkElementExtensions
    {
        /// <summary> Searches for a style with the specified key defined in <see cref="WpfClient"/>, and throws an exception if the requested style is not found. </summary>
        /// <param name="frameworkElement"> The framework element for which to search. </param>
        /// <param name="styleKey"> The string key of the style. </param>
        /// <returns></returns>
        public static Style GetStyle(this FrameworkElement frameworkElement, string styleKey) =>
            frameworkElement.FindResource(styleKey) as Style;

        /// <summary> Gets the tag of the given <paramref name="frameworkElement"/> and casts it to <typeparamref name="T"/>. </summary>
        /// <typeparam name="T"> The type to cast the tag to. </typeparam>
        /// <param name="frameworkElement"> The framework element for which to search. </param>
        /// <returns></returns>
        public static T GetTag<T>(this FrameworkElement frameworkElement) =>
            frameworkElement.Tag.CastTo<T>();
    }
}