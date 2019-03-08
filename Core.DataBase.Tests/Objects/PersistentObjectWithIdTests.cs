using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Core.DataBase.Tests.Objects
{
    /// <summary> See <see cref="PersistentObjectWithId"/>. </summary>
    [TestClass]
    public class PersistentObjectWithIdTests
    {
        #region Fake Classes

        private class MockPersistentObjectWithId : PersistentObjectWithId
        {
            #region Constructors

            public MockPersistentObjectWithId(IDataRepository dataRepository)
                : this(dataRepository, Guid.NewGuid())
            {
            }

            public MockPersistentObjectWithId(IDataRepository dataRepository, Guid id)
                : base(dataRepository, id)
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
            var mockPersistentObject = new MockPersistentObjectWithId(Presets.MockDataRepository.Object);

            // act
            var isEquivalent = mockPersistentObject.IsEquivalentTo(mockPersistentObject, true, 1);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_SameId_ShouldBeTrue()
        {
            // arrange
            var mockPersistentObjectA = new MockPersistentObjectWithId(Presets.MockDataRepository.Object);
            var mockPersistentObjectB = new MockPersistentObjectWithId(Presets.MockDataRepository.Object, mockPersistentObjectA.Id);

            // act
            var isEquivalent = mockPersistentObjectA.IsEquivalentTo(mockPersistentObjectB, true, 1);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_DifferentId_ShouldBeFalse()
        {
            // arrange
            var mockPersistentObjectA = new MockPersistentObjectWithId(Presets.MockDataRepository.Object);
            var mockPersistentObjectB = new MockPersistentObjectWithId(Presets.MockDataRepository.Object);

            // act
            var isEquivalent = mockPersistentObjectA.IsEquivalentTo(mockPersistentObjectB, true, 1);

            // assert
            isEquivalent.Should().BeFalse();
        }

        #endregion Tests: IsEquivalentTo()
    }
}
