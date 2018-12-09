using Core.DataBase.Helpers;
using Core.DataBase.Tests.Enumerations;
using Core.DataBase.Tests.Mapping.OneClass.Id.Mapping;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            using (var repository = new DataRepository(fileName, false, Assembly.Load(EAssemblies.AssemblyWithMappingBase), Presets.Logger))
            {
                var id = $"{fileName}/0";

                // act
                var fakeObject = new PersistentObjectFakeWithId(id);
                fakeObject.InitializeNonPersistentFields(repository);

                repository.NewObjects.Add(fakeObject);
                repository.PersistNewObjects();

                var query = repository.Query<PersistentObjectFakeWithId>();

                // assert
                query.Count().Should().Be(1);
                query.First().Id.Should().Be(id);
            }
        }

        #endregion Tests: CommitChanges() and Query()
    }
}
