using Core.DataBase.WarThunder.Enumerations;
using Core.Web.WarThunder.Objects;
using System.Collections.Generic;

namespace Core.Web.WarThunder.Helpers.Interfaces
{
    public interface IThunderSkillParser
    {
        IDictionary<string, VehicleUsage> GetVehicleUsage(EBranch branch);
    }
}