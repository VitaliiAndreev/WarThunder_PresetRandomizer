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

        #region Tank Destroyers

        [TestMethod]
        public void Constructor_Td_Atgm_Returns_Atgm()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.TankDestroyer);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsTankDestroyer = true,
                IsAtgmCarrier = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.AntiTankMissileCarrier };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        #endregion Tank Destroyers
        #region Fighters

        [TestMethod]
        public void Constructor_Fighter_Returns_Fighter()
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
        public void Constructor_Fighter_Interceptor_Returns_Interceptor()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Fighter);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsFighter = true,
                IsInterceptor = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.Interceptor };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_Fighter_NightFighter_Returns_NightFighter()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Fighter);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsFighter = true,
                IsAirDefenceFighter = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.AirDefenceFighter };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_Fighter_StrikeFighter_Returns_StrikeFighter()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Fighter);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsFighter = true,
                IsStrikeFighter = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.StrikeFighter };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_Fighter_JetFighter_Returns_JetFighter()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Fighter);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsFighter = true,
                IsJetFighter = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.JetFighter };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        #endregion Fighters
        #region Bombers

        [TestMethod]
        public void Constructor_Bomber_LightBomber_Returns_LightBomber()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Bomber);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsBomber = true,
                IsLightBomber = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.LightBomber };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_Bomber_DiveBomber_Returns_DiveBomber()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Bomber);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsBomber = true,
                IsDiveBomber = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.DiveBomber };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_Bomber_Returns_Bomber()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Bomber);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsBomber = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.Bomber };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_FrontBomber_Returns_FrontBomber()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Bomber);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsBomber = true,
                IsFrontlineBomber = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.FrontlineBomber };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_HeavyBomber_Returns_HeavyBomber()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Bomber);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsBomber = true,
                IsLongRangeBomber = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.LongRangeBomber };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_JetBomber_Returns_JetBomber()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Bomber);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsBomber = true,
                IsJetBomber = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.JetBomber };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        #endregion Bombers
        #region Boats

        [TestMethod]
        public void Constructor_Boat_Gunboat_Returns_Gunboat()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Boat);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsBoat = true,
                IsGunBoat = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.MotorGunboat };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_Boat_TorpedoBoat_Returns_TorpedoBoat()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Boat);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsBoat = true,
                IsTorpedoBoat = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.MotorTorpedoBoat };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        #endregion Boats
        #region Heavy Boats

        [TestMethod]
        public void Constructor_HeavyBoat_ArmoredBoat_Returns_ArmoredBoat()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.HeavyBoat);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsHeavyBoat = true,
                IsArmoredBoat = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.ArmoredGunboat };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_HeavyBoat_Multirole_Returns_Multirole()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.HeavyBoat);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsHeavyBoat = true,
                IsTorpedoGunBoat = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.MotorTorpedoGunboat };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_HeavyBoat_Chaser_Returns_Chaser()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.HeavyBoat);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsHeavyBoat = true,
                IsSubmarineChaser = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.SubChaser };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        #endregion Heavy Boats
        #region Barges

        [TestMethod]
        public void Constructor_Barge_FlakFerry_Returns_FlakFerry()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Barge);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsBarge = true,
                IsAaFerry = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.AntiAirFerry };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_Barge_Ferry_Returns_Ferry()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Barge);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsBarge = true,
                IsFerry = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.NavalFerryBarge };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        #endregion Barges
        #region Frigates

        [TestMethod]
        public void Constructor_Frigate_Returns_Frigate()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Frigate);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsFrigate = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.Frigate };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        [TestMethod]
        public void Constructor_Frigate_Gunboat_Returns_Gunboat()
        {
            // arrange
            var mockVehicle = new Mock<IVehicle>();
            mockVehicle.Setup(vehicle => vehicle.GaijinId).Returns(nameof(mockVehicle));
            mockVehicle.Setup(vehicle => vehicle.Class).Returns(EVehicleClass.Frigate);

            var vehicleTags = new VehicleTagsDeserializedFromJson
            {
                IsFrigate = true,
                IsHeavyGunBoat = true,
            };
            var subclasses = new List<EVehicleSubclass> { EVehicleSubclass.HeavyGunboat };
            var subclass = new VehicleSubclasses(Presets.MockDataRepository.Object, mockVehicle.Object, vehicleTags);

            // act
            var allSubclasses = subclass.All;

            // assert
            allSubclasses.Should().BeEquivalentTo(subclasses);
        }

        #endregion Tank Destroyers

        #endregion Tests: Constructor
    }
}