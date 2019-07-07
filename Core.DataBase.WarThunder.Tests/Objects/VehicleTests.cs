using Core.DataBase.Tests.Enumerations;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Helpers.Logger.Enumerations;
using Core.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.WarThunder.Tests.Objects
{
    /// <summary> See <see cref="Vehicle"/>.</summary>
    [TestClass]
    public class VehicleTests
    {
        #region Internal Methods

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

            using (var dataRepository = new DataRepositoryWarThunder(fileName, true, Assembly.Load(EAssemblies.WarThunderMappingAssembly), Presets.Logger))
            {
                var wingedBicycle = new Vehicle(dataRepository, "Winged Bycicle")
                {
                    Nation = new Nation(dataRepository, "Zimbabwe")
                };

                // act
                dataRepository.PersistNewObjects();

                var query = dataRepository.Query<IVehicle>();
                query.Count().Should().Be(1);

                var vehicle = query.First();

                // assert
                vehicle.IsEquivalentTo(wingedBicycle, 2);
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
            var isEquivalent = wingedBicycle.IsEquivalentTo(wingedBicycle, 2);

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
            var isEquivalent = wingedBicycle.IsEquivalentTo(trackedWheelchair, 2);

            // assert
            isEquivalent.Should().BeFalse();
        }

        #endregion Tests: IsEquivalentTo()
    }
}