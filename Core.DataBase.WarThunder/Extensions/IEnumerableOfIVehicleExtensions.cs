using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Extensions
{
    public static class IEnumerableExtensions
    {
        #region Sorting

        public static IOrderedEnumerable<IVehicle> OrderByNationCountryBranchClassSubclassRankId(this IEnumerable<IVehicle> vehicles)
        {
            return vehicles
                .AsOrderedEnumerable()
                .ThenByNation()
                .ThenByCountry()
                .ThenByBranch()
                .ThenByClassSubclassRankId()
            ;
        }

        public static IOrderedEnumerable<IVehicle> OrderByNationSubclassRankId(this IEnumerable<IVehicle> vehicles)
        {
            return vehicles
                .AsOrderedEnumerable()
                .ThenByNation()
                .ThenBySubclassRankId()
            ;
        }

        public static IOrderedEnumerable<IVehicle> OrderByClassSubclassRankId(this IEnumerable<IVehicle> vehicles)
        {
            return vehicles
                .AsOrderedEnumerable()
                .ThenByClassSubclassRankId()
            ;
        }

        public static IOrderedEnumerable<IVehicle> OrderBySubclassRankId(this IEnumerable<IVehicle> vehicles)
        {
            return vehicles
                .AsOrderedEnumerable()
                .ThenBySubclassRankId();
        }

        #endregion Sorting
    }
}