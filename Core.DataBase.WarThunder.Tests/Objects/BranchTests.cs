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
    /// <summary> See <see cref="Branch"/>. </summary>
    [TestClass]
    public class BranchTests
    {
        private readonly IEnumerable<string> _ignoredPropertyNames;

        #region Internal Methods

        public BranchTests()
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

            using (var dataRepository = new DataRepositorySqliteWarThunder(fileName, true, Assembly.Load(EAssembly.WarThunderMappingAssembly), true, Presets.Logger))
            {
                var britain = new Nation(dataRepository, EReference.NationsFromEnumeration[ENation.GreatBritain]);
                var britishArmy = new Branch(dataRepository, EReference.CategoriesFromEnumeration[EVehicleCategory.Army], britain);
                britain.Branches = new List<IBranch> { britishArmy };

                // act
                dataRepository.PersistNewObjects();

                var query = dataRepository.Query<IBranch>();
                query.Count().Should().Be(1);

                var branch = query.First();

                // assert
                branch.IsEquivalentTo(britishArmy, 2, _ignoredPropertyNames).Should().BeTrue();
            }
        }

        #endregion Tests: CommitChanges() and Query()
        #region Tests: IsEquivalentTo()

        [TestMethod]
        public void IsEquivalentTo_Self_ShouldBeTrue()
        {
            // arrange
            var britain = new Nation(Presets.MockDataRepository.Object, EReference.NationsFromEnumeration[ENation.GreatBritain]);
            var britishArmy = new Branch(Presets.MockDataRepository.Object, EReference.CategoriesFromEnumeration[EVehicleCategory.Army], britain);
            britain.Branches = new List<IBranch> { britishArmy };

            // act
            var isEquivalent = britishArmy.IsEquivalentTo(britishArmy, 2, _ignoredPropertyNames);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_SameNation_ShouldBeTrue()
        {
            // arrange
            var britain = new Nation(Presets.MockDataRepository.Object, EReference.NationsFromEnumeration[ENation.GreatBritain]);

            var britishArmy = new Branch(Presets.MockDataRepository.Object, EReference.CategoriesFromEnumeration[EVehicleCategory.Army], britain);
            var britishArmyClone = new Branch(Presets.MockDataRepository.Object, britishArmy.Id, britishArmy.GaijinId, britishArmy.Nation);
            britain.Branches = new List<IBranch> { britishArmy, britishArmyClone };

            // act
            var isEquivalent = britishArmy.IsEquivalentTo(britishArmyClone, 2, _ignoredPropertyNames);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_DifferentNations_ShouldBeFalse()
        {
            // arrange
            var britain = new Nation(Presets.MockDataRepository.Object, EReference.NationsFromEnumeration[ENation.GreatBritain]);
            var britishArmy = new Branch(Presets.MockDataRepository.Object, EReference.CategoriesFromEnumeration[EVehicleCategory.Army], britain);
            britain.Branches = new List<IBranch> { britishArmy };

            var germany = new Nation(Presets.MockDataRepository.Object, EReference.NationsFromEnumeration[ENation.Germany]);
            var britishArmyAlignedWithGermany = new Branch(Presets.MockDataRepository.Object, britishArmy.GaijinId, germany);
            germany.Branches = new List<IBranch> { britishArmyAlignedWithGermany };

            // act
            var isEquivalent = britishArmy.IsEquivalentTo(britishArmyAlignedWithGermany, 2, _ignoredPropertyNames);

            // assert
            isEquivalent.Should().BeFalse();
        }

        #endregion Tests: IsEquivalentTo()
    }
}