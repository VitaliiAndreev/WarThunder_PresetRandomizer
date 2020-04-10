using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Tests.Extensions
{
    /// <summary> See <see cref="EBranchExtensions"/>. </summary>
    [TestClass]
    public class EBranchExtensionsTests
    {
        #region Methods: private

        private void DoTests(IEnumerable<Action> tests)
        {
            tests.ExecuteIfTestCountMatchesEnumerationSize<EBranch>("Add newly added branches classes to unit tests.");
        }

        #endregion Methods: private
        #region Tests: GetAllVehicleClassesItem()

        [TestMethod]
        public void GetAllVehicleClassesItem()
        {
            var tests = new List<Action>
            {
                () => EBranch.None.GetAllVehicleClassesItem().Should().Be(EVehicleClass.None),
                () => EBranch.All.GetAllVehicleClassesItem().Should().Be(EVehicleClass.All),
                () => EBranch.Army.GetAllVehicleClassesItem().Should().Be(EVehicleClass.AllGroundVehicles),
                () => EBranch.Helicopters.GetAllVehicleClassesItem().Should().Be(EVehicleClass.AllHelicopters),
                () => EBranch.Aviation.GetAllVehicleClassesItem().Should().Be(EVehicleClass.AllAircraft),
                () => EBranch.Fleet.GetAllVehicleClassesItem().Should().Be(EVehicleClass.AllShips),
            };

            DoTests(tests);
        }

        #endregion Tests: GetAllVehicleClassesItem()
        #region Tests: GetVehicleBranchTags()

        [TestMethod]
        public void GetVehicleBranchTags()
        {
            var tests = new List<Action>
            {
                () => EBranch.None.GetVehicleBranchTags().Should().BeEquivalentTo(new List<EVehicleBranchTag> { EVehicleBranchTag.None }),
                () => EBranch.All.GetVehicleBranchTags().Should().BeEquivalentTo(new List<EVehicleBranchTag> { EVehicleBranchTag.All }),
                () => EBranch.Army.GetVehicleBranchTags().Should().BeEquivalentTo(new List<EVehicleBranchTag> { EVehicleBranchTag.AllGroundVehicles, }),
                () => EBranch.Helicopters.GetVehicleBranchTags().Should().BeEquivalentTo(new List<EVehicleBranchTag> { EVehicleBranchTag.AllHelicopters, }),
                () => EBranch.Aviation.GetVehicleBranchTags().Should().BeEquivalentTo(new List<EVehicleBranchTag> { EVehicleBranchTag.AllAircraft, EVehicleBranchTag.UntaggedAircraft, EVehicleBranchTag.NavalAircraft, EVehicleBranchTag.Hydroplane, EVehicleBranchTag.TorpedoBomber }),
                () => EBranch.Fleet.GetVehicleBranchTags().Should().BeEquivalentTo(new List<EVehicleBranchTag> { EVehicleBranchTag.AllShips }),
            };

            DoTests(tests);
        }

        #endregion Tests: GetVehicleBranchTags()
        #region Tests: GetVehicleClasses()

        [TestMethod]
        public void GetVehicleClasses()
        {
            var tests = new List<Action>
            {
                () => EBranch.None.GetVehicleClasses().Should().BeEquivalentTo(new List<EVehicleClass> { EVehicleClass.None }),
                () => EBranch.All.GetVehicleClasses().Should().BeEquivalentTo(new List<EVehicleClass> { EVehicleClass.All }),
                () => EBranch.Army.GetVehicleClasses().Should().BeEquivalentTo(new List<EVehicleClass> { EVehicleClass.AllGroundVehicles, EVehicleClass.LightTank, EVehicleClass.MediumTank, EVehicleClass.HeavyTank, EVehicleClass.TankDestroyer, EVehicleClass.Spaa }),
                () => EBranch.Helicopters.GetVehicleClasses().Should().BeEquivalentTo(new List<EVehicleClass> { EVehicleClass.AllHelicopters, EVehicleClass.AttackHelicopter, EVehicleClass.UtilityHelicopter }),
                () => EBranch.Aviation.GetVehicleClasses().Should().BeEquivalentTo(new List<EVehicleClass> { EVehicleClass.AllAircraft, EVehicleClass.Fighter, EVehicleClass.Attacker, EVehicleClass.Bomber }),
                () => EBranch.Fleet.GetVehicleClasses().Should().BeEquivalentTo(new List<EVehicleClass> { EVehicleClass.AllShips, EVehicleClass.Boat, EVehicleClass.HeavyBoat, EVehicleClass.Barge, EVehicleClass.Destroyer, EVehicleClass.LightCruiser, EVehicleClass.HeavyCruiser, EVehicleClass.Frigate }),
            };

            DoTests(tests);
        }

        #endregion Tests: GetVehicleClasses()
        #region Tests: IsValid()

        [TestMethod]
        public void IsValid()
        {
            var tests = new List<Action>
            {
                () => EBranch.None.IsValid().Should().BeFalse(),
                () => EBranch.All.IsValid().Should().BeFalse(),
                () => EBranch.Army.IsValid().Should().BeTrue(),
                () => EBranch.Helicopters.IsValid().Should().BeTrue(),
                () => EBranch.Aviation.IsValid().Should().BeTrue(),
                () => EBranch.Fleet.IsValid().Should().BeTrue(),
            };

            DoTests(tests);
        }

        #endregion Tests: IsValid()
    }
}