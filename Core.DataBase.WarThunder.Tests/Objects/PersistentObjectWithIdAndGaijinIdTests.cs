using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects;
using Core.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.DataBase.WarThunder.Tests.Objects
{
    /// <summary> See <see cref="PersistentObjectWithIdAndGaijinId"/>. </summary>
    [TestClass]
    public class PersistentObjectWithIdAndGaijinIdTests
    {
        #region Fake Classes

        private class MockPersistentObjectWithIdAndGaijinId : PersistentObjectWithIdAndGaijinId
        {
            #region Constructors

            public MockPersistentObjectWithIdAndGaijinId(IDataRepository dataRepository, string gaijinId)
                : this(dataRepository, -1L, gaijinId)
            {
            }

            public MockPersistentObjectWithIdAndGaijinId(IDataRepository dataRepository, long id, string gaijinId)
                : base(dataRepository, id, gaijinId)
            {
            }

            #endregion Constructors
        }

        #endregion Fake Classes
        #region Tests: IsEquivalentTo()

        [TestMethod]
        public void IsEquivalentTo_Self_ShouldBeTrue()
        {
            // arrange
            var mockPersistentObject = new MockPersistentObjectWithIdAndGaijinId(Presets.MockDataRepository.Object, "Carramba!");

            // act
            var isEquivalent = mockPersistentObject.IsEquivalentTo(mockPersistentObject, 2);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_SameName_ShouldBeTrue()
        {
            // arrange
            var mockPersistentObjectA = new MockPersistentObjectWithIdAndGaijinId(Presets.MockDataRepository.Object, "Carramba!");
            var mockPersistentObjectB = new MockPersistentObjectWithIdAndGaijinId(Presets.MockDataRepository.Object, mockPersistentObjectA.Id, mockPersistentObjectA.GaijinId);

            // act
            var isEquivalent = mockPersistentObjectA.IsEquivalentTo(mockPersistentObjectB, 2);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_DifferentName_ShouldBeFalse()
        {
            // arrange
            var mockPersistentObjectA = new MockPersistentObjectWithIdAndGaijinId(Presets.MockDataRepository.Object, "Carramba!");
            var mockPersistentObjectB = new MockPersistentObjectWithIdAndGaijinId(Presets.MockDataRepository.Object, "Bananza!");

            // act
            var isEquivalent = mockPersistentObjectA.IsEquivalentTo(mockPersistentObjectB, 2);

            // assert
            isEquivalent.Should().BeFalse();
        }

        #endregion Tests: IsEquivalentTo()
    }
}