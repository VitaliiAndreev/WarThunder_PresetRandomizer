using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Core.DataBase.Tests.Objects
{
    /// <summary> See <see cref="PersistentObjectWithIdAndName"/>. </summary>
    [TestClass]
    public class PersistentObjectWithIdAndNameTests
    {
        #region Fake Classes

        private class MockPersistentObjectWithIdAndName : PersistentObjectWithIdAndName
        {
            #region Constructors

            public MockPersistentObjectWithIdAndName(IDataRepository dataRepository, string name)
                : this(dataRepository, Guid.NewGuid(), name)
            {
            }

            public MockPersistentObjectWithIdAndName(IDataRepository dataRepository, Guid id, string name)
                : base(dataRepository, id, name)
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
            var mockPersistentObject = new MockPersistentObjectWithIdAndName(Presets.MockDataRepository.Object, "Carramba!");

            // act
            var isEquivalent = mockPersistentObject.IsEquivalentTo(mockPersistentObject, true, 1);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_SameName_ShouldBeTrue()
        {
            // arrange
            var mockPersistentObjectA = new MockPersistentObjectWithIdAndName(Presets.MockDataRepository.Object, "Carramba!");
            var mockPersistentObjectB = new MockPersistentObjectWithIdAndName(Presets.MockDataRepository.Object, mockPersistentObjectA.Id, mockPersistentObjectA.Name);

            // act
            var isEquivalent = mockPersistentObjectA.IsEquivalentTo(mockPersistentObjectB, true, 1);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_DifferentName_ShouldBeFalse()
        {
            // arrange
            var mockPersistentObjectA = new MockPersistentObjectWithIdAndName(Presets.MockDataRepository.Object, "Carramba!");
            var mockPersistentObjectB = new MockPersistentObjectWithIdAndName(Presets.MockDataRepository.Object, "Bananza!");

            // act
            var isEquivalent = mockPersistentObjectA.IsEquivalentTo(mockPersistentObjectB, true, 1);

            // assert
            isEquivalent.Should().BeFalse();
        }

        #endregion Tests: IsEquivalentTo()
    }
}
