﻿using Core.DataBase.Helpers;
using Core.DataBase.Tests.Enumerations;
using Core.DataBase.Tests.Mapping.OneClass.Id.Mapping;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.Tests.Helpers
{
    /// <summary> See <see cref="DataRepository"/>. </summary>
    [TestClass]
    public class DataRepositoryTests
    {
        #region Internal Methods

        public override string ToString() => nameof(DataRepositoryTests);

        [TestCleanup]
        public void CleanUp() => Presets.CleanUp();

        #endregion Internal Methods
        #region Tests: CommitChanges() and Query()

        [TestMethod]
        public void CommitChanges_Query_ReturnsObject()
        {
            // arrange
            var fileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}()";

            using (var dataRepository = new DataRepository(fileName, true, Assembly.Load(EAssemblies.AssemblyWithMappingBase), Presets.Logger))
            {
                // act
                var fakeObject = new PersistentObjectFakeWithId(Guid.NewGuid());
                fakeObject.InitializeNonPersistentFields(dataRepository);

                dataRepository.NewObjects.Add(fakeObject);
                dataRepository.PersistNewObjects();

                var query = dataRepository.Query<PersistentObjectFakeWithId>();

                // assert
                query.Count().Should().Be(1);
                query.First().Id.Should().Be(id);
            }
        }

        #endregion Tests: CommitChanges() and Query()
    }
}
