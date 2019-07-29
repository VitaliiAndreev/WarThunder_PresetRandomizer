using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using System;

namespace Core.Organization.Helpers.Interfaces
{
    /// <summary> Controls the flow of the application. </summary>
    public interface IManager : IDisposable
    {
        /// <summary> An instance of a settings manager. </summary>
        IWarThunderSettingsManager SettingsManager { get; }

        #region Methods: Initialization

        /// <summary> Caches vehicles from the database in runtime memory. </summary>
        void CacheVehicles();

        #endregion Methods: Initialization
    }
}