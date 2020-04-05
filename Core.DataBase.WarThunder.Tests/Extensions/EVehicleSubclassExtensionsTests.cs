using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Tests.Extensions
{
    /// <summary> See <see cref="EVehicleSubclassExtensions"/>. </summary>
    [TestClass]
    public class EVehicleSubclassExtensionsTests
    {
        #region Methods: private

        private void DoTests(IEnumerable<Action> tests)
        {
            tests.ExecuteIfTestCountMatchesEnumerationSize<EVehicleSubclass>("Add newly added vehicle subclasses to unit tests.");
        }

        #endregion Methods: private
        #region Tests: GetVehicleClass()

        [TestMethod]
        public void GetVehicleClass()
        {
            var tests = new List<Action>
            {
                () => EVehicleSubclass.None.GetVehicleClass().Should().Be(EVehicleClass.None),
                () => EVehicleSubclass.All.GetVehicleClass().Should().Be(EVehicleClass.All),

                () => EVehicleSubclass.AllLightTanks.GetVehicleClass().Should().Be(EVehicleClass.LightTank),

                () => EVehicleSubclass.AllMediumTanks.GetVehicleClass().Should().Be(EVehicleClass.MediumTank),

                () => EVehicleSubclass.AllHeavyTanks.GetVehicleClass().Should().Be(EVehicleClass.HeavyTank),

                () => EVehicleSubclass.AllTankDestroyers.GetVehicleClass().Should().Be(EVehicleClass.TankDestroyer),

                () => EVehicleSubclass.AllSpaas.GetVehicleClass().Should().Be(EVehicleClass.Spaa),

                () => EVehicleSubclass.AllAttackHelicopters.GetVehicleClass().Should().Be(EVehicleClass.AttackHelicopter),

                () => EVehicleSubclass.AllUtilityHelicopters.GetVehicleClass().Should().Be(EVehicleClass.UtilityHelicopter),

                () => EVehicleSubclass.AllFighters.GetVehicleClass().Should().Be(EVehicleClass.Fighter),
                () => EVehicleSubclass.Fighter.GetVehicleClass().Should().Be(EVehicleClass.Fighter),
                () => EVehicleSubclass.Interceptor.GetVehicleClass().Should().Be(EVehicleClass.Fighter),
                () => EVehicleSubclass.NightFighter.GetVehicleClass().Should().Be(EVehicleClass.Fighter),
                () => EVehicleSubclass.StrikeFighter.GetVehicleClass().Should().Be(EVehicleClass.Fighter),
                () => EVehicleSubclass.JetFighter.GetVehicleClass().Should().Be(EVehicleClass.Fighter),

                () => EVehicleSubclass.AllAttackers.GetVehicleClass().Should().Be(EVehicleClass.Attacker),

                () => EVehicleSubclass.AllBombers.GetVehicleClass().Should().Be(EVehicleClass.Bomber),

                () => EVehicleSubclass.AllBoats.GetVehicleClass().Should().Be(EVehicleClass.Boat),

                () => EVehicleSubclass.AllHeavyBoats.GetVehicleClass().Should().Be(EVehicleClass.HeavyBoat),

                () => EVehicleSubclass.AllBarges.GetVehicleClass().Should().Be(EVehicleClass.Barge),

                () => EVehicleSubclass.AllFrigates.GetVehicleClass().Should().Be(EVehicleClass.Frigate),

                () => EVehicleSubclass.AllDestroyers.GetVehicleClass().Should().Be(EVehicleClass.Destroyer),

                () => EVehicleSubclass.AllLightCruisers.GetVehicleClass().Should().Be(EVehicleClass.LightCruiser),

                () => EVehicleSubclass.AllHeavyCruisers.GetVehicleClass().Should().Be(EVehicleClass.HeavyCruiser),
            };

            DoTests(tests);
        }

        #endregion Tests: GetVehicleClass()
        #region Tests: IsValid()

        [TestMethod]
        public void IsValid()
        {
            var tests = new List<Action>
            {
                () => EVehicleSubclass.None.IsValid().Should().BeFalse(),
                () => EVehicleSubclass.All.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllLightTanks.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllMediumTanks.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllHeavyTanks.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllTankDestroyers.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllSpaas.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllAttackHelicopters.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllUtilityHelicopters.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllFighters.IsValid().Should().BeFalse(),
                () => EVehicleSubclass.Fighter.IsValid().Should().BeTrue(),
                () => EVehicleSubclass.Interceptor.IsValid().Should().BeTrue(),
                () => EVehicleSubclass.NightFighter.IsValid().Should().BeTrue(),
                () => EVehicleSubclass.StrikeFighter.IsValid().Should().BeTrue(),
                () => EVehicleSubclass.JetFighter.IsValid().Should().BeTrue(),

                () => EVehicleSubclass.AllAttackers.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllBombers.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllBoats.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllHeavyBoats.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllBarges.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllFrigates.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllDestroyers.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllLightCruisers.IsValid().Should().BeFalse(),

                () => EVehicleSubclass.AllHeavyCruisers.IsValid().Should().BeFalse(),
            };

            DoTests(tests);
        }

        #endregion Tests: IsValid()
    }
}