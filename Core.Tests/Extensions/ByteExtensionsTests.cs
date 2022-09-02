using Core.Enumerations;
using Core.Extensions;
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
            var stepCount = Integer.Number.Zero;

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
            var stepCount = Integer.Number.Hundred;

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
            var start = Convert.ToByte(Integer.Number.Zero);
            var end = Convert.ToByte(Integer.Number.Five);
            var stepCount = Integer.Number.Four;
            var expectedSteps = new List<byte> { Integer.Number.One, Integer.Number.Two, Integer.Number.Three, Integer.Number.Four };

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
            var start = Convert.ToByte(Integer.Number.Five);
            var end = Convert.ToByte(Integer.Number.Zero);
            var stepCount = Integer.Number.Four;
            var expectedSteps = new List<byte> { Integer.Number.Four, Integer.Number.Three, Integer.Number.Two, Integer.Number.One };

            // act
            var steps = start.InterpolateTo(end, stepCount);

            // assert
            steps.Count().Should().Be(stepCount);
            steps.Should().BeEquivalentTo(expectedSteps);
        }

        #endregion Tests: InterpolateTo()
    }
}
