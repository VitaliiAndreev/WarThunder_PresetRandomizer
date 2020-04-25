using System.Windows.Controls;

namespace Client.Wpf.Controls.Base
{
    /// <summary> A user contol supporting localisation. </summary>
    public class LocalisedUserControl : UserControl
    {
        /// <summary> Applies localisation to visible text on the control. </summary>
        public virtual void Localise()
        {
        }
    }
}