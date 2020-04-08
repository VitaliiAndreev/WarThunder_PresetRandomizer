using Core.Localization.Helpers.Interfaces;

namespace Client.Wpf.Windows.Interfaces.Base
{
    /// <summary> A window that supports localization via <see cref="ILocalizationManager"/></summary>
    public interface ILocalizedWindow : IWindow
    {
        /// <summary> Applies localization to visible text in the window. </summary>
        void Localise();
    }
}