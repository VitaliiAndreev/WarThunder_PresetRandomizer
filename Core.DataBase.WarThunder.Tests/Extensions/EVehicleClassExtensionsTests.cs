﻿using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Tests.Extensions
{
    /// <summary> See <see cref="EVehicleClassExtensions"/>. </summary>
    [TestClass]
    public class EVehicleClassExtensionsTests
    {
        #region Methods: private

        private void DoTests(IEnumerable<Action> tests)
        {
            tests.ExecuteIfTestCountMatchesEnumerationSize<EVehicleClass>("Add newly added vehicle classes to unit tests.");
        }

        #endregion Methods: private
        #region Tests: GetBranch()

        [TestMethod]
        public void GetBranch()
        {
            var tests = new List<Action>
            {
                () => EVehicleClass.None.GetBranch().Should().Be(EBranch.None),
                () => EVehicleClass.All.GetBranch().Should().Be(EBranch.All),

                () => EVehicleClass.AllGroundVehicles.GetBranch().Should().Be(EBranch.Army),
                () => EVehicleClass.LightTank.GetBranch().Should().Be(EBranch.Army),
                () => EVehicleClass.MediumTank.GetBranch().Should().Be(EBranch.Army),
                () => EVehicleClass.HeavyTank.GetBranch().Should().Be(EBranch.Army),
                () => EVehicleClass.TankDestroyer.GetBranch().Should().Be(EBranch.Army),
                () => EVehicleClass.Spaa.GetBranch().Should().Be(EBranch.Army),

                () => EVehicleClass.AllHelicopters.GetBranch().Should().Be(EBranch.Helicopters),
                () => EVehicleClass.AttackHelicopter.GetBranch().Should().Be(EBranch.Helicopters),
                () => EVehicleClass.UtilityHelicopter.GetBranch().Should().Be(EBranch.Helicopters),

                () => EVehicleClass.AllAircraft.GetBranch().Should().Be(EBranch.Aviation),
                () => EVehicleClass.Fighter.GetBranch().Should().Be(EBranch.Aviation),
                () => EVehicleClass.Attacker.GetBranch().Should().Be(EBranch.Aviation),
                () => EVehicleClass.Bomber.GetBranch().Should().Be(EBranch.Aviation),

                () => EVehicleClass.AllFleet.GetBranch().Should().Be(EBranch.AllFleet),
                () => EVehicleClass.AllBluewaterFleet.GetBranch().Should().Be(EBranch.BluewaterFleet),
                () => EVehicleClass.Destroyer.GetBranch().Should().Be(EBranch.BluewaterFleet),
                () => EVehicleClass.LightCruiser.GetBranch().Should().Be(EBranch.BluewaterFleet),
                () => EVehicleClass.HeavyCruiser.GetBranch().Should().Be(EBranch.BluewaterFleet),
                () => EVehicleClass.Battlecruiser.GetBranch().Should().Be(EBranch.BluewaterFleet),
                () => EVehicleClass.Battleship.GetBranch().Should().Be(EBranch.BluewaterFleet),
                () => EVehicleClass.AllCoastalFleet.GetBranch().Should().Be(EBranch.CoastalFleet),
                () => EVehicleClass.Boat.GetBranch().Should().Be(EBranch.CoastalFleet),
                () => EVehicleClass.HeavyBoat.GetBranch().Should().Be(EBranch.CoastalFleet),
                () => EVehicleClass.Barge.GetBranch().Should().Be(EBranch.CoastalFleet),
                () => EVehicleClass.Frigate.GetBranch().Should().Be(EBranch.CoastalFleet),
            };

            DoTests(tests);
        }

        #endregion Tests: GetBranch()
        #region Tests: GetVehicleSubclasses()

        [TestMethod]
        public void GetVehicleSubclasses()
        {
            var tests = new List<Action>
            {
                () => EVehicleClass.None.GetVehicleSubclasses().Should().BeEmpty(),
                () => EVehicleClass.All.GetVehicleSubclasses().Should().BeEmpty(),

                () => EVehicleClass.AllGroundVehicles.GetVehicleSubclasses().Should().BeEmpty(),
                () => EVehicleClass.LightTank.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.MediumTank.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.HeavyTank.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.TankDestroyer.GetVehicleSubclasses().Should().BeEquivalentTo
                (
                    new List<EVehicleSubclass>
                    {
                        EVehicleSubclass.TankDestroyer,
                        EVehicleSubclass.AntiTankMissileCarrier,
                    }
                ),
                () => EVehicleClass.Spaa.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),

                () => EVehicleClass.AllHelicopters.GetVehicleSubclasses().Should().BeEmpty(),
                () => EVehicleClass.AttackHelicopter.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.UtilityHelicopter.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),

                () => EVehicleClass.AllAircraft.GetVehicleSubclasses().Should().BeEmpty(),
                () => EVehicleClass.Fighter.GetVehicleSubclasses().Should().BeEquivalentTo
                (
                    new List<EVehicleSubclass>
                    {
                        EVehicleSubclass.Fighter,
                        EVehicleSubclass.Interceptor,
                        EVehicleSubclass.AirDefenceFighter,
                        EVehicleSubclass.JetFighter,
                    }
                ),
                () => EVehicleClass.Attacker.GetVehicleSubclasses().Should().BeEquivalentTo
                (
                    new List<EVehicleSubclass>
                    {
                        EVehicleSubclass.StrikeAircraft,
                    }
                ),
                () => EVehicleClass.Bomber.GetVehicleSubclasses().Should().BeEquivalentTo
                (
                    new List<EVehicleSubclass>
                    {
                        EVehicleSubclass.LightBomber,
                        EVehicleSubclass.DiveBomber,
                        EVehicleSubclass.Bomber,
                        EVehicleSubclass.FrontlineBomber,
                        EVehicleSubclass.LongRangeBomber,
                        EVehicleSubclass.JetBomber,
                    }
                ),

                () => EVehicleClass.AllFleet.GetVehicleSubclasses().Should().BeEmpty(),
                () => EVehicleClass.AllBluewaterFleet.GetVehicleSubclasses().Should().BeEmpty(),
                () => EVehicleClass.Destroyer.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.LightCruiser.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.HeavyCruiser.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.Battlecruiser.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.Battleship.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.AllCoastalFleet.GetVehicleSubclasses().Should().BeEmpty(),
                () => EVehicleClass.Boat.GetVehicleSubclasses().Should().BeEquivalentTo
                (
                    new List<EVehicleSubclass>
                    {
                        EVehicleSubclass.MotorGunboat,
                        EVehicleSubclass.MotorTorpedoBoat,
                        EVehicleSubclass.Minelayer,
                    }
                ),
                () => EVehicleClass.HeavyBoat.GetVehicleSubclasses().Should().BeEquivalentTo
                (
                    new List<EVehicleSubclass>
                    {
                        EVehicleSubclass.ArmoredGunboat,
                        EVehicleSubclass.MotorTorpedoGunboat,
                        EVehicleSubclass.SubChaser,
                    }
                ),
                () => EVehicleClass.Barge.GetVehicleSubclasses().Should().BeEquivalentTo
                (
                    new List<EVehicleSubclass>
                    {
                        EVehicleSubclass.AntiAirFerry,
                        EVehicleSubclass.NavalFerryBarge,
                    }
                ),
                () => EVehicleClass.Frigate.GetVehicleSubclasses().Should().BeEquivalentTo
                (
                    new List<EVehicleSubclass>
                    {
                        EVehicleSubclass.HeavyGunboat,
                        EVehicleSubclass.Frigate,
                    }
                ),
            };

            DoTests(tests);
        }

        #endregion Tests: GetVehicleSubclasses()
        #region Tests: IsValid()

        [TestMethod]
        public void IsValid()
        {
            var tests = new List<Action>
            {
                () => EVehicleClass.None.IsValid().Should().BeFalse(),
                () => EVehicleClass.All.IsValid().Should().BeFalse(),

                () => EVehicleClass.AllGroundVehicles.IsValid().Should().BeFalse(),
                () => EVehicleClass.LightTank.IsValid().Should().BeTrue(),
                () => EVehicleClass.MediumTank.IsValid().Should().BeTrue(),
                () => EVehicleClass.HeavyTank.IsValid().Should().BeTrue(),
                () => EVehicleClass.TankDestroyer.IsValid().Should().BeTrue(),
                () => EVehicleClass.Spaa.IsValid().Should().BeTrue(),

                () => EVehicleClass.AllHelicopters.IsValid().Should().BeFalse(),
                () => EVehicleClass.AttackHelicopter.IsValid().Should().BeTrue(),
                () => EVehicleClass.UtilityHelicopter.IsValid().Should().BeTrue(),

                () => EVehicleClass.AllAircraft.IsValid().Should().BeFalse(),
                () => EVehicleClass.Fighter.IsValid().Should().BeTrue(),
                () => EVehicleClass.Attacker.IsValid().Should().BeTrue(),
                () => EVehicleClass.Bomber.IsValid().Should().BeTrue(),

                () => EVehicleClass.AllFleet.IsValid().Should().BeFalse(),
                () => EVehicleClass.AllBluewaterFleet.IsValid().Should().BeFalse(),
                () => EVehicleClass.Destroyer.IsValid().Should().BeTrue(),
                () => EVehicleClass.LightCruiser.IsValid().Should().BeTrue(),
                () => EVehicleClass.HeavyCruiser.IsValid().Should().BeTrue(),
                () => EVehicleClass.Battlecruiser.IsValid().Should().BeTrue(),
                () => EVehicleClass.Battleship.IsValid().Should().BeTrue(),
                () => EVehicleClass.AllCoastalFleet.IsValid().Should().BeFalse(),
                () => EVehicleClass.Boat.IsValid().Should().BeTrue(),
                () => EVehicleClass.HeavyBoat.IsValid().Should().BeTrue(),
                () => EVehicleClass.Barge.IsValid().Should().BeTrue(),
                () => EVehicleClass.Frigate.IsValid().Should().BeTrue(),
            };

            DoTests(tests);
        }

        #endregion Tests: IsValid()
    }
}