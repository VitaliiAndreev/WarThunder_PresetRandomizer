using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Tests.Extensions
{
    /// <summary> See <see cref="EVehicleBranchTagExtensions"/>. </summary>
    [TestClass]
    public class EVehicleBranchTagExtensionsTests
    {
        #region Methods: private

        private void DoTests(IEnumerable<Action> tests)
        {
            tests.ExecuteIfTestCountMatchesEnumerationSize<EVehicleBranchTag>("Add newly added vehicle branch tags to unit tests.");
        }

        #endregion Methods: private
        #region Tests: GetBranch()

        [TestMethod]
        public void GetBranch()
        {
            var tests = new List<Action>
            {
                () => EVehicleBranchTag.None.GetBranch().Should().Be(EBranch.None),
                () => EVehicleBranchTag.All.GetBranch().Should().Be(EBranch.All),

                () => EVehicleBranchTag.AllGroundVehicles.GetBranch().Should().Be(EBranch.Army),
                () => EVehicleBranchTag.UntaggedGroundVehicle.GetBranch().Should().Be(EBranch.Army),
                () => EVehicleBranchTag.Wheeled.GetBranch().Should().Be(EBranch.Army),
                () => EVehicleBranchTag.Scout.GetBranch().Should().Be(EBranch.Army),

                () => EVehicleBranchTag.AllHelicopters.GetBranch().Should().Be(EBranch.Helicopters),

                () => EVehicleBranchTag.AllAircraft.GetBranch().Should().Be(EBranch.Aviation),
                () => EVehicleBranchTag.UntaggedAircraft.GetBranch().Should().Be(EBranch.Aviation),
                () => EVehicleBranchTag.NavalAircraft.GetBranch().Should().Be(EBranch.Aviation),
                () => EVehicleBranchTag.Hydroplane.GetBranch().Should().Be(EBranch.Aviation),
                () => EVehicleBranchTag.TorpedoBomber.GetBranch().Should().Be(EBranch.Aviation),

                () => EVehicleBranchTag.AllShips.GetBranch().Should().Be(EBranch.Fleet),
            };

            DoTests(tests);
        }

        #endregion Tests: GetBranch()
        #region Tests: IsValid()

        [TestMethod]
        public void IsValid()
        {
            var tests = new List<Action>
            {
                () => EVehicleBranchTag.None.IsValid().Should().BeFalse(),
                () => EVehicleBranchTag.All.IsValid().Should().BeFalse(),

                () => EVehicleBranchTag.AllGroundVehicles.IsValid().Should().BeFalse(),
                () => EVehicleBranchTag.UntaggedGroundVehicle.IsValid().Should().BeTrue(),
                () => EVehicleBranchTag.Wheeled.IsValid().Should().BeTrue(),
                () => EVehicleBranchTag.Scout.IsValid().Should().BeTrue(),

                () => EVehicleBranchTag.AllHelicopters.IsValid().Should().BeFalse(),

                () => EVehicleBranchTag.AllAircraft.IsValid().Should().BeFalse(),
                () => EVehicleBranchTag.UntaggedAircraft.IsValid().Should().BeTrue(),
                () => EVehicleBranchTag.NavalAircraft.IsValid().Should().BeTrue(),
                () => EVehicleBranchTag.Hydroplane.IsValid().Should().BeTrue(),
                () => EVehicleBranchTag.TorpedoBomber.IsValid().Should().BeTrue(),

                () => EVehicleBranchTag.AllShips.IsValid().Should().BeFalse(),
            };

            DoTests(tests);
        }

        #endregion Tests: IsValid()
    }
}