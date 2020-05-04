using Core.DataBase.Tests.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations.Logger;
using Core.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.WarThunder.Tests.Objects
{
    /// <summary> See <see cref="Vehicle"/>.</summary>
    [TestClass]
    public class VehicleTests
    {
        private readonly IEnumerable<string> _ignoredPropertyNames;

        #region Internal Methods

        public VehicleTests()
        {
            _ignoredPropertyNames = new List<string>
            {
                nameof(INation.AsEnumerationItem),
                nameof(IVehicle.RankAsEnumerationItem),
            };
        }

        public override string ToString() => nameof(NationTests);

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            Presets.CleanUp();
        }

        #endregion Internal Methods
        #region Tests: CommitChanges() and Query()

        [TestMethod]
        public void CommitChanges_Query_ReturnsObject()
        {
            // arrange
            var fileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}()";

            using (var dataRepository = new DataRepositorySqliteWarThunder(fileName, true, Assembly.Load(EAssembly.WarThunderMappingAssembly), false, Presets.Logger))
            {
                var britain = new Nation(dataRepository, EReference.NationsFromEnumeration[ENation.GreatBritain]);
                var britishArmy = new Branch(dataRepository, EReference.BranchesFromEnumeration[EBranch.Army], britain);

                var wingedBicycle = new Vehicle(dataRepository, "Winged Bycicle")
                {
                    Nation = britain,
                    Branch = britishArmy,
                };

                // act
                dataRepository.PersistNewObjects();

                var query = dataRepository.Query<IVehicle>();
                query.Count().Should().Be(1);

                var vehicle = query.First();

                // assert
                vehicle.IsEquivalentTo(wingedBicycle, 2, _ignoredPropertyNames).Should().BeTrue();
            }
        }

        #endregion Tests: CommitChanges() and Query()
        #region Tests: IsEquivalentTo()

        [TestMethod]
        public void IsEquivalentTo_Self_ShouldBeTrue()
        {
            // arrange
            var wingedBicycle = new Vehicle(Presets.MockDataRepository.Object, "Winged Bycicle");

            // act
            var isEquivalent = wingedBicycle.IsEquivalentTo(wingedBicycle, 2, _ignoredPropertyNames);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_ShouldBeFalse()
        {
            // arrange
            var wingedBicycle = new Vehicle(Presets.MockDataRepository.Object, "Winged Bycicle");
            var trackedWheelchair = new Vehicle(Presets.MockDataRepository.Object, "Tracked Wheelchair");

            // act
            var isEquivalent = wingedBicycle.IsEquivalentTo(trackedWheelchair, 2, _ignoredPropertyNames);

            // assert
            isEquivalent.Should().BeFalse();
        }

        #endregion Tests: IsEquivalentTo()
    }
}