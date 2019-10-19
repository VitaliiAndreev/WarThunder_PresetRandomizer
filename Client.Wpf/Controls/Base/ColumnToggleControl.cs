using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls.Base
{
    /// <summary> A user control consisting of a column of <see cref="ToggleButton"/>s, indexed by <typeparamref name="T"/> values. </summary>
    /// <typeparam name="T"> The key type. </typeparam>
    /// <typeparam name="U"> The owner type. </typeparam>
    public abstract class ColumnToggleControl<T, U>: ToggleButtonGroupControl<T>
    {
        #region Fields

        /// <summary> Items grouped by their owner branches. </summary>
        protected readonly IDictionary<U, IEnumerable<T>> _groupedItems;

        #endregion Fields
        #region Properties

        /// <summary> The branch to which vehicle classes belong to. </summary>
        public abstract U Owner { get; set; }

        /// <summary> Enabled vehicle classes. </summary>
        public IEnumerable<T> EnabledItems { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        /// <param name="owner"> The owner for which to create the control. </param>
        public ColumnToggleControl(U owner)
            : this()
        {
            Owner = owner;
        }

        /// <summary> Creates a new control. </summary>
        public ColumnToggleControl()
        {
            _groupedItems = new Dictionary<U, IEnumerable<T>>();

            EnabledItems = new List<T>();
        }

        #endregion Constructors

        /// <summary> Creates toggle buttons for given <paramref name="enumerationItems"/>, with character <paramref name="icons"/>. </summary>
        /// <param name="panel"> The panel to add buttons into. </param>
        /// <param name="enumerationItems"> Enumeration items to create toggle buttons for. </param>
        /// <param name="icons"> Icons for <paramref name="enumerationItems"/>. </param>
        /// <param name="styleKey"> The key of the style (defined in <see cref="WpfClient"/> and referenced by <see cref="EStyleKey"/>) to apply. </param>
        public void CreateToggleButtons(Panel panel, IEnumerable<T> enumerationItems, IDictionary<T, char> icons, string styleKey) =>
            CreateToggleButtons(panel, enumerationItems, icons, styleKey, false);
    }
}