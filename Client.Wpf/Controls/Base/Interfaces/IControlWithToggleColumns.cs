using System.Collections.Generic;

namespace Client.Wpf.Controls.Base.Interfaces
{
    /// <summary> A user control consisting of <see cref="ColumnToggleControl{T, U}"/>s indexed by <typeparamref name="T"/> values. </summary>
    /// <typeparam name="T"> The owner type. </typeparam>
    /// <typeparam name="U"> The key type. </typeparam>
    public interface IControlWithToggleColumns<T, U> : IControlWithSubcontrols<T>
    {
        #region Properties

        /// <summary> Toggle button columns indexed by the <typeparamref name="T"/> key. </summary>
        public IDictionary<T, ToggleButtonGroupControl<U>> ToggleColumns { get; }

        #endregion Properties
    }
}