using Core.DataBase.Enumerations;
using Core.DataBase.Enumerations.Logger;
using Core.DataBase.Helpers;
using Core.DataBase.Helpers.Interfaces;
using Core.Extensions;
using Core.Helpers.Logger.Interfaces;
using System;
using System.Reflection;

namespace Core.DataBase.WarThunder.Helpers
{
    public class DataRepositoryFactoryWarThunder : DataRepositoryFactory
    {
        #region Constructors

        public DataRepositoryFactoryWarThunder(params IConfiguredLogger[] loggers)
            : base(loggers)
        {
        }

        #endregion Constructors
        #region Methods

        public override IDataRepository Create(EDataRepository repositoryType, string databaseName = null, bool overwrite = false, Assembly mappingAssembly = null)
        {
            switch (repositoryType)
            {
                case EDataRepository.InMemory:
                {
                    return new DataRepositoryInMemoryWarThunder(_loggers);
                }
                case EDataRepository.SqliteSingleSession:
                {
                    return new DataRepositorySqliteWarThunder(databaseName, overwrite, mappingAssembly, true, _loggers);
                }
                case EDataRepository.SqliteMultiSession:
                {
                    return new DataRepositorySqliteWarThunder(databaseName, overwrite, mappingAssembly, false, _loggers);
                }
                default:
                {
                    throw new ArgumentException(EDatabaseLogMessage.DataRepositoryTypeNotSupported.FormatFluently(repositoryType));
                }
            }
        }

        #endregion Methods
    }
}