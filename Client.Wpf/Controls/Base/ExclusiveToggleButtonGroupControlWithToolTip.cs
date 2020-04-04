using Client.Wpf.Extensions;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls.Base
{
    /// <summary>
    /// A user control consisting of a row or a column of <see cref="ToggleButton"/>s, indexed by <typeparamref name="T"/> values, and having a tooltip.
    /// Only one <see cref="ToggleButton"/> can be toggled at a time.
    /// </summary>
    /// <typeparam name="T"> The key type. </typeparam>
    public class ExclusiveToggleButtonGroupControlWithToolTip<T> : ToggleButtonGroupControlWithToolTip<T>
    {
        #region Methods: Event Handlers

        /// <summary> Toggles off buttons other than the one pressed. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ToggleButton"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        public override void OnClick(object sender, RoutedEventArgs eventArguments)
        {
            if (!(sender is ToggleButton clickedButton))
            {
                return;
            }

            var buttons = Buttons.Values;
            var allButtonsAreToggledOff = buttons.All(button => !button.IsChecked());

            if (allButtonsAreToggledOff)
            {
                clickedButton.IsChecked = true;
                return;
            }

            buttons
                .Except(new ToggleButton[] { clickedButton })
                .ToList()
                .ForEach(button => button.IsChecked = false)
            ;

            RaiseClickEvent(clickedButton);
        }

        #endregion Methods: Event Handlers
    }
}