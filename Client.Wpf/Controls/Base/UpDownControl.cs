using Client.Wpf.Enumerations;
using Core.Extensions;
using System;
using System.Windows.Controls;

namespace Client.Wpf.Controls.Base
{
    /// <summary> A control for incrementing/decrementing values. </summary>
    /// <typeparam name="T"> The type of values.</typeparam>
    public class UpDownControl<T> : UserControl where T : struct, IComparable
    {
        #region Fields

        /// <summary> The minimum value available. </summary>
        private T _minimumValue;

        /// <summary> The maximum value available. </summary>
        private T _maximumValue;

        /// <summary> The current value. </summary>
        protected T _currentValue;

        #endregion Fields

        /// <summary> The minimum value available. </summary>
        public T MinimumValue
        {
            get => _minimumValue;
            set
            {
                _minimumValue = value;

                if (Value.CompareTo(_minimumValue).IsNegative())
                    Value = _minimumValue;
            }
        }

        /// <summary> The maximum value available. </summary>
        public T MaximumValue
        {
            get => _maximumValue;
            set
            {
                _maximumValue = value;

                if (Value.CompareTo(_maximumValue).IsPositive())
                    Value = _maximumValue;
            }
        }

        /// <summary> The current value. </summary>
        public virtual T Value
        {
            get => _currentValue;
            set => _currentValue = value;
        }

        /// <summary> Adjusts the <see cref="Value"/>. </summary>
        /// <param name="direction"> The direction in which to adjust. </param>
        protected void AdjustValue(EDirection direction)
        {
            if (direction == EDirection.Up && Value.CompareTo(MaximumValue).IsNegative())
                Value = Value.Increment();

            else if (direction == EDirection.Down && Value.CompareTo(MinimumValue).IsPositive())
                Value = Value.Decrement();
        }
    }
}