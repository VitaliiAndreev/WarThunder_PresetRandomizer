using Core.DataBase.Enumerations.Logger;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.Helpers
{
    /// <summary> Handles connections to and actions over an in-memory database. </summary>
    public class DataRepositoryInMemory : LoggerFluency, IDataRepository
    {
        #region Fields

        /// <summary> Objects loaded into the data repository. </summary>
        private readonly IList<IPersistentObject> _objects;

        #endregion Fields
        #region Properties

        /// <summary> Instances of loggers. </summary>
        public IEnumerable<IConfiguredLogger> Loggers { get { return _loggers; } }

        /// <summary> Indicates whether the repository has been disposed of. </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// Transient objects cached in the repository and not yet persisted.
        /// Objects should not be added to the collection directly, instead they are added on their initialization.
        /// </summary>
        public IList<IPersistentObject> NewObjects { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new repository, as well as creates and configures a <see cref="ISessionFactory"/> for it. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public DataRepositoryInMemory(params IConfiguredLogger[] loggers)
            : base(EDatabaseLogCategory.DataRepository, loggers)
        {
            LogDebug(EDatabaseLogMessage.CreatingInMemoryDataRepository);

            _objects = new List<IPersistentObject>();
            NewObjects = new List<IPersistentObject>();

            LogDebug(EDatabaseLogMessage.DataRepositoryCreated);
        }

        #endregion Constructors
        #region Methods: IDataRepository Members

        /// <summary> Reads instances (filtered if needed) of a specified persistent class from the database and caches them into a collection. </summary>
        /// <typeparam name="T"> The type of objects to look for. </typeparam>
        /// <param name="filter"> The filter by which to query objects from the database. </param>
        /// <returns></returns>
        public virtual IEnumerable<T> Query<T>(Func<IQueryable<T>, IQueryable<T>> filter = null) where T : IPersistentObject
        {
            LogDebug((filter is null ? EDatabaseLogMessage.QueryingAllObjects : EDatabaseLogMessage.QueryingObjectsWithFilter).FormatFluently(typeof(T).Name));

            var query = _objects.OfType<T>().AsQueryable();

            if (!(filter is null))
            {
                query = filter(query);
                LogDebug(EDatabaseLogMessage.FilteredQueryIs.FormatFluently(query.Expression.ToString()));
            }

            var cachedQuery = query.ToList();

            foreach (var instance in cachedQuery)
            {
                InitializeNonPersistentFields(instance);
                LogTrace(EDatabaseLogMessage.InstantiatedFromQuery.FormatFluently(instance.ToString()));
            }

            LogDebug(EDatabaseLogMessage.QueryReturnedObjects.FormatFluently(cachedQuery.Count()));
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
        /// <param name="instance"> the object instance to create/update. </param>
        public virtual void CommitChanges(IPersistentObject instance) { }

        /// <summary> Persists any transient objects cached in the repository. </summary>
        public virtual void PersistNewObjects()
        {
            _objects.AddRange(NewObjects);
            NewObjects.Clear();
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
            LogDebug(ECoreLogMessage.PreparingToDisposeOf.FormatFluently(EDatabaseLogMessage.TheInMemoryDataRepository));

            if (IsClosed)
            {
                LogDebug(ECoreLogMessage.AlreadyDisposed);
                return;
            }

            if (disposing)
            {
            }

            IsClosed = true;
            LogDebug(ECoreLogMessage.SuccessfullyDisposed);
        }

        #endregion Methods: IDisposeable Members
    }
}