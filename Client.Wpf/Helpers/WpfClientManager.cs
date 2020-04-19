using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Client.Wpf.Helpers.Interfaces;
using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Helpers.Logger.Interfaces;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.Organization.Helpers;
using Core.Organization.Helpers.Interfaces;
using Core.Randomization.Helpers.Interfaces;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Client.Wpf.Helpers
{
    /// <summary> Controls the flow of the application. </summary>
    public class WpfClientManager: Manager, IWpfClientManager
    {
        #region Fields

        private readonly IDictionary<ECountry, ImageSource> _flagImageSources;
        private readonly IDictionary<string, BitmapSource> _vehicleIconBitmapSources;
        private readonly IDictionary<string, BitmapSource> _vehiclePortraitBitmapSources;

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
            _flagImageSources = new Dictionary<ECountry, ImageSource>();
            _vehicleIconBitmapSources = new ConcurrentDictionary<string, BitmapSource>();
            _vehiclePortraitBitmapSources = new ConcurrentDictionary<string, BitmapSource>();

            ProcessVehicleImages = (vehicle) => { GetIconBitmapSource(vehicle); GetPortraitBitmapSource(vehicle); };
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

        public ImageSource GetFlagImageSource(ECountry country)
        {
            if (!(_flagImageSources is null) && _flagImageSources.TryGetValue(country, out var cachedSource))
                return cachedSource;

            var imageSource = Application.Current.MainWindow.FindResource(EReference.CountryIconKeys[country]) as ImageSource;

            _flagImageSources?.Add(country, imageSource);

            return imageSource;
        }

        private BitmapSource GetBitmapSource(IDictionary<string, BitmapSource> cache, IVehicle vehicle, Func<IVehicle, byte[]> getImageBytes)
        {
            if (!(cache is null) && cache.TryGetValue(vehicle.GaijinId, out var cachedSource))
                return cachedSource;

            var bitmapSource = getImageBytes(vehicle).ToBitmapSource();

            bitmapSource.Freeze();

            cache?.Add(vehicle.GaijinId, bitmapSource);

            return bitmapSource;
        }

        public BitmapSource GetIconBitmapSource(IVehicle vehicle) =>
            GetBitmapSource(_vehicleIconBitmapSources, vehicle, anyVehicle => anyVehicle.Images.IconBytes);

        public BitmapSource GetPortraitBitmapSource(IVehicle vehicle) =>
            GetBitmapSource(_vehiclePortraitBitmapSources, vehicle, anyVehicle => anyVehicle.Images.PortraitBytes);

        #endregion Methods: Working with Caches
    }
}