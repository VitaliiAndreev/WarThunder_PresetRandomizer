using System.Collections.Generic;

namespace Client.Wpf.Controls.Base
{
    /// <summary> A user control consisting of a row or a column of <see cref="DropCapToggleButton"/>s, indexed by <typeparamref name="T"/> values, and having a tooltip. </summary>
    /// <typeparam name="T"> The key type. </typeparam>
    public class ToggleButtonGroupControlWithDropCap<T> : ToggleButtonGroupControl<T>
    {
        #region Properties

        /// <summary> The map of the game mode enumeration onto corresponding drop cap toggle buttons. </summary>
        protected readonly IDictionary<T, DropCapToggleButton> _dropCapToggleButtons;

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public ToggleButtonGroupControlWithDropCap()
        {
            _dropCapToggleButtons = new Dictionary<T, DropCapToggleButton>();
        }

        #endregion Constructors

        /// <summary> Applies localization to visible text on the control. </summary>
        public override void Localise()
        {
            base.Localise();

            foreach (var dropCapToggleButton in _dropCapToggleButtons.Values)
                if (dropCapToggleButton.Tag is T key)
                    dropCapToggleButton.Caption = ApplicationHelpers.LocalizationManager.GetLocalizedString(key.ToString());
        }
    }
}