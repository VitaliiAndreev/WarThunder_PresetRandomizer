using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Strategies.Interfaces;
using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Presenters
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="IAboutWindow"/>. </summary>
    public class AboutWindowPresenter : Presenter, IAboutWindowPresenter
    {
        #region Properties

        /// <summary> The parent window. </summary>
        new public IAboutWindow Owner => base.Owner as IAboutWindow;

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new presenter. </summary>
        /// <param name="strategy"> An instance of a strategy. </param>
        public AboutWindowPresenter(IAboutWindowStrategy strategy)
            : base(strategy)
        {
        }

        #endregion Constructors
    }
}