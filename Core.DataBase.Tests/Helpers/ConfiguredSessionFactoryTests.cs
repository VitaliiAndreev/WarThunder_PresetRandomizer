using Core.DataBase.Helpers;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Tests;
using Core.DataBase.Tests.Enumerations;
using Core.DataBase.Tests.Mapping.OneClass.IdAndName.Mapping;
using Core.Enumerations;
using FluentAssertions;
using FluentNHibernate.Cfg;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Exceptions;
using System;
using System.IO;
using System.Reflection;

namespace Core.Tests.Helpers.DataBase
{
    /// <summary> See <see cref="ConfiguredSessionFactory"/>. </summary>
    [TestClass]
    public class ConfiguredSessionFactoryTests
    {
        #region Internal Methods

        public override string ToString() => nameof(ConfiguredSessionFactoryTests);

        [TestCleanup]
        public void CleanUp() => Presets.CleanUp();

        #endregion Internal Methods
        #region Tests: ConfiguredSessionFactory()

        [TestMethod]
        [Description("If the assembly provided when creating a factory has no mapping, a FluentConfigurationException is thrown.")]
        public void ConfiguredSessionFactory_NoMapping_ShouldThrow()
        {
            // arrange
            var fileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}().{EFileExtension.SqLite3}";
            
            // act
            Action buildDataBase = () =>
            {
                using (var factory = new ConfiguredSessionFactory(fileName, false, Assembly.Load(EAssemblies.AssemblyWithNoMapping), Presets.Logger)) { }
            };

            // assert
            buildDataBase.Should().Throw<FluentConfigurationException>();
        }

        [TestMethod]
        [Description("If there is no database when it shouldn't be overwritten, a new file is to be created.")]
        public void ConfiguredSessionFactory_DontOverwriteExistingDataBase_DataBaseDoesntExist_ShouldCreateNewFile()
        {
            // arrange
            var assemblyName = EAssemblies.AssemblyWithMappingBase;
            var fileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}().{EFileExtension.SqLite3}";

            IConfiguredSessionFactory createSessionFactory() =>
                new ConfiguredSessionFactory(fileName, false, Assembly.Load(assemblyName), Presets.Logger);

            bool fileExists(string file) =>
                File.Exists(file);

            if (fileExists(fileName))
                File.Delete(fileName);

            fileExists(fileName).Should().BeFalse();

            // act
            using (var factory = createSessionFactory()) { }

            // assert
            fileExists(fileName).Should().BeTrue();
        }

        [TestMethod]
        [Description("If an existing database is not to be overwritten while the schema doesn't match, a GenericADOException is thrown.")]
        public void ConfiguredSessionFactory_DontOverwriteExistingDataBase_SchemaMismatches_ShouldThrow()
        {
            // arrange
            var firstAssemblyName = EAssemblies.AssemblyWithMappingBase;
            var secondAssemblyName = EAssemblies.AssemblyWithMappingAltered;
            var fileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}().{EFileExtension.SqLite3}";

            IConfiguredSessionFactory createSessionFactory(string assemblyName) =>
                new ConfiguredSessionFactory(fileName, false, Assembly.Load(assemblyName), Presets.Logger);

            using (var factory = createSessionFactory(firstAssemblyName)) { }

            // act
            Action buildDataBaseWithDifferentMappingAndInsertNewObject = () =>
            {
                using (var factory = createSessionFactory(secondAssemblyName))
                using (var session = factory.OpenSession())
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(new PersistentObjectFakeWithIdAndName("name"));
                    transaction.Commit();
                }
            };

            // assert
            buildDataBaseWithDifferentMappingAndInsertNewObject.Should().Throw<GenericADOException>();
        }

        [TestMethod]
        [Description("If an existing database is not to be overwritten while the schema matches, a session factory should be created correctly.")]
        public void ConfiguredSessionFactory_DontOverwriteExistingDataBase_SchemaMatches_ShouldNotWriteOverFile()
        {
            // arrange
            var assemblyName = EAssemblies.AssemblyWithMappingBase;
            var fileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}().{EFileExtension.SqLite3}";

            IConfiguredSessionFactory createSessionFactory() =>
                new ConfiguredSessionFactory(fileName, false, Assembly.Load(assemblyName), Presets.Logger);

            using (var factory = createSessionFactory()) { }
            var expectedCreationTime = File.GetLastWriteTime(fileName);

            // act
            using (var factory = createSessionFactory()) { }

            // assert
            File.GetLastWriteTime(fileName).Should().Be(expectedCreationTime);
        }

        [TestMethod]
        [Description("If there is no database when it should be overwritten, a new file is to be created.")]
        public void ConfiguredSessionFactory_OverwriteExistingDataBase_DataBaseDoesntExist_ShouldCreateNewFile()
        {
            // arrange
            var assemblyName = EAssemblies.AssemblyWithMappingBase;
            var fileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}().{EFileExtension.SqLite3}";

            IConfiguredSessionFactory createSessionFactory() =>
                new ConfiguredSessionFactory(fileName, true, Assembly.Load(assemblyName), Presets.Logger);

            bool fileExists(string file) =>
                File.Exists(file);

            if (fileExists(fileName))
                File.Delete(fileName);

            fileExists(fileName).Should().BeFalse();

            // act
            using (var factory = createSessionFactory()) { }

            // assert
            fileExists(fileName).Should().BeTrue();
        }

        [TestMethod]
        [Description("If an existing database is to be overwritten, a fresh schema is to be written over the old one.")]
        public void ConfiguredSessionFactory_OverwriteExistingDataBase_DataBaseExists_ShouldWriteOverFile()
        {
            // arrange
            var assemblyName = EAssemblies.AssemblyWithMappingBase;
            var fileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}().{EFileExtension.SqLite3}";

            IConfiguredSessionFactory createSessionFactory() =>
                new ConfiguredSessionFactory(fileName, true, Assembly.Load(assemblyName), Presets.Logger);

            using (var factory = createSessionFactory()) { }
            var firstCreationTime = File.GetLastWriteTime(fileName);

            // act
            using (var factory = createSessionFactory()) { }

            // assert
            File.GetLastWriteTime(fileName).Should().BeAfter(firstCreationTime);
        }

        #endregion Tests: ConfiguredSessionFactory()
    }
}
