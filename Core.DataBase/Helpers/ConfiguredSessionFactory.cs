using Core.DataBase.Enumerations.Logger;
using Core.DataBase.Helpers.Interfaces;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using NHibernate.Tool.hbm2ddl;
using System;
using System.IO;
using System.Reflection;

namespace Core.DataBase.Helpers
{
    /// <summary> A wrapper around <see cref="ISessionFactory"/> that applies configuration to it. </summary>
    public class ConfiguredSessionFactory : LoggerFluency, IConfiguredSessionFactory
    {
        #region Fields

        /// <summary> Indicates whether the factory has been disposed of. </summary>
        private bool _disposed;

        /// <summary> A session factory that provides units of work. </summary>
        private readonly ISessionFactory _sessionFactory;

        /// <summary> Indicates whether an existing database should be overwritten on creation of the <see cref="_sessionFactory"/>. </summary>
        private readonly bool _overwriteExistingDataBase;

        /// <summary> The assembly to read mapping from. </summary>
        private readonly Assembly _assemblyWithMapping;

        #endregion Fields
        #region Properties

        /// <summary> The name of the SQLite database file (with an extension). </summary>
        public string DataBaseFileName { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates and configures a new session factory. </summary>
        /// <param name="dataBaseFileName"> The name of an SQLite database file (with an extension). </param>
        /// <param name="overwriteExistingDataBase"> Indicates whether an existing database should be overwritten on creation of the <see cref="ISessionFactory"/>. </param>
        /// <param name="assemblyWithMapping"> An assembly containing mapped classes. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public ConfiguredSessionFactory(string dataBaseFileName, bool overwriteExistingDataBase, Assembly assemblyWithMapping, params IConfiguredLogger[] loggers)
            : base(nameof(ConfiguredSessionFactory), loggers)
        {
            DataBaseFileName = dataBaseFileName;
            _overwriteExistingDataBase = overwriteExistingDataBase;
            _assemblyWithMapping = assemblyWithMapping;

            _sessionFactory = CreateSessionFactory();
        }

        #endregion Constructors
        #region Methods: Creation and Configuration

        /// <summary> Creates and configures the session factory. </summary>
        /// <returns></returns>
        private ISessionFactory CreateSessionFactory()
        {
            var factory = default(ISessionFactory);

            LogDebug
            (
                EDatabaseLogMessage.CreatingSessionFactory.ResetFormattingPlaceholders().Format
                (
                    DataBaseFileName,
                    _overwriteExistingDataBase ? string.Empty : $"don't ",
                    _assemblyWithMapping
                )
            );

            try
            {
                factory = Fluently
                    .Configure()
                    .Database(SQLiteConfiguration.Standard.UsingFile(DataBaseFileName))
                    .ExposeConfiguration(WriteMappingFromAttributes)
                    .ExposeConfiguration(CreateSchema)
                    .BuildSessionFactory()
                ;
            }
            catch (Exception exception)
            {
                LogFatal(EDatabaseLogMessage.ErrorCreaingSessionFactory, exception);
                throw;
            }

            LogDebug(EDatabaseLogMessage.SessionFactoryCreated);
            return factory;
        }

        /// <summary> Serializes the <see cref="_assemblyWithMapping"/>, reads ORM mapping and writes it to a provided configuration. </summary>
        /// <param name="configuration"> A configuration to apply ORM mapping information to. </param>
        private void WriteMappingFromAttributes(Configuration configuration)
        {
            HbmSerializer.Default.Validate = true;
            LogDebug(EDatabaseLogMessage.WritingMappingAttributes);

            try
            {
                configuration.AddInputStream(HbmSerializer.Default.Serialize(_assemblyWithMapping));
            }
            catch (Exception exception)
            {
                LogError("An error has occurred.", exception);
                throw;
            }

            LogDebug(EDatabaseLogMessage.MappingAttributesWritten);
        }

        /// <summary>
        /// Creates a schema for an SQLite database from a provided configuration.
        /// ORM mapping has to be added to the configuration prior.
        /// </summary>
        /// <param name="configuration"> A configuration to build a schema with. </param>
        private void CreateSchema(Configuration configuration)
        {
            LogDebug(EDatabaseLogMessage.CreatingSchema);

            if (File.Exists(DataBaseFileName))
            {
                LogDebug(EDatabaseLogMessage.DataBaseFileFound);

                if (!_overwriteExistingDataBase)
                {
                    LogDebug(EDatabaseLogMessage.ShouldNotOverwrite);
                    return;
                }

                LogDebug(EDatabaseLogMessage.OverwritingSchema);
            }
            else
            {
                LogDebug(EDatabaseLogMessage.CreatingFileWithSchema);
            }

            new SchemaExport(configuration).Create(false, true);
            LogDebug($"Database schema created.");
        }

        #endregion Methods: Creation and Configuration
        #region Methods: IConfiguredSessionFactory Members

        /// <summary> Creates a database connection and opens a session on it. </summary>
        /// <returns></returns>
        public ISession OpenSession() =>
            _sessionFactory.OpenSession();

        #endregion Methods: IConfiguredSessionFactory Members
        #region Methods: IDisposeable Members

        /// <summary> Releases unmanaged resources. </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary> Releases unmanaged resources. </summary>
        /// <param name="disposing"> Indicates whether this method is being called from <see cref="Dispose"/>. </param>
        protected virtual void Dispose(bool disposing)
        {
            LogDebug($"Preparing to dispose of the session factory for \"{DataBaseFileName}\".");

            if (_disposed)
            {
                LogDebug("Already disposed of.");
                return;
            }

            if (disposing)
            {
                if (_sessionFactory == null)
                {
                    LogDebug("The session factory is NULL. Disposal aborted.");
                    return;
                }

                if (_sessionFactory.IsClosed)
                {
                    LogDebug("Already closed.");
                }
                else
                {
                    LogDebug("Closing.");
                    _sessionFactory.Close();
                }

                LogDebug("Disposing.");
                _sessionFactory.Dispose();
            }

            _disposed = true;
            LogDebug("Successfully disposed of.");
        }

        #endregion Methods: IDisposeable Members
    }
}