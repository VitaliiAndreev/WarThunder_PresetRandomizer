using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Connectors;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Extensions
{
    public static class ENationExtensions
    {
        public static Image CreateFlag(this ENation nation, Style flagStyle, Thickness margin)
        {
            return new NationCountryPair(nation, ECountry.None).CreateFlag(flagStyle, margin, true);
        }
    }
}