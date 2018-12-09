using NHibernate;
using System;

namespace Core.DataBase.Helpers.Interfaces
{
    /// <summary> A wrapper around <see cref="ISessionFactory"/> that applies configuration to it. </summary>
    public interface IConfiguredSessionFactory : IDisposable
    {
        /// <summary> The name of the SQLite database file (with an extension). </summary>
        string DataBaseFileName { get; }

        /// <summary> Creates a database connection and opens a session on it. </summary>
        /// <returns></returns>
        ISession OpenSession();
    }
}