using Core.DataBase.WarThunder.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace Core.DataBase.WarThunder.Extensions
{
    public static class ERepairCostExtensions
    {
        public static bool IsValid(this ERepairCost repairCost) =>
            repairCost.EnumerationItemValueIsPositive();

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Part of the extension method signature.")]
        public static ECategory GetEconomyCategory(this ERepairCost repairCost) =>
            ECategory.RepairCost;
    }
}