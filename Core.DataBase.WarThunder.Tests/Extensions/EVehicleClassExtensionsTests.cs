using Core.DataBase.WarThunder.Enumerations;
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

                () => EVehicleClass.AllShips.GetBranch().Should().Be(EBranch.Fleet),
                () => EVehicleClass.Boat.GetBranch().Should().Be(EBranch.Fleet),
                () => EVehicleClass.HeavyBoat.GetBranch().Should().Be(EBranch.Fleet),
                () => EVehicleClass.Barge.GetBranch().Should().Be(EBranch.Fleet),
                () => EVehicleClass.Frigate.GetBranch().Should().Be(EBranch.Fleet),
                () => EVehicleClass.Destroyer.GetBranch().Should().Be(EBranch.Fleet),
                () => EVehicleClass.LightCruiser.GetBranch().Should().Be(EBranch.Fleet),
                () => EVehicleClass.HeavyCruiser.GetBranch().Should().Be(EBranch.Fleet),
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
                        EVehicleSubclass.StrikeFighter,
                        EVehicleSubclass.JetFighter,
                    }
                ),
                () => EVehicleClass.Attacker.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
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

                () => EVehicleClass.AllShips.GetVehicleSubclasses().Should().BeEmpty(),
                () => EVehicleClass.Boat.GetVehicleSubclasses().Should().BeEquivalentTo
                (
                    new List<EVehicleSubclass>
                    {
                        EVehicleSubclass.MotorGunBoat,
                        EVehicleSubclass.MotorTorpedoBoat,
                    }
                ),
                () => EVehicleClass.HeavyBoat.GetVehicleSubclasses().Should().BeEquivalentTo
                (
                    new List<EVehicleSubclass>
                    {
                        EVehicleSubclass.ArmoredGunBoat,
                        EVehicleSubclass.MotorTorpedoGunBoat,
                        EVehicleSubclass.SubChaser,
                    }
                ),
                () => EVehicleClass.Barge.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.Frigate.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.Destroyer.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.LightCruiser.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
                () => EVehicleClass.HeavyCruiser.GetVehicleSubclasses().Should().BeEquivalentTo(new List<EVehicleSubclass>()),
            };
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

                () => EVehicleClass.AllShips.IsValid().Should().BeFalse(),
                () => EVehicleClass.Boat.IsValid().Should().BeTrue(),
                () => EVehicleClass.HeavyBoat.IsValid().Should().BeTrue(),
                () => EVehicleClass.Barge.IsValid().Should().BeTrue(),
                () => EVehicleClass.Frigate.IsValid().Should().BeTrue(),
                () => EVehicleClass.Destroyer.IsValid().Should().BeTrue(),
                () => EVehicleClass.LightCruiser.IsValid().Should().BeTrue(),
                () => EVehicleClass.HeavyCruiser.IsValid().Should().BeTrue(),
            };
        }

        #endregion Tests: IsValid()
    }
}