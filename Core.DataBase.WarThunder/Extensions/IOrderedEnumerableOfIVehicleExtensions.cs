using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Linq;

namespace Core.DataBase.WarThunder.Extensions
{
    public static class IOrderedEnumerableOfIVehicleExtensions
    {
        #region Sorting

        public static IOrderedEnumerable<IVehicle> ThenByClassSubclassRankId(this IOrderedEnumerable<IVehicle> vehicles)
        {
            return vehicles
                .ThenBy(vehicle => vehicle.Class)
                .ThenBySubclassRankId()
            ;
        }

        public static IOrderedEnumerable<IVehicle> ThenBySubclassRankId(this IOrderedEnumerable<IVehicle> orderedVehicles)
        {
            return orderedVehicles
                .ThenBy(vehicle => vehicle.Subclasses.First)
                .ThenBy(vehicle => vehicle.Subclasses.Second)
                .ThenBy(vehicle => vehicle.Rank)
                .ThenBy(vehicle => vehicle.GaijinId)
            ;
        }

        #endregion Sorting
    }
}