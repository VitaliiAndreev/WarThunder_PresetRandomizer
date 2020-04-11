using Core.DataBase.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations.Logger;
using Core.Objects;
using Core.Organization.Extensions;
using Core.Organization.Helpers;
using Core.Organization.Helpers.Interfaces;
using Core.Randomization.Helpers;
using Core.Randomization.Helpers.Interfaces;
using Core.Tests;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Core.Organization.Tests.Helpers
{
    /// <summary> See <see cref="VehicleSelector"/>. </summary>
    [TestClass]
    public class VehicleSelectorTests
    {
        #region Fields

        private IRandomiser _randomizer;
        private IVehicleSelector _vehicleSelector;

        #endregion Fields
        #region Internal Methods

        [TestInitialize]
        public void Initialize()
        {
            _randomizer = new CustomRandomiser(Presets.Logger);
            _vehicleSelector = new VehicleSelector(_randomizer, Presets.Logger);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            Presets.CleanUp();
        }

        #endregion Internal Methods
        #region Tests: OrderByHighestBattleRating()

        [TestMethod]
        public void OrderByHighestBattleRating_VehiclesOutsideOfBracketNotTaken()
        {
            // arrange
            var m8a1 = new Mock<IVehicle>();
            m8a1.Setup(vehicle => vehicle.BattleRating[EGameMode.Arcade]).Returns(new decimal?(2.7m));

            var grant = new Mock<IVehicle>();
            grant.Setup(vehicle => vehicle.BattleRating[EGameMode.Arcade]).Returns(new decimal?(2.7m));

            var m5a1 = new Mock<IVehicle>();
            m5a1.Setup(vehicle => vehicle.BattleRating[EGameMode.Arcade]).Returns(new decimal?(2.3m));

            var m3 = new Mock<IVehicle>();
            m3.Setup(vehicle => vehicle.BattleRating[EGameMode.Arcade]).Returns(new decimal?(1.0m));

            var vehicles = new List<IVehicle>
            {
                m8a1.Object,
                m3.Object,
                m5a1.Object,
                grant.Object,
            };

            var maximumBattleRating = m8a1.Object.BattleRating[EGameMode.Arcade].Value;
            var minumumBattleRating = maximumBattleRating - 1.0m;
            var battleRatingBracket = new Interval<decimal>(true, minumumBattleRating, maximumBattleRating, true);

            var expectedDictionary = new Dictionary<decimal, IList<IVehicle>>
            {
                { 2.7m, new List<IVehicle> { m8a1.Object, grant.Object } },
                { 2.3m, new List<IVehicle> { m5a1.Object } },
            };

            // act
            var actualDictionary = _vehicleSelector.OrderByHighestBattleRating(EGameMode.Arcade, battleRatingBracket, vehicles);

            // assert
            actualDictionary.Should().BeEquivalentTo(expectedDictionary);
        }

        #endregion Tests: OrderByHighestBattleRating()
        #region Tests: RandomizeSelection()

        [TestMethod]
        public void RandomizeSelection()
        {
            // arrange
            var m8a1 = new Mock<IVehicle>();
            m8a1.Setup(vehicle => vehicle.BattleRating.Arcade).Returns(new decimal?(2.7m));
            m8a1.Setup(vehicle => vehicle.IsEquivalentTo(m8a1.Object, It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).Returns(true);

            var grant = new Mock<IVehicle>();
            grant.Setup(vehicle => vehicle.BattleRating.Arcade).Returns(new decimal?(2.7m));
            grant.Setup(vehicle => vehicle.IsEquivalentTo(grant.Object, It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).Returns(true);

            var m5a1 = new Mock<IVehicle>();
            m5a1.Setup(vehicle => vehicle.BattleRating.Arcade).Returns(new decimal?(2.3m));
            m5a1.Setup(vehicle => vehicle.IsEquivalentTo(m5a1.Object, It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).Returns(true);

            var m5a1premium = new Mock<IVehicle>();
            m5a1premium.Setup(vehicle => vehicle.BattleRating.Arcade).Returns(new decimal?(2.3m));
            m5a1premium.Setup(vehicle => vehicle.IsEquivalentTo(m5a1premium.Object, It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).Returns(true);

            var vehicles = new Dictionary<decimal, IList<IVehicle>>
            {
                { 2.7m, new List<IVehicle> { m8a1.Object, grant.Object } },
                { 2.3m, new List<IVehicle> { m5a1.Object, m5a1premium.Object } },
            };

            var vehicleCombination1 = new List<IVehicle> { m8a1.Object, grant.Object, m5a1.Object, m5a1premium.Object };
            var vehicleCombination2 = new List<IVehicle> { grant.Object, m8a1.Object, m5a1.Object, m5a1premium.Object };
            var vehicleCombination3 = new List<IVehicle> { m8a1.Object, grant.Object, m5a1premium.Object, m5a1.Object };
            var vehicleCombination4 = new List<IVehicle> { grant.Object, m8a1.Object, m5a1premium.Object, m5a1.Object };

            // act
            var randomizedVehicles = vehicles.GetRandomVehicles(_vehicleSelector, 10);

            // assert
            var isCombination1 = randomizedVehicles.IsEquivalentTo(vehicleCombination1);
            var isCombination2 = randomizedVehicles.IsEquivalentTo(vehicleCombination2);
            var isCombination3 = randomizedVehicles.IsEquivalentTo(vehicleCombination3);
            var isCombination4 = randomizedVehicles.IsEquivalentTo(vehicleCombination4);

            (isCombination1 | isCombination2 | isCombination3 | isCombination4).Should().BeTrue();
        }

        #endregion Tests: RandomizeSelection()
    }
}
