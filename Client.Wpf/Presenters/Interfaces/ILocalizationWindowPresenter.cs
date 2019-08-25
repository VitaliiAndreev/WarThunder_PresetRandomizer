using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Presenters.Interfaces
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="ILocalizationWindow"/>. </summary>
    public interface ILocalizationWindowPresenter : IPresenter
    {
        #region Properties

        /// <summary> The parent window. </summary>
        new ILocalizationWindow Owner { get; }

        /// <summary> The currently selected localization language. </summary>
        string Language { get; set; }

        #endregion Properties
    }
}