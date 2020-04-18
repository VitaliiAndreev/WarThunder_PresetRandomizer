using Core.DataBase.Enumerations;
using System.Reflection;

namespace Core.DataBase.Helpers.Interfaces
{
    public interface IDataRepositoryFactory
    {
        IDataRepository Create(EDataRepository repositoryType, string databaseName = null, bool overwrite = false, Assembly mappingAssembly = null);
    }
}