using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Client.Wpf.Windows.Interfaces.Base;
using Core.Enumerations.Logger;
using Core.Extensions;
using System;
using System.Configuration;
using System.Windows;

namespace Client.Wpf.Windows
{
    /// <summary> Interaction logic for AboutWindow.xaml. </summary>
    public partial class AboutWindow : BaseWindow, IAboutWindow
    {
        #region Properties

        /// <summary> An instance of a presenter. </summary>
        new public IAboutWindowPresenter Presenter { get; private set; }

        /// <summary> An instance of a presenter. </summary>
        IPresenter IWindowWithPresenter.Presenter => Presenter;

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new loading window. </summary>
        /// <param name="presenter"> The presenter to attach. </param>
        /// <param name="parentWindow"> The window that owns this one. </param>
        public AboutWindow(IAboutWindowPresenter presenter, IBaseWindow parentWindow)
            : base(EWpfClientLogCategory.AboutWindow, parentWindow)
        {
            Presenter = presenter;
            Presenter.SetParentWindow(this);

            InitializeComponent();

            _version.Text = $"{EApplicationData.Version} ({{0}})";
            _vitalyAndreyevStats.Text = EApplicationData.ContributionsByVitalyAndreyev;

            Localize();

            Log.Debug(ECoreLogMessage.Initialized);
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Logs showing of the window. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnLoaded(object sender, RoutedEventArgs eventArguments) =>
            Log.Debug(ECoreLogMessage.Shown);

        /// <summary> Logs closing of the window. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnClosed(object sender, EventArgs eventArguments) =>
            Log.Debug(ECoreLogMessage.Closed);

        #endregion Methods: Event Handlers

        /// <summary> Applies localization to visible text in the window. </summary>
        public override void Localize()
        {
            base.Localize();

            Title = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.ApplicationNameAbbreviated);

            _version.Text = _version.Text.FormatFluently(ApplicationHelpers.LocalizationManager.GetLocalizedString(EApplicationData.DevelopmentStageLocalizationKey));

            _contributors.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Contributors);
            _vitalyAndreyev.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.VitalyAndreyev);
            _vitalyAndreyevStats.Text = $"{_vitalyAndreyevStats.Text} {ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Commits)}";

            _thanks.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Thanks);
            _gaijin.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.GaijinEntertainmentForWarThunder);
            _klensy.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.KlensyForWtTools);
        }
    }
}