using Core.DataBase.Enumerations.Logger;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Enumerations;
using Core.Helpers.Logger.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.Helpers
{
    /// <summary> Handles connections to and actions over an SQLite database. </summary>
    public class DataRepository : LoggerFluency, IDataRepository
    {
        #region Properties

        /// <summary> An instance of a logger. </summary>
        public IConfiguredLogger Logger { get { return _logger; } }

        /// <summary> Indicates whether the repository has been disposed of. </summary>
        public bool IsClosed { get; private set; }

        /// <summary> A session factory that provides units of work. </summary>
        public IConfiguredSessionFactory SessionFactory { get; }

        /// <summary>
        /// Transient objects cached in the repository and not yet persisted.
        /// Objects should not be added to the collection directly, instead they are added on their initialization.
        /// </summary>
        public IList<IPersistentObject> NewObjects { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new repository, as well as creates and configures a <see cref="ISessionFactory"/> for it. </summary>
        /// <param name="dataBaseFileName"> The name of an SQLite database file (without an extension). </param>
        /// <param name="overwriteExistingDataBase"> Indicates whether an existing database should be overwritten on creation of the <see cref="SessionFactory"/>. </param>
        /// <param name="assemblyWithMapping"> An assembly containing mapped classes. </param>
        /// <param name="logger"> An instance of a logger. </param>
        public DataRepository(string dataBaseFileName, bool overwriteExistingDataBase, Assembly assemblyWithMapping, IConfiguredLogger logger)
            : base(logger, EDataBaseLogCategory.DataRepository)
        {
            LogDebug
            (
                EDataBaseLogMessage.CreatingDataRepository.ResetFormattingPlaceholders().FormatFluently
                (
                    $"{dataBaseFileName}.{EFileExtension.SqLite3}",
                    overwriteExistingDataBase ? string.Empty : ECoreLogMessage.W_dont_SPC,
                    assemblyWithMapping
                )
            );

            SessionFactory = new ConfiguredSessionFactory($"{dataBaseFileName}.{EFileExtension.SqLite3}", overwriteExistingDataBase, assemblyWithMapping, logger);
            NewObjects = new List<IPersistentObject>();

            LogDebug(EDataBaseLogMessage.DataRepositoryCreated);
        }

        #endregion Constructors
        #region Methods: IDataRepository Members

        /// <summary> Read all instances of a specified persistent class from the database and caches them into a collection. </summary>
        /// <typeparam name="T"> The type of objects to look for. </typeparam>
        /// <returns></returns>
        public IEnumerable<T> Query<T>() where T : IPersistentObject
        {
            LogDebug(EDataBaseLogMessage.QueryingObjects.FormatFluently(typeof(T)));

            var cachedQuery = default(IEnumerable<T>);

            using (var session = SessionFactory.OpenSession())
            {
                cachedQuery = session
                    .Query<T>()
                    .ToList();
            }

            foreach (var instance in cachedQuery)
                instance.InitializeNonPersistentFields(this);

            LogDebug(EDataBaseLogMessage.QueryReturnedObjects.FormatFluently(cachedQuery.Count()));
            return cachedQuery;
        }

        /// <summary> Commits any changes to a specified object to the database. </summary>
        /// <param name="instance"> An object to create/update. </param>
        public void CommitChanges(IPersistentObject instance)
        {
            LogDebug(EDataBaseLogMessage.CommittingChangesTo.FormatFluently(instance.ToString()));

            using (var session = SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(instance);
                transaction.Commit();
            }

            LogDebug(EDataBaseLogMessage.ChangesCommitted);
        }

        /// <summary> Persists any transient objects cached in the repository. </summary>
        public void PersistNewObjects()
        {
            LogDebug(EDataBaseLogMessage.PersistingNewObjects.FormatFluently(NewObjects.Count()));

            foreach (var instance in NewObjects.ToList())
                instance.CommitChanges();

            LogDebug(EDataBaseLogMessage.AllNewObjectsPersisted);
        }

        #endregion Methods: IDataRepository Members
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
            LogDebug(ECoreLogMessage.PreparingToDisposeOf.FormatFluently(EDataBaseLogMessage.DataRepositoryFor_noFS.FormatFluently(SessionFactory?.DataBaseFileName ?? ECoreLogMessage.NULL)));

            if (IsClosed)
            {
                LogDebug(ECoreLogMessage.AlreadyDisposed);
                return;
            }

            if (disposing)
            {
                if (SessionFactory == null)
                {
                    LogDebug(ECoreLogMessage.IsNull_DisposalAborted.FormatFluently(ECoreLogMessage.W_TheSessionFactory));
                    return;
                }
                else
                {
                    LogDebug(ECoreLogMessage.Disposing);
                    SessionFactory.Dispose();
                }
            }

            IsClosed = true;
            LogDebug(ECoreLogMessage.SuccessfullyDisposed);
        }

        #endregion Methods: IDisposeable Members
    }
}