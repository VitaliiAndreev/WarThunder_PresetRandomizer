using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls.Base
{
    /// <summary> A user control consisting of a column of <see cref="ToggleButton"/>s, indexed by <typeparamref name="T"/> values. </summary>
    /// <typeparam name="T"> The key type. </typeparam>
    /// <typeparam name="U"> The owner type. </typeparam>
    public abstract class ColumnToggleControlWithTooltips<T, U>: ToggleButtonGroupControlWithToolTip<U>
    {
        #region Fields

        /// <summary> Items grouped by their owner branches. </summary>
        protected readonly IDictionary<T, IEnumerable<U>> _groupedItems;

        #endregion Fields
        #region Properties

        /// <summary> The branch to which vehicle classes belong to. </summary>
        public abstract T Owner { get; set; }

        /// <summary> Enabled vehicle classes. </summary>
        public IEnumerable<U> EnabledItems { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        /// <param name="owner"> The owner for which to create the control. </param>
        public ColumnToggleControlWithTooltips(T owner)
            : this()
        {
            Owner = owner;
        }

        /// <summary> Creates a new control. </summary>
        public ColumnToggleControlWithTooltips()
        {
            _groupedItems = new Dictionary<T, IEnumerable<U>>();

            EnabledItems = new List<U>();
        }

        #endregion Constructors

        /// <summary> Creates toggle buttons for given <paramref name="enumerationItems"/>, with character <paramref name="icons"/>. </summary>
        /// <param name="panel"> The panel to add buttons into. </param>
        /// <param name="enumerationItems"> Enumeration items to create toggle buttons for. </param>
        /// <param name="icons"> Icons for <paramref name="enumerationItems"/>. </param>
        /// <param name="styleKey"> The key of the style (defined in <see cref="WpfClient"/> and referenced by <see cref="EStyleKey"/>) to apply. </param>
        public void CreateToggleButtons(Panel panel, IEnumerable<U> enumerationItems, IDictionary<U, char> icons, string styleKey) =>
            CreateToggleButtons(panel, enumerationItems, icons, styleKey, false);
    }
}