using Client.Shared.Interfaces;
using Client.Shared.Wpf.Extensions;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Windows.Controls;

namespace Client.Wpf
{
    public static class DataGridRowExtensions
    {
        #region Methods: Reading

        public static bool TryGetVehicle(this DataGridRow row, out IVehicle vehicle)
        {
            var gaijinId = row.GetFieldValue(nameof(IVehicleLite.GaijinIdLite))?.ToString() ?? string.Empty;

            return ApplicationHelpers.Manager.PlayableVehicles.TryGetValue(gaijinId, out vehicle);
        }

        #endregion Methods: Reading
        #region Methods: Styling

        public static void SetRowBackground(this DataGridRow row, IVehicle vehicle)
        {
            if (vehicle.IsSquadronVehicle)
                row.Style = row.GetStyle(EStyleKey.DataGridRow.DataGridRowSquadron);

            else if (vehicle.IsPremium)
                row.Style = row.GetStyle(EStyleKey.DataGridRow.DataGridRowPremium);

            else
                row.Style = row.GetStyle(EStyleKey.DataGridRow.DataGridRowRegular);
        }

        #endregion Methods: Styling
    }
}