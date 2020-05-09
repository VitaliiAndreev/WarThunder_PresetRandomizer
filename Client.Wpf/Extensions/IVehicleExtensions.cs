using Client.Shared.Interfaces;
using Client.Shared.Objects;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using System.Linq;

namespace Client.Wpf.Extensions
{
    public static class IVehicleExtensions
    {
        public static IVehicleLite AsLite(this IVehicle vehicle, ELanguage language)
        {
            static string localise(object key) => ApplicationHelpers.LocalisationManager.GetLocalisedString(key);
            string getTag(int index)
            {
                var tags = vehicle.Tags.Where(tag => !tag.IsDefault());

                return index < tags.Count() ? localise(tags.ToList()[index]) : null;
            }

            return new VehicleLite(vehicle, language, localise, getTag);
        }
    }
}