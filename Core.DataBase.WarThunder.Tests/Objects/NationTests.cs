using Core.DataBase.Tests.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
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
        private readonly IEnumerable<string> _ignoredPropertyNames;

        #region Internal Methods

        public NationTests()
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
            Presets.Logger.LogInfo(CoreLogCategory.UnitTests, CoreLogMessage.CleanUpAfterUnitTestStartsHere);
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
                // act
                var britain = new Nation(dataRepository, EReference.NationsFromEnumeration[ENation.GreatBritain]);

                dataRepository.PersistNewObjects();

                var query = dataRepository.Query<INation>();
                query.Count().Should().Be(1);

                var nation = query.First();

                // assert
                nation.IsEquivalentTo(britain, 1, _ignoredPropertyNames).Should().BeTrue();
            }
        }

        #endregion Tests: CommitChanges() and Query()
        #region Tests: IsEquivalentTo()

        [TestMethod]
        public void IsEquivalentTo_Self_ShouldBeTrue()
        {
            // arrange
            var britain = new Nation(Presets.MockDataRepository.Object, EReference.NationsFromEnumeration[ENation.GreatBritain]);
            britain.Branches = new List<IBranch> { new Branch(Presets.MockDataRepository.Object, EReference.CategoriesFromEnumeration[EVehicleCategory.Army], britain) };

            // act
            var isEquivalent = britain.IsEquivalentTo(britain, 2, _ignoredPropertyNames);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_SameBranches_ShouldBeTrue()
        {
            // arrange
            var britain = new Nation(Presets.MockDataRepository.Object, EReference.NationsFromEnumeration[ENation.GreatBritain]);
            britain.Branches = new List<IBranch> { new Branch(Presets.MockDataRepository.Object, EReference.CategoriesFromEnumeration[EVehicleCategory.Army], britain) };

            var britainClone = new Nation(Presets.MockDataRepository.Object, britain.Id, britain.GaijinId)
            { Branches = new List<IBranch> { britain.Branches.First() } };

            // act
            var isEquivalent = britain.IsEquivalentTo(britainClone, 2, _ignoredPropertyNames);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_DifferentBranches_ShouldBeFalse()
        {
            // arrange
            var britain = new Nation(Presets.MockDataRepository.Object, EReference.NationsFromEnumeration[ENation.GreatBritain]);
            britain.Branches = new List<IBranch> { new Branch(Presets.MockDataRepository.Object, EReference.CategoriesFromEnumeration[EVehicleCategory.Army], britain) };

            var britainCloneFlawed = new Nation(Presets.MockDataRepository.Object, britain.Id, britain.GaijinId);
            britainCloneFlawed.Branches = new List<IBranch> { new Branch(Presets.MockDataRepository.Object, EReference.CategoriesFromEnumeration[EVehicleCategory.Aviation], britainCloneFlawed) };

            // act
            var isEquivalent = britain.IsEquivalentTo(britainCloneFlawed, 2, _ignoredPropertyNames);

            // assert
            isEquivalent.Should().BeFalse();
        }

        #endregion Tests: IsEquivalentTo()
    }
}