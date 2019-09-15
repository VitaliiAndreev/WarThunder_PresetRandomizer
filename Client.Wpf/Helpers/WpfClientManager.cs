using Client.Wpf.Enumerations;
using Client.Wpf.Helpers.Interfaces;
using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.Helpers.Logger.Interfaces;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.Organization.Helpers;
using Core.Organization.Helpers.Interfaces;
using Core.Randomization.Helpers.Interfaces;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System.Reflection;

namespace Client.Wpf.Helpers
{
    /// <summary> Controls the flow of the application. </summary>
    public class WpfClientManager: Manager, IWpfClientManager
    {
        #region Constructors

        /// <summary> Creates a new manager and loads settings stored in the <see cref="EWpfClientFile.Settings"/> file. </summary>
        public WpfClientManager
        (
            IWarThunderFileManager fileManager,
            IWarThunderFileReader fileReader,
            IWarThunderSettingsManager settingsManager,
            IParser parser,
            IUnpacker unpacker,
            IWarThunderJsonHelper jsonHelper,
            ICsvDeserializer csvDeserializer,
            IRandomizer randomizer,
            IVehicleSelector vehicleSelector,
            params IConfiguredLogger[] loggers
        ) : base(fileManager, fileReader, settingsManager, parser, unpacker, jsonHelper, csvDeserializer, randomizer, vehicleSelector, loggers)
        {
        }

        #endregion Constructors
        #region Methods: Settings

        /// <summary> Loads settings from the file attached to the settings manager. </summary>
        protected override void LoadSettings() => LoadSettings(typeof(WpfSettings));

        #endregion Methods: Settings
    }
}