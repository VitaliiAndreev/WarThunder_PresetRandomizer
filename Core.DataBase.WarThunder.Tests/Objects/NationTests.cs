using Core.DataBase.Helpers;
using Core.DataBase.Tests;
using Core.DataBase.Tests.Enumerations;
using Core.Objects;
using Core.Objects.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.WarThunder.Tests.Objects
{
    /// <summary> See <see cref="Nation"/>. </summary>
    [TestClass]
    public class NationTests
    {
        #region Internal Methods

        public override string ToString() => nameof(NationTests);

        [TestCleanup]
        public void CleanUp() => Presets.CleanUp();

        #endregion Internal Methods
        #region Tests: CommitChanges() and Query()

        [TestMethod]
        public void CommitChanges_Query_ReturnsObject()
        {
            // arrange
            var fileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}()";

            using (var dataRepository = new DataRepository(fileName, false, Assembly.Load(EAssemblies.WarThunderMappingAssembly), Presets.Logger))
            {
                // act
                var zimbabwe = new Nation(dataRepository, "Zimbabwe");

                dataRepository.PersistNewObjects();

                var query = dataRepository.Query<INation>();
                query.Count().Should().Be(1);

                var nation = query.First();

                // assert
                zimbabwe.Id.Should().Be("0");
                zimbabwe.Name.Should().Be("Zimbabwe");
            }
        }

        #endregion Tests: CommitChanges() and Query()
    }
}
