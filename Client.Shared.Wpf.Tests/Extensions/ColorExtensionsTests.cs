using Client.Shared.Wpf.Extensions;
using Core.Enumerations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Client.Shared.Wpf.Tests.Extensions
{
    [TestClass]
    public class ColorExtensionsTests
    {
        #region Tests: InterpolateTo()

        [TestMethod]
        public void InterpolateTo_NegativeSteps_ShouldBeEmpty()
        {
            // arrange
            var black = new Color().From(0);
            var white = new Color().From(255);
            var stepCount = int.MinValue;

            // act
            var colors = black.InterpolateTo(white, stepCount);

            // assert
            colors.Should().BeEmpty();
        }

        [TestMethod]
        public void InterpolateTo_ZeroSteps_ShouldBeEmpty()
        {
            // arrange
            var black = new Color().From(0);
            var white = new Color().From(255);
            var stepCount = int.MinValue;

            // act
            var colors = black.InterpolateTo(white, stepCount);

            // assert
            colors.Should().BeEmpty();
        }

        [TestMethod]
        public void InterpolateTo_SameColors()
        {
            // arrange
            var blackOriginal = new Color().From(0);
            var blackClone = new Color().From(0);
            var stepCount = Integer.Number.Ten;

            // act
            var colors = blackOriginal.InterpolateTo(blackClone, stepCount);

            // assert
            colors.Count().Should().Be(stepCount);
            colors.All(color => color == blackOriginal).Should().BeTrue();
        }

        [TestMethod]
        public void InterpolateTo_BlackToWhite_2Steps()
        {
            // arrange
            var black = new Color().From(0);
            var white = new Color().From(255);
            var stepCount = Integer.Number.Two;
            var expectedFirstStep = Convert.ToByte(85);
            var expectedSecondStep = Convert.ToByte(170);
            var expectedColors = new List<Color> { new Color().From(expectedFirstStep), new Color().From(expectedSecondStep) };

            // act
            var colors = black.InterpolateTo(white, stepCount);

            // assert
            colors.Should().BeEquivalentTo(expectedColors);
        }

        [TestMethod]
        public void InterpolateTo_WhiteToBlack_2Steps()
        {
            // arrange
            var black = new Color().From(0);
            var white = new Color().From(255);
            var stepCount = Integer.Number.Two;
            var expectedFirstStep = Convert.ToByte(170);
            var expectedSecondStep = Convert.ToByte(85);
            var expectedColors = new List<Color> { new Color().From(expectedFirstStep), new Color().From(expectedSecondStep) };

            // act
            var colors = white.InterpolateTo(black, stepCount);

            // assert
            colors.Should().BeEquivalentTo(expectedColors);
        }

        [TestMethod]
        public void InterpolateTo_RedToGreen_2Steps()
        {
            // arrange
            var red = new Color().From(255, 0, 0);
            var green = new Color().From(0, 255, 0);
            var stepCount = Integer.Number.Two;
            var expectedFirstStep = Convert.ToByte(85);
            var expectedSecondStep = Convert.ToByte(170);
            var expectedColors = new List<Color>
            {
                new Color().From(expectedSecondStep, expectedFirstStep, 0),
                new Color().From(expectedFirstStep, expectedSecondStep, 0),
            };

            // act
            var colors = red.InterpolateTo(green, stepCount);

            // assert
            colors.Should().BeEquivalentTo(expectedColors);
        }

        #endregion Tests: InterpolateTo()
    }
}