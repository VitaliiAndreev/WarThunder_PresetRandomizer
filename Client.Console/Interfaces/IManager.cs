using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Organization.Objects.SearchSpecifications;
using System;
using System.Collections.Generic;

namespace Client.Console.Interfaces
{
    /// <summary> Controls the flow of the application. </summary>
    interface IManager : IDisposable
    {
        /// <summary> Selects vehicles based on the given specification. </summary>
        /// <param name="specification"> The specification to base the selection on. </param>
        /// <returns></returns>
        IEnumerable<IVehicle> GetVehicles(Specification specification);
    }
}