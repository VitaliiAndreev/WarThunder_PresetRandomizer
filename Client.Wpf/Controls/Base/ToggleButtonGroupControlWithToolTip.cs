using Core.DataBase.WarThunder.Objects.Connectors;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls.Base
{
    /// <summary> A user control consisting of a row or a column of <see cref="ToggleButton"/>s, indexed by <typeparamref name="T"/> values, and having a tooltip. </summary>
    /// <typeparam name="T"> The key type. </typeparam>
    public class ToggleButtonGroupControlWithToolTip<T> : ToggleButtonGroupControl<T>
    {
        /// <summary> Applies localisation to visible text on the control. </summary>
        [SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "Implicit variable declaration.")]
        public override void Localise()
        {
            base.Localise();

            foreach (var button in Buttons.Values)
            {
                var localizationKey = default(string);

                switch (button.Tag)
                {
                    case NationCountryPair nationCountryPair:
                        localizationKey = nationCountryPair.Country.ToString();
                        break;
                    case T key:
                        localizationKey = key.ToString();
                        break;
                    default:
                        return;
                }

                button.ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(localizationKey);
            }
        }
    }
}