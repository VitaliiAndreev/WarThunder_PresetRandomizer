using Core.DataBase.Objects.Interfaces;
using Core.Helpers.Logger.Interfaces;
using System;
using System.Collections.Generic;

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

        /// <summary> A session factory that provides units of work. </summary>
        IConfiguredSessionFactory SessionFactory { get; }

        /// <summary>
        /// Transient objects cached in the repository and not yet persisted.
        /// Objects should not be added to the collection directly, instead they are added on their instantiation.
        /// </summary>
        IList<IPersistentObject> NewObjects { get; }

        #endregion Properties
        #region Methods

        /// <summary> Read all instances of a specified persistent class from the database in a cached collection. </summary>
        /// <typeparam name="T"> The type of objects to look for. </typeparam>
        /// <returns></returns>
        IEnumerable<T> Query<T>() where T : IPersistentObject;

        /// <summary> Commits any changes to a specified object to the database. </summary>
        /// <param name="instance"> An object to create/update. </param>
        void CommitChanges(IPersistentObject instance);

        /// <summary> Persists any transient objects cached in the repository. </summary>
        void PersistNewObjects();

        #endregion Methods
    }
}