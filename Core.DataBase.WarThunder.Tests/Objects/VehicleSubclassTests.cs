using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations.Logger;
using Core.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Tests.Objects
{
    /// <summary> See <see cref="VehicleSubclass"/>.</summary>
    [TestClass]
    public class VehicleSubclassTests
    {
        #region Internal Methods

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            Presets.CleanUp();
        }

        #endregion Internal Methods
        #region Tests: All

        [TestMethod]
        public void All_1_Returns_1()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.Fighter };
            var subclass = new VehicleSubclass(Presets.MockDataRepository.Object, mockVehicle.Object, subclasses);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void All_2_Returns_2()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.Fighter, EVehicleSubclass.Interceptor };
            var subclass = new VehicleSubclass(Presets.MockDataRepository.Object, mockVehicle.Object, subclasses);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void All_3_Returns_3()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.Fighter, EVehicleSubclass.Interceptor, EVehicleSubclass.JetFighter };
            var subclass = new VehicleSubclass(Presets.MockDataRepository.Object, mockVehicle.Object, subclasses);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void All_4_1_Dupe_Returns_3()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.Fighter, EVehicleSubclass.Fighter, EVehicleSubclass.Interceptor, EVehicleSubclass.JetFighter };
            var subclass = new VehicleSubclass(Presets.MockDataRepository.Object, mockVehicle.Object, subclasses);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses.Distinct());
        }

        [TestMethod]
        public void All_4_Throws()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));

            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.Fighter, EVehicleSubclass.Interceptor, EVehicleSubclass.AirDefenceFighter, EVehicleSubclass.JetFighter };

            // act
            Action createSubclass = () => { new VehicleSubclass(Presets.MockDataRepository.Object, mockVehicle.Object, subclasses); };

            // assert
            createSubclass.Should().Throw<NotImplementedException>();
        }

        #endregion Tests: All
    }
}