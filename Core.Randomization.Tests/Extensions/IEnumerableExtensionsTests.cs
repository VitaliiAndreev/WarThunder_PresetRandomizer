using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Randomization.Extensions;
using Core.Randomization.Helpers;
using Core.Randomization.Helpers.Interfaces;
using Core.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Core.Randomization.Tests.Extensions
{
    /// <summary> See <see cref="IEnumerableExtensions"/>. </summary>
    [TestClass]
    public class IEnumerableExtensionsTests
    {
        private IRandomizer _randomizer;

        #region Internal Methods

        [TestInitialize]
        public void Initialize()
        {
            _randomizer = new CustomRandomizer(Presets.Logger);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            Presets.CleanUp();
        }

        #endregion Internal Methods
        #region Tests: GetRandomVehicle()

        [TestMethod]
        public void Randomize_SameOrderShouldBeExtremelyRare()
        {
            // arrange
            var observationCount = 1_000_000;
            var minimumValue = 0;
            var count = 10;
            var numbers = Enumerable.Range(minimumValue, count);

            var amountOfNonRandomizedCollections = default(int);
            var amountOfRandomizedCollections = default(int);

            // act
            for (var observationNumber = 0; observationNumber < observationCount; observationNumber++)
            {
                var randomizedNumbers = numbers.Randomize(_randomizer);

                if (randomizedNumbers.SequenceEqual(numbers))
                    amountOfNonRandomizedCollections++;

                else if (randomizedNumbers.OrderBy(value => value).SequenceEqual(numbers))
                    amountOfRandomizedCollections++;
            }

            // assert
            amountOfNonRandomizedCollections.Should().BeLessOrEqualTo(1);
            amountOfRandomizedCollections.Should().BeGreaterOrEqualTo(observationCount - 1);
        }

        #endregion Tests: GetRandomVehicle()
    }
}