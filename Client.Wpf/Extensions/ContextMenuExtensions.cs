using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Client.Wpf.Extensions
{
    /// <summary> Methods extending the <see cref="ContextMenu"/> class. </summary>
    public static class ContextMenuExtensions
    {
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