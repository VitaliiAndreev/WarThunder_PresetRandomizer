using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using Core.DataBase.Objects.Interfaces;
using Core.Extensions;
using Core.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.Tests.Extensions
{
    /// <summary> See <see cref="IEnumerableExtensions"/>. </summary>
    [TestClass]
    public class IEnumerableExtensionsTests
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
        public void IsEquivalentTo_SameCount_ShouldBeTrue()
        {
            // arrange
            var collectionA = new List<IPersistentObject> { new MockPersistentObject(Presets.MockDataRepository.Object), new MockPersistentObject(Presets.MockDataRepository.Object) };
            var collectionB = new List<IPersistentObject> { collectionA.First(), collectionA.Last() };

            // act
            var isEquivalent = collectionA.IsEquivalentTo(collectionB, 1);

            // assert
            isEquivalent.Should().BeTrue();
        }

        [TestMethod]
        public void IsEquivalentTo_DifferentCount_ShouldBeFalse()
        {
            // arrange
            var collectionA = new List<IPersistentObject> { new MockPersistentObject(Presets.MockDataRepository.Object), new MockPersistentObject(Presets.MockDataRepository.Object) };
            var collectionB = new List<IPersistentObject> { collectionA.First() };

            // act
            var isEquivalent = collectionA.IsEquivalentTo(collectionB, 1);

            // assert
            isEquivalent.Should().BeFalse();
        }

        #endregion Tests: IsEquivalentTo()
    }
}