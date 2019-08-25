using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Strategies.Interfaces;
using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Presenters
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="ILoadingWindow"/>. </summary>
    public class LoadingWindowPresenter : Presenter, ILoadingWindowPresenter
    {
        #region Properties

        /// <summary> The parent window. </summary>
        new public ILoadingWindow Owner => base.Owner as ILoadingWindow;

        /// <summary> Indicates whether the initialization has been completed. </summary>
        public bool InitializationComplete
        {
            get => Owner.InitializationComplete;
            set => Owner.InitializationComplete = value;
        }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new presenter. </summary>
        /// <param name="strategy"> An instance of a strategy. </param>
        public LoadingWindowPresenter(ILoadingWindowStrategy strategy)
            : base(strategy)
        {
        }

        #endregion Constructors
    }
}