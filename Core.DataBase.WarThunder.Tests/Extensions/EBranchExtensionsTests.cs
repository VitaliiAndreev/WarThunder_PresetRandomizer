using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Tests.Extensions
{
    /// <summary> See <see cref="EBranchExtensions"/>. </summary>
    [TestClass]
    public class EBranchExtensionsTests
    {
        #region Tests: GetVehicleClasses()

        [TestMethod]
        public void GetVehicleClasses()
        {
            EBranch.None.GetVehicleClasses().Should().BeEquivalentTo(new List<EVehicleClass> { EVehicleClass.None });
            EBranch.Army.GetVehicleClasses().Should().BeEquivalentTo(new List<EVehicleClass> { EVehicleClass.LightTank, EVehicleClass.MediumTank, EVehicleClass.HeavyTank, EVehicleClass.TankDestroyer, EVehicleClass.Spaa });
            EBranch.Helicopters.GetVehicleClasses().Should().BeEquivalentTo(new List<EVehicleClass> { EVehicleClass.AttackHelicopter, EVehicleClass.UtilityHelicopter });
            EBranch.Aviation.GetVehicleClasses().Should().BeEquivalentTo(new List<EVehicleClass> { EVehicleClass.Fighter, EVehicleClass.Attacker, EVehicleClass.Bomber });
            EBranch.Fleet.GetVehicleClasses().Should().BeEquivalentTo(new List<EVehicleClass> { EVehicleClass.Boat, EVehicleClass.HeavyBoat, EVehicleClass.Barge, EVehicleClass.Destroyer, EVehicleClass.LightCruiser, EVehicleClass.HeavyCruiser });
        }

        #endregion Tests: GetVehicleClasses()
    }
}