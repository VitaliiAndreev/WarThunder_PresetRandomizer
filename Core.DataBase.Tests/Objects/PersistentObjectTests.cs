using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using Core.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.DataBase.Tests.Objects
{
    /// <summary> See <see cref="PersistentObject"/>. </summary>
    [TestClass]
    public class PersistentObjectTests
    {
        #region Fake Classes

        private class MockPersistentObject : PersistentObject
        {
            #region Constructors

            public MockPersistentObject(IDataRepository dataRepository)
            {
                SetLogCategory();
                _dataRepository = dataRepository;
                _dataRepository.NewObjects.Add(this);

                LogCreation();
            }

            #endregion Constructors
        }

        #endregion Fake Classes
        #region Tests: IsEquivalentTo()

        [TestMethod]
        public void IsEquivalentTo_Self_ShouldBeTrue()
        {
            // arrange
            var mockPersistentObject = new MockPersistentObject(Presets.MockDataRepository.Object);

            // act
            var isEquivalent = mockPersistentObject.IsEquivalentTo(mockPersistentObject, 2);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_AnotherInstance_ShouldBeTrue()
        {
            // arrange
            var mockPersistentObjectA = new MockPersistentObject(Presets.MockDataRepository.Object);
            var mockPersistentObjectB = new MockPersistentObject(Presets.MockDataRepository.Object);

            // act
            var isEquivalent = mockPersistentObjectA.IsEquivalentTo(mockPersistentObjectB, 2);

            // assert
            isEquivalent.Should().BeTrue();
        }

        #endregion Tests: IsEquivalentTo()
    }
}