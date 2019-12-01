using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Client.Wpf.Controls.Base
{
    /// <summary> A user control consisting of a row or a column of <see cref="DropCapToggleButton"/>s, indexed by <typeparamref name="T"/> values, and having content read from resources. </summary>
    /// <typeparam name="T"> The key type. </typeparam>
    public class ToggleButtonGroupControlWithResource<T> : ToggleButtonGroupControlWithToolTip<T>
    {
        #region Methods: CreateToggleButtons()

        /// <summary> Creates toggle buttons for given <paramref name="enumerationItems"/>, with icons read from resources by their <paramref name="resourceKeys"/>. </summary>
        /// <param name="panel"> The panel to add buttons into. </param>
        /// <param name="enumerationItems"> Enumeration items to create toggle buttons for. </param>
        /// <param name="resourceKeys"> Resource keys for <paramref name="enumerationItems"/>. </param>
        /// <param name="styleKey"> The key of the style (defined in <see cref="WpfClient"/> and referenced by <see cref="EStyleKey"/>) to apply. </param>
        /// <param name="horizontal"> Whether to arrange buttons in a row or in a column. </param>
        /// <param name="createToggleAllButton"> Whether to create the toggle-all button. </param>
        public void CreateToggleButtons(Panel panel, IEnumerable<T> enumerationItems, IDictionary<T, string> resourceKeys, string styleKey, bool horizontal = true, bool createToggleAllButton = false)
        {
            if (createToggleAllButton && enumerationItems.Any())
                CreateToggleAllButton(panel, enumerationItems.First(), styleKey, horizontal);

            foreach (var enumerationItem in enumerationItems)
            {
                var toggleButton = new ToggleButton
                {
                    Style = this.GetStyle(styleKey),
                    Tag = enumerationItem,
                    Content = new Image()
                    {
                        Source = Application.Current.MainWindow.FindResource(resourceKeys[enumerationItem]) as ImageSource,
                        Style = this.GetStyle(EStyleKey.Image.FlagIcon),
                    },
                };

                toggleButton.Click += OnClick;
                toggleButton.AddToPanel(panel, horizontal);

                Buttons.Add(enumerationItem, toggleButton);
            }
        }

        #endregion Methods: CreateToggleButtons()
    }
}