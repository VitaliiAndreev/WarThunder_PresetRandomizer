using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;

namespace Core.DataBase.WarThunder.Extensions
{
    public static class EVehicleAvailabilityExtensions
    {
        public static bool IsValid(this EVehicleAvailability availability) =>
            availability.EnumerationItemValueIsPositive();
    }
}