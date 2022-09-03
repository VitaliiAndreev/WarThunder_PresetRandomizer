using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Tests.Extensions
{
    [TestClass]
    public class ByteExtensionsTests
    {
        #region Tests: InterpolateTo()

        [TestMethod]
        public void InterpolateTo_NegativeSteps_ShouldBeEmpty()
        {
            // arrange
            var start = byte.MinValue;
            var end = byte.MaxValue;
            var stepCount = int.MinValue;

            // act
            var steps = start.InterpolateTo(end, stepCount);

            // assert
            steps.Should().BeEmpty();
        }

        [TestMethod]
        public void InterpolateTo_ZeroSteps_ShouldBeEmpty()
        {
            // arrange
            var start = byte.MinValue;
            var end = byte.MaxValue;
            var stepCount = 0;

            // act
            var steps = start.InterpolateTo(end, stepCount);

            // assert
            steps.Should().BeEmpty();
        }

        [TestMethod]
        public void InterpolateTo_SameValues_ShouldBeSameValues()
        {
            // arrange
            var start = byte.MinValue;
            var end = start;
            var stepCount = 100;

            // act
            var steps = start.InterpolateTo(end, stepCount);

            // assert
            steps.Count().Should().Be(stepCount);
            steps.All(value => value == start).Should().BeTrue();
        }

        [TestMethod]
        public void InterpolateTo_0To5_4Steps_ShouldBe1234()
        {
            // arrange
            var start = Convert.ToByte(0);
            var end = Convert.ToByte(5);
            var stepCount = 4;
            var expectedSteps = new List<byte> { 1, 2, 3, 4 };

            // act
            var steps = start.InterpolateTo(end, stepCount);

            // assert
            steps.Count().Should().Be(stepCount);
            steps.Should().BeEquivalentTo(expectedSteps);
        }

        [TestMethod]
        public void InterpolateTo_5To0_4Steps_ShouldBe4321()
        {
            // arrange
            var start = Convert.ToByte(5);
            var end = Convert.ToByte(0);
            var stepCount = 4;
            var expectedSteps = new List<byte> { 4, 3, 2, 1 };

            // act
            var steps = start.InterpolateTo(end, stepCount);

            // assert
            steps.Count().Should().Be(stepCount);
            steps.Should().BeEquivalentTo(expectedSteps);
        }

        #endregion Tests: InterpolateTo()
    }
}
