using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Organization.Helpers.Interfaces;
using Core.Organization.Objects.SearchSpecifications;
using System.Collections.Generic;

namespace Client.Console.Helpers.Interfaces
{
    /// <summary> Controls the flow of the application. </summary>
    public interface IConsoleClientManager : IManager
    {
        /// <summary> Randomly selects vehicles based on the given specification. </summary>
        /// <param name="specification"> The specification to base the selection on. </param>
        /// <returns></returns>
        IEnumerable<IVehicle> GetRandomVehicles(Specification specification);
    }
}