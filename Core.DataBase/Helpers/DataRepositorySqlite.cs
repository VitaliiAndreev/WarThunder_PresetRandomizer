using Core.DataBase.Enumerations.Logger;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.Helpers
{
    /// <summary> Handles connections to and actions over an SQLite database. </summary>
    public class DataRepositorySqlite : LoggerFluency, IDataRepository
    {
        #region Fields

        private readonly IList<IPersistentObject> _newObjects;

        private readonly ISession _session;

        protected readonly object _lock;

        #endregion Fields
        #region Properties

        /// <summary> Instances of loggers. </summary>
        public IEnumerable<IConfiguredLogger> Loggers { get { return _loggers; } }

        public object TransactionalLock { get; } 

        /// <summary> Indicates whether the repository has been disposed of. </summary>
        public bool IsClosed { get; private set; }

        /// <summary> A session factory that provides units of work. </summary>
        public IConfiguredSessionFactory SessionFactory { get; }

        /// <summary> Transient objects cached in the repository and not yet persisted. </summary>
        public virtual IEnumerable<IPersistentObject> NewObjects { get { lock(_lock) return _newObjects.ToList(); } }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new repository, as well as creates and configures a <see cref="ISessionFactory"/> for it. </summary>
        /// <param name="dataBaseFileName"> The name of an SQLite database file (without an extension). </param>
        /// <param name="overwriteExistingDataBase"> Indicates whether an existing database should be overwritten on creation of the <see cref="SessionFactory"/>. </param>
        /// <param name="assemblyWithMapping"> An assembly containing mapped classes. </param>
        /// <param name="singleSession"> Whether to use a create one session at the start and use throughout the lifecycle of the repository. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public DataRepositorySqlite(string dataBaseFileName, bool overwriteExistingDataBase, Assembly assemblyWithMapping, bool singleSession, params IConfiguredLogger[] loggers)
            : base(nameof(DataRepositorySqlite), loggers)
        {
            LogDebug
            (
                EDatabaseLogMessage.CreatingDataRepository.ResetFormattingPlaceholders().Format
                (
                    $"{dataBaseFileName}.{FileExtension.SqLite3}",
                    overwriteExistingDataBase ? string.Empty : $"don't ",
                    assemblyWithMapping
                )
            );

            _lock = new object();
            TransactionalLock = new object();

            SessionFactory = new ConfiguredSessionFactory($"{dataBaseFileName}.{FileExtension.SqLite3}", overwriteExistingDataBase, assemblyWithMapping, loggers);
            _newObjects = new List<IPersistentObject>();

            if (singleSession)
                _session = SessionFactory.OpenSession();

            LogDebug(EDatabaseLogMessage.DataRepositoryCreated);
        }

        #endregion Constructors
        #region Methods: IDataRepository Members

        public virtual IEnumerable<T> GetNewObjects<T>() where T : IPersistentObject
        {
            lock (_lock)
            {
                return _newObjects.OfType<T>().ToList();
            }
        }

        public virtual void AddToNewObjects<T>(T newObject) where T : IPersistentObject
        {
            lock (_lock)
            {
                _newObjects.Add(newObject);
            }
        }

        /// <summary> Reads instances (filtered if needed) of a specified persistent class from the database and caches them into a collection. </summary>
        /// <typeparam name="T"> The type of objects to look for. </typeparam>
        /// <param name="session"> The session in whose scope to query. </param>
        /// <param name="filter"> The filter by which to query objects from the database. </param>
        /// <returns></returns>
        protected IEnumerable<T> Query<T>(ISession session, Func<IQueryable<T>, IQueryable<T>> filter = null) where T : IPersistentObject
        {
            LogDebug((filter is null ? EDatabaseLogMessage.QueryingAllObjects : EDatabaseLogMessage.QueryingObjectsWithFilter).Format(typeof(T).Name));

            var cachedQuery = default(IEnumerable<T>);

            lock (_lock)
            {
                var query = session.Query<T>();

                if (!(filter is null))
                {
                    query = filter(query);
                    LogDebug(EDatabaseLogMessage.FilteredQueryIs.Format(query.Expression.ToString()));
                }

                cachedQuery = query.ToList();
            }

            foreach (var instance in cachedQuery)
            {
                InitializeNonPersistentFields(instance);
                LogTrace(EDatabaseLogMessage.InstantiatedFromQuery.Format(instance.ToString()));
            }

            LogDebug(EDatabaseLogMessage.QueryReturnedObjects.Format(cachedQuery.Count()));
            return cachedQuery;
        }

        /// <summary> Reads instances (filtered if needed) of a specified persistent class from the database and caches them into a collection. </summary>
        /// <typeparam name="T"> The type of objects to look for. </typeparam>
        /// <param name="filter"> The filter by which to query objects from the database. </param>
        /// <returns></returns>
        public virtual IEnumerable<T> Query<T>(Func<IQueryable<T>, IQueryable<T>> filter = null) where T : IPersistentObject
        {
            var cachedQuery = default(IEnumerable<T>);

            if (_session is null)
            {
                using (var session = SessionFactory.OpenSession())
                    cachedQuery = Query(session, filter);
            }
            else
            {
                cachedQuery = Query(_session, filter);
            }

            return cachedQuery;
        }

        /// <summary> Initializes non-persistent fields of a persistent object. This method is required to finalize reading from a database. </summary>
        /// <typeparam name="T"> The type of a persistent object to finilize initialization of. </typeparam>
        /// <param name="instance"> The instance to finilize initialization of. </param>
        private void InitializeNonPersistentFields<T>(T instance) where T : IPersistentObject
        {
            instance.InitializeNonPersistentFields(this);

            foreach (var nestedObject in instance.GetAllNestedObjects())
                nestedObject?.InitializeNonPersistentFields(this);
        }

        /// <summary> Commits any changes to a specified object to the database. </summary>
        /// <param name="session"> The session in whose scope to commit. </param>
        /// <param name="instance"> The object instance to create/update. </param>
        protected void CommitChanges(ISession session, IPersistentObject instance)
        {
            LogDebug(EDatabaseLogMessage.CommittingChangesTo.Format(instance.ToString()));

            lock (_lock)
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(instance);
                    transaction.Commit();
                }
                RemoveFromNewObjects(instance);
            }

            LogDebug(EDatabaseLogMessage.ChangesCommitted);
        }

        /// <summary> Commits any changes to a specified object to the database. </summary>
        /// <param name="instance"> the object instance to create/update. </param>
        public virtual void CommitChanges(IPersistentObject instance)
        {
            if (_session is null)
            {
                using (var session = SessionFactory.OpenSession())
                    CommitChanges(session, instance);
            }
            else
            {
                CommitChanges(_session, instance);
            }
        }

        protected virtual void PersistNewObjects(ISession session)
        {
            var newObjects = NewObjects.ToList();

            LogDebug(EDatabaseLogMessage.PersistingNewObjects.Format(newObjects.Count()));

            lock (_lock)
            {
                using (var transaction = session.BeginTransaction())
                {
                    foreach (var instance in newObjects)
                    {
                        LogTrace(EDatabaseLogMessage.CommittingChangesTo.Format(instance.ToString()));
                        session.Save(instance);
                    }
                    transaction.Commit();
                }
            }
            ClearNewObjects();

            LogDebug(EDatabaseLogMessage.AllNewObjectsPersisted);
        }

        /// <summary> Persists any transient objects cached in the repository. </summary>
        public virtual void PersistNewObjects()
        {
            if (_session is null)
            {
                using (var session = SessionFactory.OpenSession())
                    PersistNewObjects(session);
            }
            else
            {
                PersistNewObjects(_session);
            }
        }

        public virtual void RemoveFromNewObjects(IPersistentObject @object)
        {
            lock (_lock)
            {
                if (_newObjects.Contains(@object))
                    _newObjects.Remove(@object);
            }
        }

        public virtual void ClearNewObjects()
        {
            lock (_lock)
            {
                _newObjects.Clear();
            }
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
            LogDebug($"Preparing to dispose of the data repository for \"{SessionFactory?.DataBaseFileName ?? "NULL"}\".");

            if (IsClosed)
            {
                LogDebug("Already disposed of.");
                return;
            }

            if (disposing)
            {
                LogDebug("Disposing.");

                if (!(_session is null))
                {
                    _session.Close();
                    _session.Dispose();
                }
                if (!(SessionFactory is null))
                {
                    SessionFactory.Dispose();
                }
            }

            IsClosed = true;
            LogDebug("Successfully disposed of.");
        }

        #endregion Methods: IDisposeable Members
    }
}