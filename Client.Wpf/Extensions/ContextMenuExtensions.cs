using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Extensions
{
    /// <summary> Methods extending the <see cref="ContextMenu"/> class. </summary>
    public static class ContextMenuExtensions
    {
        /// <summary> Creates a togglable context menu item for the given <paramref name="tag"/> and puts it into the <paramref name="contextMenu"/>. </summary>
        /// <param name="contextMenu"> The context menu. </param>
        /// <param name="tag"> The tag for whom to create the context menu item. </param>
        /// <param name="clickHandler"> The handler of clicking on the menu item. </param>
        /// <returns></returns>
        public static MenuItem CreateTogglableMenuItem<T>(this ContextMenu contextMenu, T tag, RoutedEventHandler clickHandler) where T : struct
        {
            var menuItem = new MenuItem()
            {
                IsCheckable = true,
                Tag = tag,
                Header = tag.ToString(),
                StaysOpenOnClick = true,
            };
            menuItem.Click += clickHandler;

            contextMenu.Items.Add(menuItem);

            return menuItem;
        }

        /// <summary> Gets all <see cref="MenuItem"/>s from the <paramref name="contextMenu"/>. </summary>
        /// <param name="contextMenu"> The context menu to get menu items from. </param>
        /// <returns></returns>
        public static IEnumerable<MenuItem> GetMenuItems(this ContextMenu contextMenu) =>
            contextMenu.Items.OfType<MenuItem>();

        /// <summary> Checks whether the <paramref name="contextMenu"/> has any <see cref="MenuItem"/>s that are checked. </summary>
        /// <param name="contextMenu"> The context menu to check. </param>
        /// <returns></returns>
        public static bool HasCheckedItems(this ContextMenu contextMenu) =>
            contextMenu.GetMenuItems().Any(menuItem => menuItem.IsChecked);
    }
}