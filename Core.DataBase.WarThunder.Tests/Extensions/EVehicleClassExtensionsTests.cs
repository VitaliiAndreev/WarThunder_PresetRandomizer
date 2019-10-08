using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.DataBase.WarThunder.Tests.Extensions
{
    /// <summary> See <see cref="EVehicleClassExtensions"/>. </summary>
    [TestClass]
    public class EVehicleClassExtensionsTests
    {
        #region Tests: GetBranch()

        [TestMethod]
        public void GetBranch()
        {
            EVehicleClass.None.GetBranch().Should().Be(EBranch.None);

            EVehicleClass.LightTank.GetBranch().Should().Be(EBranch.Army);
            EVehicleClass.MediumTank.GetBranch().Should().Be(EBranch.Army);
            EVehicleClass.HeavyTank.GetBranch().Should().Be(EBranch.Army);

            EVehicleClass.AttackHelicopter.GetBranch().Should().Be(EBranch.Helicopters);
            EVehicleClass.UtilityHelicopter.GetBranch().Should().Be(EBranch.Helicopters);

            EVehicleClass.Fighter.GetBranch().Should().Be(EBranch.Aviation);
            EVehicleClass.Attacker.GetBranch().Should().Be(EBranch.Aviation);
            EVehicleClass.Bomber.GetBranch().Should().Be(EBranch.Aviation);

            EVehicleClass.Boat.GetBranch().Should().Be(EBranch.Fleet);
            EVehicleClass.HeavyBoat.GetBranch().Should().Be(EBranch.Fleet);
            EVehicleClass.Barge.GetBranch().Should().Be(EBranch.Fleet);
            EVehicleClass.Destroyer.GetBranch().Should().Be(EBranch.Fleet);
            EVehicleClass.LightCruiser.GetBranch().Should().Be(EBranch.Fleet);
            EVehicleClass.HeavyCruiser.GetBranch().Should().Be(EBranch.Fleet);
        }

        #endregion Tests: GetBranch()
    }
}