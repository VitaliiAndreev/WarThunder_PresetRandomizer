using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Connectors;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Extensions
{
    public static class ECountryExtensions
    {
        public static Image CreateFlag(this ECountry country, Style flagStyle, Thickness margin)
        {
            return new NationCountryPair(ENation.None, country).CreateFlag(flagStyle, margin, false);
        }
    }
}