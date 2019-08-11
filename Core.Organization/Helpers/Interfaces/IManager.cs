using System;

namespace Core.Organization.Helpers.Interfaces
{
    /// <summary> Controls the flow of the application. </summary>
    public interface IManager : IDisposable
    {
        #region Methods: Initialization

        /// <summary> Reads and stores the version of the game client. </summary>
        void InitializeGameClientVersion();

        /// <summary> Caches vehicles from the database in runtime memory. </summary>
        void CacheVehicles();

        #endregion Methods: Initialization
    }
}