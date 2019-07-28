using Core.DataBase.Objects.Interfaces;
using Core.Helpers.Logger.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.Helpers
{
    public class DataRepositoryWithSession : DataRepositoryWithoutSession
    {
        #region Properties

        public ISession Session { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new repository, as well as creates and configures a <see cref="ISessionFactory"/> for it. </summary>
        /// <param name="dataBaseFileName"> The name of an SQLite database file (without an extension). </param>
        /// <param name="overwriteExistingDataBase"> Indicates whether an existing database should be overwritten on creation of the session factory. </param>
        /// <param name="assemblyWithMapping"> An assembly containing mapped classes. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public DataRepositoryWithSession(string dataBaseFileName, bool overwriteExistingDataBase, Assembly assemblyWithMapping, params IConfiguredLogger[] loggers)
            : base(dataBaseFileName, overwriteExistingDataBase, assemblyWithMapping, loggers)
        {
            Session = SessionFactory.OpenSession();
        }

        #endregion Constructors
        #region Methods: IDataRepository Members

        /// <summary> Reads instances (filtered if needed) of a specified persistent class from the database and caches them into a collection. </summary>
        /// <typeparam name="T"> The type of objects to look for. </typeparam>
        /// <param name="filter"> The filter by which to query objects from the database. </param>
        /// <returns></returns>
        public override IEnumerable<T> Query<T>(Func<IQueryable<T>, IQueryable<T>> filter = null) =>
            Query(Session, filter);

        /// <summary> Commits any changes to a specified object to the database. </summary>
        /// <param name="instance"> the object instance to create/update. </param>
        public override void CommitChanges(IPersistentObject instance) =>
            CommitChanges(Session, instance);

        /// <summary> Persists any transient objects cached in the repository. </summary>
        public override void PersistNewObjects() =>
            PersistNewObjects(Session);

        #endregion Methods: IDataRepository Members
        #region Methods: IDisposeable Members

        /// <summary> Releases unmanaged resources. </summary>
        /// <param name="disposing"> Indicates whether this method is being called from <see cref="Dispose"/>. </param>
        protected override void Dispose(bool disposing)
        {
            Session.Close();
            Session.Dispose();

            base.Dispose(disposing);
        }

        #endregion Methods: IDisposeable Members
    }
}