using Core.DataBase.Enumerations;
using Core.DataBase.Enumerations.Logger;
using Core.DataBase.Helpers.Interfaces;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using System;
using System.Reflection;

namespace Core.DataBase.Helpers
{
    public class DataRepositoryFactory : LoggerFluency, IDataRepositoryFactory
    {
        #region Constructors

        public DataRepositoryFactory(params IConfiguredLogger[] loggers)
            : base(nameof(DataRepositoryFactory), loggers)
        {
        }

        #endregion Constructors
        #region Methods

        public virtual IDataRepository Create(EDataRepository repositoryType, string databaseName = null, bool overwrite = false, Assembly mappingAssembly = null)
        {
            switch (repositoryType)
            {
                case EDataRepository.InMemory:
                {
                    return new DataRepositoryInMemory(_loggers);
                }
                case EDataRepository.SqliteSingleSession:
                {
                    return new DataRepositorySqlite(databaseName, overwrite, mappingAssembly, true, _loggers);
                }
                case EDataRepository.SqliteMultiSession:
                {
                    return new DataRepositorySqlite(databaseName, overwrite, mappingAssembly, false, _loggers);
                }
                default:
                {
                    throw new ArgumentException(EDatabaseLogMessage.DataRepositoryTypeNotSupported.Format(repositoryType));
                }
            }
        }

        #endregion Methods
    }
}