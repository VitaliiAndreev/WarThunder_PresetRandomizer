namespace Core.Objects.Abstract
{
    /// <summary> The abstract base class for type-specific intervals. </summary>
    public abstract class IntervalBase
    {
        /// <summary> Whether the left endpoint is included in the interval. </summary>
        public bool LeftBounded { get; }
        /// <summary> Whether the right endpoint is included in the interval. </summary>
        public bool RightBounded { get; }

        /// <summary> Takes parameters from inheriting interval classes. </summary>
        /// <param name="leftBounded"> Whether the left endpoint is included in the interval. </param>
        /// <param name="rightBounded"> Whether the right endpoint is included in the interval. </param>
        public IntervalBase(bool leftBounded, bool rightBounded)
        {
            LeftBounded = leftBounded;
            RightBounded = rightBounded;
        }
    }
}