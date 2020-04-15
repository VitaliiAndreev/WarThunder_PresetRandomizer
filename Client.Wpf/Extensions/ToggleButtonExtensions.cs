using Client.Wpf.Enumerations;
using Core.Enumerations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Extensions
{
    /// <summary> Methods extending the <see cref="ToggleButton"/> class. </summary>
    public static class ToggleButtonExtensions
    {
        public static void SetContextMenuState(this ToggleButton toggleButton, bool activate)
        {
            if (!(toggleButton.ContextMenu is ContextMenu contextMenu))
                return;

            if (activate)
                contextMenu.Activate();
            else
                contextMenu.Deactivate();
        }

        /// <summary> Checks whether the <paramref name="toggleButton"/> is checked. </summary>
        /// <param name="toggleButton"> The toggle button to check. </param>
        /// <returns></returns>
        public static bool IsChecked(this ToggleButton toggleButton) =>
            toggleButton.IsChecked.HasValue && toggleButton.IsChecked.Value;

        public static Grid ReplaceContentWithGrid(this ToggleButton button, double leftColumnWidth, double rightColumnWidth)
        {
            button.HorizontalContentAlignment = HorizontalAlignment.Right;

            var newGrid = new Grid { Style = button.GetStyle(EStyleKey.Grid.SkyQuakeGrid) };

            newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(leftColumnWidth, GridUnitType.Pixel) });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(rightColumnWidth, GridUnitType.Pixel) });

            var iconTextBlock = new TextBlock { Style = button.GetStyle(EStyleKey.TextBlock.TextBlockWithSkyQuake), Text = button.Content.ToString() };

            newGrid.Add(iconTextBlock, EInteger.Number.Zero);

            button.Content = newGrid;

            return newGrid;
        }

        public static void UpdateContextMenuItemCount(this ToggleButton button, double leftColumnWidth, double rightColumnWidth, double fontSize)
        {
            if (button.ContextMenu is ContextMenu contextMenu)
            {
                var toggleMenuItems = contextMenu.Items.OfType<MenuItem>().Where(i => i.IsCheckable);
                var newTextBlock = new TextBlock
                {
                    Style = button.GetStyle(EStyleKey.TextBlock.TextBlockWithSkyQuakeVerticallyCentered),
                    FontSize = fontSize,
                    Text = $"{toggleMenuItems.Where(menuItem => menuItem.IsChecked).Count()}/{toggleMenuItems.Count()}",
                };

                if (button.Content is Grid grid)
                {
                    var lastTextBlock = grid.Children.OfType<TextBlock>().Last();

                    grid.Children.Remove(lastTextBlock);
                    grid.Add(newTextBlock, EInteger.Number.One);
                }
                else
                {
                    button
                        .ReplaceContentWithGrid(leftColumnWidth, rightColumnWidth)
                        .Add(newTextBlock, EInteger.Number.One)
                    ;
                }
            }
        }
    }
}