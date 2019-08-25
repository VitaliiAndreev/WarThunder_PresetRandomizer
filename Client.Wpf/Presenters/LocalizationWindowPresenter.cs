using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Strategies.Interfaces;
using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Presenters
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="ILocalizationWindow"/>. </summary>
    public class LocalizationWindowPresenter : Presenter, ILocalizationWindowPresenter
    {
        #region Properties

        /// <summary> The parent window. </summary>
        new public ILocalizationWindow Owner => base.Owner as ILocalizationWindow;

        /// <summary> The currently selected localization language. </summary>
        public string Language { get; set; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new presenter. </summary>
        /// <param name="strategy"> An instance of a strategy. </param>
        public LocalizationWindowPresenter(ILocalizationWindowStrategy strategy)
            : base(strategy)
        {
        }

        #endregion Constructors
    }
}