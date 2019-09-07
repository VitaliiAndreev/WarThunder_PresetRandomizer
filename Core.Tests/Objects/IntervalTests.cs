using Core.Objects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Tests.Objects
{
    /// <summary> See <see cref="Interval{T}"/>. </summary>
    [TestClass]
    public class IntervalTests
    {
        private readonly decimal _infinitelySmallPositiveDecimalFraction = 0.0000000000000000000000000001m;

        #region Contains()

        [TestMethod]
        public void Contains_false_0_1_false()
        {
            // arrange
            var leftEndpoint = 0m;
            var rightEndpoint = 1m;
            var interval = new Interval<decimal>(false, leftEndpoint, rightEndpoint, false);

            // act
            var containsBeforeLeftEndpoint = interval.Contains(leftEndpoint - _infinitelySmallPositiveDecimalFraction);
            var containsLeftEndpoint = interval.Contains(leftEndpoint);
            var containsBeforeRightEndpoint = interval.Contains(rightEndpoint - _infinitelySmallPositiveDecimalFraction);
            var containsRightEndpoint = interval.Contains(rightEndpoint);
            var containsAfterRightEndpoint = interval.Contains(rightEndpoint + _infinitelySmallPositiveDecimalFraction);

            // assert
            containsBeforeLeftEndpoint.Should().BeFalse();
            containsLeftEndpoint.Should().BeFalse();
            containsBeforeRightEndpoint.Should().BeTrue();
            containsRightEndpoint.Should().BeFalse();
            containsAfterRightEndpoint.Should().BeFalse();
        }

        [TestMethod]
        public void Contains_true_0_1_false()
        {
            // arrange
            var leftEndpoint = 0m;
            var rightEndpoint = 1m;
            var interval = new Interval<decimal>(true, leftEndpoint, rightEndpoint, false);

            // act
            var containsBeforeLeftEndpoint = interval.Contains(leftEndpoint - _infinitelySmallPositiveDecimalFraction);
            var containsLeftEndpoint = interval.Contains(leftEndpoint);
            var containsBeforeRightEndpoint = interval.Contains(rightEndpoint - _infinitelySmallPositiveDecimalFraction);
            var containsRightEndpoint = interval.Contains(rightEndpoint);
            var containsAfterRightEndpoint = interval.Contains(rightEndpoint + _infinitelySmallPositiveDecimalFraction);

            // assert
            containsBeforeLeftEndpoint.Should().BeFalse();
            containsLeftEndpoint.Should().BeTrue();
            containsBeforeRightEndpoint.Should().BeTrue();
            containsRightEndpoint.Should().BeFalse();
            containsAfterRightEndpoint.Should().BeFalse();
        }

        [TestMethod]
        public void Contains_false_0_1_true()
        {
            // arrange
            var leftEndpoint = 0m;
            var rightEndpoint = 1m;
            var interval = new Interval<decimal>(false, leftEndpoint, rightEndpoint, true);

            // act
            var containsBeforeLeftEndpoint = interval.Contains(leftEndpoint - _infinitelySmallPositiveDecimalFraction);
            var containsLeftEndpoint = interval.Contains(leftEndpoint);
            var containsBeforeRightEndpoint = interval.Contains(rightEndpoint - _infinitelySmallPositiveDecimalFraction);
            var containsRightEndpoint = interval.Contains(rightEndpoint);
            var containsAfterRightEndpoint = interval.Contains(rightEndpoint + _infinitelySmallPositiveDecimalFraction);

            // assert
            containsBeforeLeftEndpoint.Should().BeFalse();
            containsLeftEndpoint.Should().BeFalse();
            containsBeforeRightEndpoint.Should().BeTrue();
            containsRightEndpoint.Should().BeTrue();
            containsAfterRightEndpoint.Should().BeFalse();
        }

        [TestMethod]
        public void Contains_true_0_1_true()
        {
            // arrange
            var leftEndpoint = 0m;
            var rightEndpoint = 1m;
            var interval = new Interval<decimal>(true, leftEndpoint, rightEndpoint, true);

            // act
            var containsBeforeLeftEndpoint = interval.Contains(leftEndpoint - _infinitelySmallPositiveDecimalFraction);
            var containsLeftEndpoint = interval.Contains(leftEndpoint);
            var containsBeforeRightEndpoint = interval.Contains(rightEndpoint - _infinitelySmallPositiveDecimalFraction);
            var containsRightEndpoint = interval.Contains(rightEndpoint);
            var containsAfterRightEndpoint = interval.Contains(rightEndpoint + _infinitelySmallPositiveDecimalFraction);

            // assert
            containsBeforeLeftEndpoint.Should().BeFalse();
            containsLeftEndpoint.Should().BeTrue();
            containsBeforeRightEndpoint.Should().BeTrue();
            containsRightEndpoint.Should().BeTrue();
            containsAfterRightEndpoint.Should().BeFalse();
        }

        #endregion Contains()
    }
}