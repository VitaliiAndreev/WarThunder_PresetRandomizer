using Core;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client.Shared.Wpf.Extensions
{
    /// <summary> Methods extending the <see cref="Grid"/> class. </summary>
    public static class GridExtensions
    {
        public static void Add(this Grid grid, UIElement element, int? columnIndex = null, int? rowIndex = null)
        {
            grid.Children.Add(element);

            if (columnIndex.HasValue)
            {
                for (var newColumnIndex = grid.ColumnDefinitions.Count() - Integer.Number.One; newColumnIndex < columnIndex; newColumnIndex++)
                    grid.ColumnDefinitions.Add(new ColumnDefinition());

                Grid.SetColumn(element, columnIndex.Value);
            }

            if (rowIndex.HasValue)
            {
                for (var newRowIndex = grid.RowDefinitions.Count() - Integer.Number.One; newRowIndex < rowIndex; newRowIndex++)
                    grid.RowDefinitions.Add(new RowDefinition());

                Grid.SetRow(element, rowIndex.Value);
            }
        }

        /// <summary> Removes the given <paramref name="element"/> from the <paramref name="grid"/> along with its row/column (if not other elements are present). </summary>
        /// <param name="grid"> The grid to remove the <paramref name="element"/> from. </param>
        /// <param name="element"> The element to remove from the <paramref name="grid"/>. </param>
        /// <param name="column"> Whether to remove </param>
        public static void Remove(this Grid grid, UIElement element, bool column = true)
        {
            grid.Children.Remove(element);

            var children = grid.Children.OfType<UIElement>();

            if (column)
            {
                var columnIndex = Grid.GetColumn(element);

                if (!children.Any(item => Grid.GetColumn(item) == columnIndex))
                    grid.ColumnDefinitions.RemoveAt(columnIndex);
            }
            else
            {
                var rowIndex = Grid.GetRow(element);

                if (!children.Any(item => Grid.GetRow(item) == rowIndex))
                    grid.RowDefinitions.RemoveAt(rowIndex);
            }
        }
    }
}