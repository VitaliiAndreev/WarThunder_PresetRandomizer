using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Connectors;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Extensions
{
    public static class NationCountryPairExtensions
    {
        public static Image CreateFlag(this NationCountryPair nationCountryPair, Style flagStyle, Thickness margin, bool useNationFlags, bool createTooltip = true, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left)
        {
            var flag = new Image
            {
                Style = flagStyle,
                Source = ApplicationHelpers.Manager.GetFlagImageSource(useNationFlags ? nationCountryPair.Nation.GetBaseCountry() : nationCountryPair.Country),
                Margin = margin,
                HorizontalAlignment = horizontalAlignment,
            };

            if (createTooltip)
                flag.ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(useNationFlags ? nationCountryPair.Nation.ToString() : nationCountryPair.Country.ToString());

            return flag;
        }
    }
}