using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Strategies.Interfaces;
using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Presenters
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="ISettingsWindow"/>. </summary>
    public class SettingsWindowPresenter : Presenter, ISettingsWindowPresenter
    {
        #region Properties

        /// <summary> The parent window. </summary>
        new public ISettingsWindow Owner => base.Owner as ISettingsWindow;

        /// <summary> The current state of closure of the <see cref="Owner"/> window. </summary>
        public ESettingsWindowClosureState ClosingState { get; set; }

        /// <summary> Indicates whether the location of the War Thunder directory is valid. </summary>
        public bool WarThunderLocationIsValid => Owner.WarThunderLocationIsValid;

        /// <summary> Indicates whether the location of the Klensy's War Thunder Tools directory is valid. </summary>
        public bool KlensysWarThunderToolsLocationIsValid => Owner.KlensysWarThunderToolsLocationIsValid;

        /// <summary> The location of the War Thunder directory at the moment the <see cref="Owner"/> is opened. </summary>
        public string PreviousValidWarThunderLocation { get; set; }

        /// <summary> The location of the Klensy's War Thunder Tools directory at the moment the <see cref="Owner"/> is opened. </summary>
        public string PreviousValidKlensysWarThunderToolsLocation { get; set; }

        /// <summary> The location of the War Thunder directory. </summary>
        public string WarThunderLocation => Owner.WarThunderLocation;

        /// <summary> The location of the Klensy's War Thunder Tools directory. </summary>
        public string KlensysWarThunderToolsLocation => Owner.KlensysWarThunderToolsLocation;

        /// <summary> Indicates whether location settings have changed from previously valid ones. </summary>
        public bool LocationSettingsChangedFromValidToValid =>
            PreviousValidWarThunderLocation is string
            && PreviousValidKlensysWarThunderToolsLocation is string
            && (WarThunderLocation != PreviousValidWarThunderLocation || KlensysWarThunderToolsLocation != PreviousValidKlensysWarThunderToolsLocation);

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new presenter. </summary>
        /// <param name="strategy"> An instance of a strategy. </param>
        public SettingsWindowPresenter(ISettingsWindowStrategy strategy)
            : base(strategy)
        {
        }

        #endregion Constructors
    }
}