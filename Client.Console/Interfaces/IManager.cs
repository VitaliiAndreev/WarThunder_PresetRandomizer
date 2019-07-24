using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Organization.Objects.SearchSpecifications;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using System;
using System.Collections.Generic;

namespace Client.Console.Interfaces
{
    /// <summary> Controls the flow of the application. </summary>
    interface IManager : IDisposable
    {
        /// <summary> An instance of a settings manager. </summary>
        IWarThunderSettingsManager SettingsManager { get; }

        #region Methods: Initialization

        /// <summary> Caches vehicles from the database in runtime memory. </summary>
        void CacheVehicles();

        #endregion Methods: Initialization

        /// <summary> Randomly selects vehicles based on the given specification. </summary>
        /// <param name="specification"> The specification to base the selection on. </param>
        /// <returns></returns>
        IEnumerable<IVehicle> GetRandomVehicles(Specification specification);
    }
}