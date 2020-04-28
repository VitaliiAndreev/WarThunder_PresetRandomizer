using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Client.Wpf.Windows.Interfaces.Base;
using Core.Enumerations.Logger;
using Core.Extensions;
using System;
using System.Windows;

namespace Client.Wpf.Windows
{
    /// <summary> Interaction logic for AboutWindow.xaml. </summary>
    public partial class AboutWindow : BaseWindow, IAboutWindow
    {
        #region Properties

        /// <summary> An instance of a presenter. </summary>
        new public IAboutWindowPresenter Presenter => base.Presenter as IAboutWindowPresenter;

        /// <summary> An instance of a presenter. </summary>
        IPresenter IWindowWithPresenter.Presenter => base.Presenter;

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new "About" window. </summary>
        /// <param name="presenter"> The presenter to attach. </param>
        /// <param name="parentWindow"> The window that owns this one. </param>
        public AboutWindow(IAboutWindowPresenter presenter, IBaseWindow parentWindow)
            : base(EWpfClientLogCategory.AboutWindow, parentWindow, presenter)
        {
            InitializeComponent();

            _version.Text = $"{EApplicationData.Version} ({{0}})";

            Localise();

            Log.Debug(ECoreLogMessage.Initialised);
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
        public override void Localise()
        {
            base.Localise();

            Title = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ApplicationNameAbbreviated);

            _version.Text = _version.Text.FormatFluently(ApplicationHelpers.LocalisationManager.GetLocalisedString(EApplicationData.DevelopmentStageLocalizationKey));
            _thanks.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.Thanks);
            _gaijin.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.GaijinEntertainmentForWarThunder);
            _klensy.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.KlensyForWtTools);
            _benjaminDeCock.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.BenjaminDeCockForStripeFlagSet);
            _yummigum.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.YummigumForIconSweets2);
        }
    }
}