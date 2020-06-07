using Client.Shared.Wpf.Enumerations.Logger;
using Core.Enumerations;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client.Shared.Wpf.Extensions
{
    /// <summary> Methods extending the <see cref="UIElement"/> class. </summary>
    public static class UiElementExtensions
    {
        /// <summary> Adds the given <paramref name="uiElement"/> to the <paramref name="panel"/>, stacking it horizontally or vertically. </summary>
        /// <param name="panel"> The panel to add <paramref name="uiElement"/> onto. </param>
        /// <param name="horizontal"> Whether to stack the <paramref name="uiElement"/> into a row or into a column. </param>
        /// <param name="uiElement"> The UI element to add onto the <paramref name="panel"/></param>
        public static void AddToPanel(this UIElement uiElement, Panel panel, bool horizontal)
        {
            if (panel is Grid grid)
                uiElement.AddToGrid(grid, horizontal);

            else if (panel is StackPanel && horizontal)
                throw new ArgumentException(EWpfCoreLogMessage.StackPanelCantBeHorizontal);

            else if (panel is WrapPanel && !horizontal)
                throw new ArgumentException(EWpfCoreLogMessage.WrapPanelCantBeVertical);

            else
                panel.Children.Add(uiElement);
        }

        /// <summary> Adds the given <paramref name="uiElement"/> to the <paramref name="grid"/>, stacking it horizontally or vertically. </summary>
        /// <param name="grid"> The grid to add <paramref name="uiElement"/> onto. </param>
        /// <param name="horizontal"> Whether to stack the <paramref name="uiElement"/> into a row or into a column. </param>
        /// <param name="uiElement"> The UI element to add onto the <paramref name="grid"/></param>
        public static void AddToGrid(this UIElement uiElement, Grid grid, bool horizontal)
        {
            if (horizontal)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.Children.Add(uiElement);

                Grid.SetColumn(uiElement, grid.ColumnDefinitions.Count() - EInteger.Number.One);
            }
            else
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.Children.Add(uiElement);

                Grid.SetRow(uiElement, grid.RowDefinitions.Count() - EInteger.Number.One);
            }
        }
    }
}