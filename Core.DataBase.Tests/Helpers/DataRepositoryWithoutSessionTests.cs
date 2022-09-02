using Core.DataBase.Helpers;
using Core.DataBase.Tests.Enumerations;
using Core.DataBase.Tests.Mapping.OneClass.Id.Mapping;
using Core.Enumerations.Logger;
using Core.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.Tests.Helpers
{
    /// <summary> See <see cref="DataRepositorySqlite"/>. </summary>
    [TestClass]
    public class DataRepositoryWithoutSessionTests
    {
        #region Internal Methods

        public override string ToString() => nameof(DataRepositoryWithoutSessionTests);

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

            using (var dataRepository = new DataRepositorySqlite(fileName, true, Assembly.Load(EAssembly.AssemblyWithMappingBase), false, Presets.Logger))
            {
                var id = -1L;

                var fakeObject = new PersistentObjectFakeWithId(id);
                fakeObject.InitializeNonPersistentFields(dataRepository);

                dataRepository.AddToNewObjects(fakeObject);

                // act
                dataRepository.PersistNewObjects();

                var query = dataRepository.Query<PersistentObjectFakeWithId>();

                // assert
                query.Count().Should().Be(1);
                query.First().IsEquivalentTo(fakeObject, 1).Should().BeTrue();
            }
        }

        #endregion Tests: CommitChanges() and Query()
    }
}