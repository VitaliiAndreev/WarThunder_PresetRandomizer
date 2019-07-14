using Core.DataBase.Tests.Enumerations;
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
    /// <summary> See <see cref="Nation"/>. </summary>
    [TestClass]
    public class NationTests
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
                // act
                var zimbabwe = new Nation(dataRepository, "Zimbabwe");

                dataRepository.PersistNewObjects();

                var query = dataRepository.Query<INation>();
                query.Count().Should().Be(1);

                var nation = query.First();

                // assert
                nation.IsEquivalentTo(zimbabwe, 1).Should().BeTrue();
            }
        }

        #endregion Tests: CommitChanges() and Query()
        #region Tests: IsEquivalentTo()

        [TestMethod]
        public void IsEquivalentTo_Self_ShouldBeTrue()
        {
            // arrange
            var zimbabwe = new Nation(Presets.MockDataRepository.Object, "Zimbabwe");
            zimbabwe.Branches = new List<IBranch> { new Branch(Presets.MockDataRepository.Object, "BycicleCorps", zimbabwe) };

            // act
            var isEquivalent = zimbabwe.IsEquivalentTo(zimbabwe, 2);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_SameBranches_ShouldBeTrue()
        {
            // arrange
            var zimbabwe = new Nation(Presets.MockDataRepository.Object, "Zimbabwe");
            zimbabwe.Branches = new List<IBranch> { new Branch(Presets.MockDataRepository.Object, "BycicleCorps", zimbabwe) };

            var zimbabweClone = new Nation(Presets.MockDataRepository.Object, zimbabwe.Id, zimbabwe.GaijinId)
            { Branches = new List<IBranch> { zimbabwe.Branches.First() } };

            // act
            var isEquivalent = zimbabwe.IsEquivalentTo(zimbabweClone, 2);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_DifferentBranches_ShouldBeFalse()
        {
            // arrange
            var zimbabwe = new Nation(Presets.MockDataRepository.Object, "Zimbabwe");
            zimbabwe.Branches = new List<IBranch> { new Branch(Presets.MockDataRepository.Object, "BycicleCorps", zimbabwe) };

            var zimbabweCloneFlawed = new Nation(Presets.MockDataRepository.Object, zimbabwe.Id, zimbabwe.GaijinId);
            zimbabweCloneFlawed.Branches = new List<IBranch> { new Branch(Presets.MockDataRepository.Object, "SledCorps", zimbabweCloneFlawed) };

            // act
            var isEquivalent = zimbabwe.IsEquivalentTo(zimbabweCloneFlawed, 2);

            // assert
            isEquivalent.Should().BeFalse();
        }

        #endregion Tests: IsEquivalentTo()
    }
}