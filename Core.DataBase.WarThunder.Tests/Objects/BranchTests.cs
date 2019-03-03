using Core.DataBase.Helpers;
using Core.DataBase.Tests;
using Core.DataBase.Tests.Enumerations;
using Core.Objects;
using Core.Objects.Interfaces;
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

            using (var dataRepository = new DataRepository(fileName, true, Assembly.Load(EAssemblies.WarThunderMappingAssembly), Presets.Logger))
            {
                // act
                var zimbabwe = new Nation(dataRepository, "Zimbabwe");
                var bycicleCorps = new Branch(dataRepository, "Bycicle Corps", zimbabwe);
                zimbabwe.Branches = new List<IBranch> { bycicleCorps };

                dataRepository.PersistNewObjects();

                var query = dataRepository.Query<IBranch>();
                query.Count().Should().Be(1);

                var branch = query.First();

                // assert
                branch.IsEquivalentTo(bycicleCorps, true, 2);
            }
        }

        #endregion Tests: CommitChanges() and Query()
    }
}
