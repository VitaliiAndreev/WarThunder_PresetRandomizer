using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Helpers;
using Client.Wpf.Helpers.Interfaces;
using Core.Csv.WarThunder.Helpers;
using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.Json.Helpers;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.Localization.Enumerations.Logger;
using Core.Localization.Helpers;
using Core.Localization.Helpers.Interfaces;
using Core.Organization.Helpers;
using Core.Organization.Helpers.Interfaces;
using Core.Randomization.Helpers;
using Core.Randomization.Helpers.Interfaces;
using Core.UnpackingToolsIntegration.Attributes;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Client.Wpf
{
    /// <summary> Stores instances of helpers to be available project-wide. </summary>
    public static class ApplicationHelpers
    {
        #region Fields

        private static IParser _parser;
        private static IUnpacker _unpacker;
        private static IConverter _converter;
        private static IWarThunderJsonHelper _jsonHelper;
        private static ICsvDeserializer _csvDeserializer;
        private static IRandomiser _randomizer;
        private static IVehicleSelector _vehicleSelector;
        private static IPresetGenerator _presetGenerator;

        /// <summary> Indicates whether loggers have been initialized. </summary>
        private static bool _loggersInitialized;
        /// <summary> Indicates whether helpers have been initialized. </summary>
        private static bool _helpersInitialized;

        #endregion Fields
        #region Properties

        public static IConfiguredLogger[] Loggers { get; private set; }
        public static IWarThunderFileManager FileManager { get; private set; }
        public static IWarThunderFileReader FileReader { get; private set; }
        public static IWpfClientSettingsManager SettingsManager { get; private set; }
        public static IWpfClientManager Manager { get; private set; }
        public static ILocalizationManager LocalizationManager { get; internal set; }

        /// <summary> An instance of a window factory. </summary>
        public static IWindowFactory WindowFactory { get; private set; }
        /// <summary> An instance of an active logger. </summary>
        public static IActiveLogger Log { get; private set; }

        #endregion Properties
        #region Constructors

        /// <summary> The static constructor is used for setting default values. </summary>
        static ApplicationHelpers()
        {
            _helpersInitialized = false;
            _loggersInitialized = false;
        }

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Initializes NLog-based loggers. Repeated initialization is being skipped. </summary>
        public static void InitializeLoggers()
        {
            if (_loggersInitialized) return;

            var fileLogger = new ConfiguredNLogger(ELoggerName.FileLogger, new ExceptionFormatter(), ESubdirectory.Logs, true);

            var wpfLogger = new ConfiguredNLogger(ELoggerName.WpfLogger, new ExceptionFormatter());
            wpfLogger.LogInstantiation(fileLogger);

            Loggers = new IConfiguredLogger[]
            {
                fileLogger,
                wpfLogger,
            };

            Log = CreateActiveLogger(EWpfClientLogCategory.ApplicationHelpers);

            Log.Debug(ECoreLogMessage.ObjectInitialized.FormatFluently(EWord.Loggers));
            _loggersInitialized = true;
        }

        /// <summary> Initializes fields and properties. Repeated initialization is being skipped. </summary>
        /// <param name="generateDatabase"> Whether to read data from JSON instead of the database. </param>
        /// <param name="readOnlyJson"> Whether to generate the database. </param>
        /// <param name="readPreviouslyUnpackedJson"> Whether to extract game files. </param>
        public static void Initialize(bool generateDatabase, bool readOnlyJson, bool readPreviouslyUnpackedJson)
        {
            if (!_loggersInitialized)
                InitializeLoggers();

            if (_helpersInitialized)
                return;

            Log.Debug(ECoreLogMessage.Initializing.FormatFluently(EWord.Helpers.ToLower()));

            InitializeHelpers(generateDatabase, readOnlyJson, readPreviouslyUnpackedJson);

            Log.Debug(ECoreLogMessage.ObjectInitialized.FormatFluently(EWord.Helpers));
            _helpersInitialized = true;
        }

        /// <summary> Initializes helpers. </summary>
        /// <param name="generateDatabase"> Whether to read data from JSON instead of the database. </param>
        /// <param name="readOnlyJson"> Whether to generate the database. </param>
        /// <param name="readPreviouslyUnpackedJson"> Whether to extract game files. </param>
        private static void InitializeHelpers(bool generateDatabase, bool readOnlyJson, bool readPreviouslyUnpackedJson)
        {
            var settingsTypes = new List<Type>
            {
                typeof(Settings),
                typeof(WpfSettings),
            };

            var requiredSettingNames = settingsTypes
                .SelectMany(settingsType => settingsType.GetProperties(BindingFlags.Public | BindingFlags.Static))
                .Where(settingProperty => settingProperty.GetCustomAttribute<RequiredSettingAttribute>() is RequiredSettingAttribute)
                .Select(settingProperty => settingProperty.Name);

            WindowFactory = new WindowFactory(Loggers);

            FileManager = new WarThunderFileManager(Loggers);
            FileReader = new WarThunderFileReader(Loggers);
            SettingsManager = new WpfClientSettingsManager(FileManager, EWpfClientFile.Settings, requiredSettingNames, Loggers);
            _parser = new Parser(Loggers);
            _unpacker = new Unpacker(FileManager, Loggers);
            _converter = new Converter(Loggers);
            _jsonHelper = new WarThunderJsonHelper(Loggers);
            _csvDeserializer = new CsvDeserializer(Loggers);
            _randomizer = new CustomRandomiserWithNormalisation(Loggers);
            _vehicleSelector = new VehicleSelector(_randomizer, Loggers);
            _presetGenerator = new PresetGenerator(_randomizer, _vehicleSelector, Loggers);

            Manager = new WpfClientManager
            (
                FileManager,
                FileReader,
                SettingsManager,
                _parser,
                _unpacker,
                _converter,
                _jsonHelper,
                _csvDeserializer,
                _randomizer,
                _vehicleSelector,
                _presetGenerator,
                generateDatabase,
                readOnlyJson,
                readPreviouslyUnpackedJson,
                Loggers
            );
            InitializeLocalizationManager();
        }

        /// <summary>
        /// Initializes the <see cref="LocalizationManager"/>.
        /// If the language stored in the <see cref="EWpfClientFile.Settings"/> file doesn't have a matching localization file, the language selection window is shown.
        /// </summary>
        private static void InitializeLocalizationManager()
        {
            try
            {
                Log.Debug(ECoreLogMessage.TryingToInitialize.FormatFluently(ELocalizationLogCategory.LocalizationManager));
                LocalizationManager = new LocalizationManager(FileReader, WpfSettings.Localization, Loggers);
            }
            catch (FileNotFoundException exception)
            {
                Log.Info(exception.Message);
                WindowFactory.CreateLocalizationWindow().ShowDialog();
            }
        }

        #endregion Methods: Initialization

        /// <summary> Creates and active logger. </summary>
        /// <param name="logCategory"> The category of logs generated by this instance. </param>
        /// <returns></returns>
        public static IActiveLogger CreateActiveLogger(string logCategory) =>
            new ActiveLogger(logCategory, Loggers);
    }
}