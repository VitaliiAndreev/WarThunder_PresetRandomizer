using Core.Enumerations.Logger;
using Core.Randomization.Helpers;
using Core.Randomization.Helpers.Interfaces;
using Core.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Core.Randomization.Tests.Helpers
{
    /// <summary> See <see cref="Randomizer"/>. </summary>
    [TestClass]
    public class CustomRandomizerTests
    {
        private IRandomiser _randomizer;

        #region Internal Methods

        [TestInitialize]
        public void Initialise()
        {
            _randomizer = new CustomRandomiser(Presets.Logger);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(CoreLogCategory.UnitTests, CoreLogMessage.CleanUpAfterUnitTestStartsHere);
            Presets.CleanUp();
        }

        #endregion Internal Methods
        #region Tests: GetRandomVehicle()

        [TestMethod]
        public void GetRandom_MustReturnEdgeCases()
        {
            // arrange
            var observationCount = 1_000_000;
            var minimumValue = 0;
            var count = 100;

            var numbers = Enumerable.Range(minimumValue, count);
            var selectedNumbers = new List<double>();

            // act
            for (var observationNumber = 0; observationNumber < observationCount; observationNumber++)
                selectedNumbers.Add(_randomizer.GetRandom(numbers));

            // assert
            selectedNumbers.Should().Contain(numbers.First());
            selectedNumbers.Should().Contain(numbers.Last());
        }

        #endregion Tests: GetRandomVehicle()
    }
}