using Client.Wpf.Presenters.Interfaces;

namespace Client.Wpf.Windows.Interfaces
{
    /// <summary> The settings window. </summary>
    public interface ISettingsWindow : IBaseWindow
    {
        #region Properties

        /// <summary> An instance of a presenter. </summary>
        new ISettingsWindowPresenter Presenter { get; }

        /// <summary> Indicates whether the location of the War Thunder directory is valid. </summary>
        bool WarThunderLocationIsValid { get; }

        /// <summary> Indicates whether the location of the Klensy's War Thunder Tools directory is valid. </summary>
        bool KlensysWarThunderToolsLocationIsValid { get; }

        /// <summary> Location of the War Thunder directory. </summary>
        string WarThunderLocation { get; }

        /// <summary> Location of the Klensy's War Thunder Tools directory. </summary>
        string KlensysWarThunderToolsLocation { get; }

        #endregion Properties
    }
}