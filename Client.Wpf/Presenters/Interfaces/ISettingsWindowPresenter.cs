using Client.Wpf.Enumerations;
using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Presenters.Interfaces
{
    /// <summary> A presenter that serves to facilitate cofunction of the backend and the frontend. Specific to the <see cref="ISettingsWindow"/>. </summary>
    public interface ISettingsWindowPresenter : IPresenter
    {
        #region Properties

        /// <summary> The parent window. </summary>
        new ISettingsWindow Owner { get; }

        /// <summary> The current state of closure of the <see cref="Owner"/> window. </summary>
        ESettingsWindowClosureState ClosingState { get; set; }

        /// <summary> Indicates whether the location of the War Thunder directory is valid. </summary>
        bool WarThunderLocationIsValid { get; }

        /// <summary> Indicates whether the location of the Klensy's War Thunder Tools directory is valid. </summary>
        bool KlensysWarThunderToolsLocationIsValid { get; }

        /// <summary> The location of the War Thunder directory at the moment the <see cref="Owner"/> window is opened. </summary>
        string PreviousValidWarThunderLocation { get; set; }

        /// <summary> The location of the Klensy's War Thunder Tools directory at the moment the <see cref="Owner"/> window is opened. </summary>
        string PreviousValidKlensysWarThunderToolsLocation { get; set; }

        /// <summary> The location of the War Thunder directory. </summary>
        string WarThunderLocation { get; }

        /// <summary> The location of the Klensy's War Thunder Tools directory. </summary>
        string KlensysWarThunderToolsLocation { get; }

        /// <summary> Indicates whether location settings have changed from previously valid ones. </summary>
        bool LocationSettingsChangedFromValidToValid { get; }

        #endregion Properties
    }
}