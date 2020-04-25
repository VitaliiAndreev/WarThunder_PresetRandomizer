using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Extensions;
using System.Linq;
using System.Windows.Controls;

namespace Client.Wpf.Extensions
{
    public static class DataGridRowExtensions
    {
        public static object GetFieldValue(this DataGridRow row, string fieldName)
        {
            return row
                .Item
                .GetType()
                .GetProperties()
                .FirstOrDefault(property => property.Name == fieldName)
                .GetValue(row.Item)
            ;
        }

        public static bool TryGetVehicle(this DataGridRow row, out IVehicle vehicle)
        {
            var gaijinId = row.GetFieldValue(nameof(IPersistentObjectWithIdAndGaijinId.GaijinId)).ToString();

            return ApplicationHelpers.Manager.PlayableVehicles.TryGetValue(gaijinId, out vehicle);
        }

        public static void Localise(this DataGridRow row)
        {
            var rowItem = row.Item;

            foreach (var property in rowItem.GetType().GetProperties())
                rowItem.Set(property, ApplicationHelpers.LocalisationManager.GetLocalisedString(property.GetValue(rowItem).ToString(), true));
        }
    }
}