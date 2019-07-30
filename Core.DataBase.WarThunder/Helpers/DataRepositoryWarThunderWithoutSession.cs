using Core.DataBase.Helpers;
using Core.DataBase.Helpers.Interfaces;
using Core.Helpers.Logger.Interfaces;
using NHibernate;
using System.Reflection;

namespace Core.DataBase.WarThunder.Helpers
{
    public class DataRepositoryWarThunderWithoutSession : DataRepositoryWithoutSession
    {
        #region Constructors

        /// <summary> Creates a new repository, as well as creates and configures a <see cref="ISessionFactory"/> for it. </summary>
        /// <param name="dataBaseFileName"> The name of an SQLite database file (without an extension). </param>
        /// <param name="overwriteExistingDataBase"> Indicates whether an existing database should be overwritten on creation of the <see cref="SessionFactory"/>. </param>
        /// <param name="assemblyWithMapping"> An assembly containing mapped classes. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public DataRepositoryWarThunderWithoutSession(string dataBaseFileName, bool overwriteExistingDataBase, Assembly assemblyWithMapping, params IConfiguredLogger[] loggers)
            : base(dataBaseFileName, overwriteExistingDataBase, assemblyWithMapping, loggers)
        {
        }

        #endregion Constructors

        /// <summary>
        /// Persists any transient objects cached in the repository.
        /// This override is used to reorder the <see cref="IDataRepository.NewObjects"/> collection before persisting its contents so that the latter adhere to foreign key constraints when committed.
        /// </summary>
        protected override void PersistNewObjects(ISession session)
        {
            DataRepositoryWarThunder.ReorderNewObjectsToAdhereToForeignKeys(this);

            base.PersistNewObjects(session);
        }
    }
}