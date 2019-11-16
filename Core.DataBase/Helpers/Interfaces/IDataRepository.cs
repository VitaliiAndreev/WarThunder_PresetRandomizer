using Core.DataBase.Objects.Interfaces;
using Core.Helpers.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.Helpers.Interfaces
{
    /// <summary> Handles connections to and actions over an SQLite database. </summary>
    public interface IDataRepository : IDisposable
    {
        #region Properties

        /// <summary> Instances of loggers. </summary>
        IEnumerable<IConfiguredLogger> Loggers { get; }

        /// <summary> Indicates whether the repository has been disposed of. </summary>
        bool IsClosed { get; }

        /// <summary>
        /// Transient objects cached in the repository and not yet persisted.
        /// Objects should not be added to the collection directly, instead they are added on their instantiation.
        /// </summary>
        IList<IPersistentObject> NewObjects { get; }

        #endregion Properties
        #region Methods

        /// <summary> Reads instances (filtered if needed) of a specified persistent class from the database and caches them into a collection. </summary>
        /// <typeparam name="T"> The type of objects to look for. </typeparam>
        /// <param name="filter"> The filter by which to query objects from the database. </param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(Func<IQueryable<T>, IQueryable<T>> filter = null) where T : IPersistentObject;

        /// <summary> Commits any changes to a specified object to the database. </summary>
        /// <param name="instance"> An object to create/update. </param>
        void CommitChanges(IPersistentObject instance);

        /// <summary> Persists any transient objects cached in the repository. </summary>
        void PersistNewObjects();

        #endregion Methods
    }
}