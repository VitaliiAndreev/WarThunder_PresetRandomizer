using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Linq;

namespace Core.DataBase.WarThunder.Extensions
{
    public static class IOrderedEnumerableOfIVehicleExtensions
    {
        #region Sorting

        public static IOrderedEnumerable<IVehicle> ThenByNation(this IOrderedEnumerable<IVehicle> vehicles) => vehicles.ThenBy(vehicle => vehicle.Nation.AsEnumerationItem);
        public static IOrderedEnumerable<IVehicle> ThenByCountry(this IOrderedEnumerable<IVehicle> vehicles) => vehicles.ThenBy(vehicle => vehicle.Country);
        public static IOrderedEnumerable<IVehicle> ThenByBranch(this IOrderedEnumerable<IVehicle> vehicles) => vehicles.ThenBy(vehicle => vehicle.Branch.AsEnumerationItem);
        public static IOrderedEnumerable<IVehicle> ThenByClass(this IOrderedEnumerable<IVehicle> vehicles) => vehicles.ThenBy(vehicle => vehicle.Class);
        public static IOrderedEnumerable<IVehicle> ThenByFirstSubclass(this IOrderedEnumerable<IVehicle> vehicles) => vehicles.ThenBy(vehicle => vehicle.Subclasses.First);
        public static IOrderedEnumerable<IVehicle> ThenBySecondSubclass(this IOrderedEnumerable<IVehicle> vehicles) => vehicles.ThenBy(vehicle => vehicle.Subclasses.Second);
        public static IOrderedEnumerable<IVehicle> ThenByRank(this IOrderedEnumerable<IVehicle> vehicles) => vehicles.ThenBy(vehicle => vehicle.Rank);
        public static IOrderedEnumerable<IVehicle> ThenByGaijinId(this IOrderedEnumerable<IVehicle> vehicles) => vehicles.ThenBy(vehicle => vehicle.GaijinId);

        public static IOrderedEnumerable<IVehicle> ThenByClassSubclassRankId(this IOrderedEnumerable<IVehicle> vehicles)
        {
            return vehicles
                .ThenByClass()
                .ThenBySubclassRankId()
            ;
        }

        public static IOrderedEnumerable<IVehicle> ThenBySubclassRankId(this IOrderedEnumerable<IVehicle> orderedVehicles)
        {
            return orderedVehicles
                .ThenByFirstSubclass()
                .ThenBySecondSubclass()
                .ThenByRankId()
            ;
        }

        public static IOrderedEnumerable<IVehicle> ThenByRankId(this IOrderedEnumerable<IVehicle> orderedVehicles)
        {
            return orderedVehicles
                .ThenByRank()
                .ThenByGaijinId()
            ;
        }

        #endregion Sorting
    }
}