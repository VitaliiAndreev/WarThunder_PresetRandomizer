using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Client.Wpf.Helpers.Interfaces;
using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Helpers.Logger.Interfaces;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.Organization.Helpers;
using Core.Organization.Helpers.Interfaces;
using Core.Randomization.Helpers.Interfaces;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Client.Wpf.Helpers
{
    /// <summary> Controls the flow of the application. </summary>
    public class WpfClientManager: Manager, IWpfClientManager
    {
        #region Fields

        private readonly IDictionary<string, BitmapSource> _vehicleIconBitmapSources;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new manager and loads settings stored in the <see cref="EWpfClientFile.Settings"/> file. </summary>
        /// <param name="generateDatabase"> Whether to read data from JSON instead of the database. </param>
        /// <param name="readOnlyJson"> Whether to generate the database. </param>
        /// <param name="readPreviouslyUnpackedJson"> Whether to extract game files. </param>
        public WpfClientManager
        (
            IWarThunderFileManager fileManager,
            IWarThunderFileReader fileReader,
            IWarThunderSettingsManager settingsManager,
            IParser parser,
            IUnpacker unpacker,
            IConverter converter,
            IWarThunderJsonHelper jsonHelper,
            ICsvDeserializer csvDeserializer,
            IDataRepositoryFactory dataRepositoryFactory,
            IRandomiser randomizer,
            IVehicleSelector vehicleSelector,
            IPresetGenerator presetGenerator,
            bool generateDatabase,
            bool readOnlyJson,
            bool readPreviouslyUnpackedJson,
            params IConfiguredLogger[] loggers
        ) : base(fileManager, fileReader, settingsManager, parser, unpacker, converter, jsonHelper, csvDeserializer, dataRepositoryFactory, randomizer, vehicleSelector, presetGenerator, generateDatabase, readOnlyJson, readPreviouslyUnpackedJson, loggers)
        {
            _vehicleIconBitmapSources = new ConcurrentDictionary<string, BitmapSource>();

            ProcessVehicleImages = (vehicle) => GetIconBitmapSource(vehicle);
        }

        #endregion Constructors
        #region Methods: Settings

        /// <summary> Loads settings from the file attached to the settings manager. </summary>
        protected override void LoadSettings()
        {
            base.LoadSettings();

            LoadSettings(typeof(WpfSettings));
        }

        #endregion Methods: Settings
        #region Methods: Working with Caches

        public BitmapSource GetIconBitmapSource(IVehicle vehicle)
        {
            if (_vehicleIconBitmapSources.TryGetValue(vehicle.GaijinId, out var cachedSource))
                return cachedSource;

            var bitmapSource = vehicle.Images.Icon.ToBitmapSource(ImageFormat.Png);

            bitmapSource.Freeze();

            _vehicleIconBitmapSources.Add(vehicle.GaijinId, bitmapSource);

            return bitmapSource;
        }

        #endregion Methods: Working with Caches
    }
}