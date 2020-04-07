using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.Enumerations.Logger;
using Core.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Tests.Objects
{
    /// <summary> See <see cref="VehicleSubclasses"/>.</summary>
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
        #region Tests: Constructor()

        [TestMethod]
        public void Constructor_1_Returns_1()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Fighter);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsFighter = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.Fighter };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_2_Returns_2()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Fighter);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsInterceptor = true,
                IsAirDefenceFighter = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.Interceptor, EVehicleSubclass.AirDefenceFighter };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_3_Returns_3()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Fighter);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsInterceptor = true,
                IsAirDefenceFighter = true,
                IsJetFighter = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.Interceptor, EVehicleSubclass.AirDefenceFighter, EVehicleSubclass.JetFighter };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_4_Throws()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Fighter);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsInterceptor = true,
                IsAirDefenceFighter = true,
                IsStrikeFighter = true,
                IsJetFighter = true,
            };

            // act
            Action createSubclass = () => { new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags); };

            // assert
            createSubclass.Should().Throw<NotImplementedException>();
        }

        #endregion Tests: Constructor
    }
}