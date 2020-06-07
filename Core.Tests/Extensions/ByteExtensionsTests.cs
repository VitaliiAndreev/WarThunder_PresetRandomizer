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
            var stepCount = EInteger.Number.Zero;

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
            var stepCount = EInteger.Number.Hundred;

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
            var start = Convert.ToByte(EInteger.Number.Zero);
            var end = Convert.ToByte(EInteger.Number.Five);
            var stepCount = EInteger.Number.Four;
            var expectedSteps = new List<byte> { EInteger.Number.One, EInteger.Number.Two, EInteger.Number.Three, EInteger.Number.Four };

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
            var start = Convert.ToByte(EInteger.Number.Five);
            var end = Convert.ToByte(EInteger.Number.Zero);
            var stepCount = EInteger.Number.Four;
            var expectedSteps = new List<byte> { EInteger.Number.Four, EInteger.Number.Three, EInteger.Number.Two, EInteger.Number.One };

            // act
            var steps = start.InterpolateTo(end, stepCount);

            // assert
            steps.Count().Should().Be(stepCount);
            steps.Should().BeEquivalentTo(expectedSteps);
        }

        #endregion Tests: InterpolateTo()
    }
}
