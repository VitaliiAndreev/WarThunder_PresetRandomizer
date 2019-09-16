using Core.DataBase.WarThunder.Enumerations;
using Core.Organization.Objects;
using System;
using System.Collections.Generic;

namespace Core.Organization.Helpers.Interfaces
{
    /// <summary> Controls the flow of the application. </summary>
    public interface IManager : IDisposable
    {
        #region Properties

        /// <summary> Research trees. This collection needs to be filled up after caching vehicles up from the database by calling <see cref="CacheData"/>. </summary>
        public IDictionary<ENation, ResearchTree> ResearchTrees { get; }

        #endregion Properties
        #region Methods: Initialization

        /// <summary> Reads and stores the version of the game client. </summary>
        void InitializeGameClientVersion();

        /// <summary> Caches vehicles from the database in runtime memory. </summary>
        void CacheData();

        #endregion Methods: Initialization
    }
}