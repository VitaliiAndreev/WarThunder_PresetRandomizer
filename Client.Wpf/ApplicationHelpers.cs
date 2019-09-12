using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Helpers;
using Client.Wpf.Helpers.Interfaces;
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
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System.Collections.Generic;
using System.IO;

namespace Client.Wpf
{
    /// <summary> Stores instances of helpers to be available project-wide. </summary>
    public static class ApplicationHelpers
    {
        #region Fields

        private static IParser _parser;
        private static IUnpacker _unpacker;
        private static IWarThunderJsonHelper _jsonHelper;
        private static IRandomizer _randomizer;
        private static IVehicleSelector _vehicleSelector;

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

            var fileLogger = new ConfiguredNLogger(ELoggerName.FileLogger, new ExceptionFormatter(), true);

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
        public static void Initialize()
        {
            if (!_loggersInitialized)
                InitializeLoggers();

            if (_helpersInitialized)
                return;

            Log.Debug(ECoreLogMessage.Initializing.FormatFluently(EWord.Helpers.ToLower()));

            InitializeHelpers();

            Log.Debug(ECoreLogMessage.ObjectInitialized.FormatFluently(EWord.Helpers));
            _helpersInitialized = true;
        }

        /// <summary> Initializes helpers. </summary>
        private static void InitializeHelpers()
        {
            var requiredSettings = new List<string>
            {
                nameof(WpfSettings.Localization),
                nameof(Settings.WarThunderLocation),
                nameof(Settings.KlensysWarThunderToolsLocation),
            };

            WindowFactory = new WindowFactory(Loggers);

            FileManager = new WarThunderFileManager(Loggers);
            FileReader = new WarThunderFileReader(Loggers);
            SettingsManager = new WpfClientSettingsManager(FileManager, EWpfClientFile.Settings, requiredSettings, Loggers);
            _parser = new Parser(Loggers);
            _unpacker = new Unpacker(FileManager, Loggers);
            _jsonHelper = new WarThunderJsonHelper(Loggers);
            _randomizer = new CustomRandomizer(Loggers);
            _vehicleSelector = new VehicleSelector(_randomizer, Loggers);

            Manager = new WpfClientManager(FileManager, FileReader, SettingsManager, _parser, _unpacker, _jsonHelper, _randomizer, _vehicleSelector, Loggers);
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