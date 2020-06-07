using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Helpers;
using Client.Wpf.Helpers.Interfaces;
using Core.Csv.WarThunder.Helpers;
using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Helpers;
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
using Core.Randomization.Helpers.Interfaces;
using Core.UnpackingToolsIntegration.Attributes;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using Core.Web.WarThunder.Helpers;
using Core.Web.WarThunder.Helpers.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Threading;

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
        private static IDataRepositoryFactory _dataRepositoryFactory;
        private static IRandomiser _randomizer;
        private static IVehicleSelector _vehicleSelector;
        private static IPresetGenerator _presetGenerator;
        private static IThunderSkillParser _thunderSkillParser;

        /// <summary> Indicates whether loggers have been initialized. </summary>
        private static bool _loggersInitialised;
        /// <summary> Indicates whether helpers have been initialized. </summary>
        private static bool _helpersInitialised;

        #endregion Fields
        #region Properties

        public static IConfiguredLogger[] Loggers { get; private set; }
        public static IWarThunderFileManager FileManager { get; private set; }
        public static IWarThunderFileReader FileReader { get; private set; }
        public static IWpfClientSettingsManager SettingsManager { get; private set; }
        public static IWpfClientManager Manager { get; private set; }
        public static ILocalisationManager LocalisationManager { get; internal set; }

        /// <summary> An instance of a window factory. </summary>
        public static IWindowFactory WindowFactory { get; private set; }
        /// <summary> An instance of an active logger. </summary>
        public static IActiveLogger Log { get; private set; }

        #endregion Properties
        #region Constructors

        /// <summary> The static constructor is used for setting default values. </summary>
        static ApplicationHelpers()
        {
            _helpersInitialised = false;
            _loggersInitialised = false;
        }

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Initializes NLog-based loggers. Repeated initialization is being skipped. </summary>
        public static void InitialiseLoggers()
        {
            if (_loggersInitialised) return;

            var fileLogger = new ConfiguredNLogger(ELoggerName.FileLogger, new ExceptionFormatter(), ESubdirectory.Logs, true);

            var wpfLogger = new ConfiguredNLogger(ELoggerName.WpfLogger, new ExceptionFormatter());
            wpfLogger.LogInstantiation(fileLogger);

            Loggers = new IConfiguredLogger[]
            {
                fileLogger,
                wpfLogger,
            };

            Log = CreateActiveLogger(EWpfClientLogCategory.ApplicationHelpers);

            Log.Debug(ECoreLogMessage.InstanceInitialised.FormatFluently(EWord.Loggers));
            _loggersInitialised = true;
        }

        /// <summary> Initialises fields and properties. Repeated initialisation is being skipped. </summary>
        /// <param name="generateDatabase"> Whether to read data from JSON instead of the database. </param>
        /// <param name="readOnlyJson"> Whether to generate the database. </param>
        /// <param name="readPreviouslyUnpackedJson"> Whether to extract game files. </param>
        public static void Initialise(bool generateDatabase, bool readOnlyJson, bool readPreviouslyUnpackedJson)
        {
            if (!_loggersInitialised)
                InitialiseLoggers();

            if (_helpersInitialised)
                return;

            Log.Debug(ECoreLogMessage.InitialisingInstance.FormatFluently(EWord.Helpers.ToLower()));

            InitialiseHelpers(generateDatabase, readOnlyJson, readPreviouslyUnpackedJson);

            Log.Debug(ECoreLogMessage.InstanceInitialised.FormatFluently(EWord.Helpers));
            _helpersInitialised = true;
        }

        /// <summary> Initialises helpers. </summary>
        /// <param name="generateDatabase"> Whether to read data from JSON instead of the database. </param>
        /// <param name="readOnlyJson"> Whether to generate the database. </param>
        /// <param name="readPreviouslyUnpackedJson"> Whether to extract game files. </param>
        private static void InitialiseHelpers(bool generateDatabase, bool readOnlyJson, bool readPreviouslyUnpackedJson)
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
            _dataRepositoryFactory = new DataRepositoryFactoryWarThunder(Loggers);
            _converter = new Converter(Loggers);
            _jsonHelper = new WarThunderJsonHelper(Loggers);
            _csvDeserializer = new CsvDeserializer(Loggers);
            _randomizer = new CustomRandomiserWithNormalisation(Loggers);
            _vehicleSelector = new VehicleSelector(_randomizer, Loggers);
            _presetGenerator = new PresetGenerator(_randomizer, _vehicleSelector, Loggers);
            _thunderSkillParser = new ThunderSkillParser(Loggers);

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
                _dataRepositoryFactory,
                _randomizer,
                _vehicleSelector,
                _presetGenerator,
                _thunderSkillParser,
                generateDatabase,
                readOnlyJson,
                readPreviouslyUnpackedJson,
                Loggers
            );
            InitialiseLocalisationManager();
        }

        /// <summary>
        /// Initialises the <see cref="LocalisationManager"/>.
        /// If the language stored in the <see cref="EWpfClientFile.Settings"/> file doesn't have a matching localisation file, the language selection window is shown.
        /// </summary>
        private static void InitialiseLocalisationManager()
        {
            try
            {
                Log.Debug(ECoreLogMessage.TryingToInitialise.FormatFluently(ELocalisationLogCategory.LocalisationManager));
                LocalisationManager = new LocalisationManager(FileReader, WpfSettings.Localization, Loggers);
            }
            catch (FileNotFoundException exception)
            {
                Log.Info(exception.Message);
                WindowFactory.CreateLocalisationWindow().ShowDialog();
            }
        }

        #endregion Methods: Initialization

        /// <summary> Creates and active logger. </summary>
        /// <param name="logCategory"> The category of logs generated by this instance. </param>
        /// <returns></returns>
        public static IActiveLogger CreateActiveLogger(string logCategory) =>
            new ActiveLogger(logCategory, Loggers);

        public static void ProcessUiTasks()
        {
            var frame = new DispatcherFrame();

            Dispatcher.CurrentDispatcher.BeginInvoke
            (
                DispatcherPriority.Background,
                new DispatcherOperationCallback(delegate (object parameter) { frame.Continue = false; return null; }),
                null
            );

            try { Dispatcher.PushFrame(frame); }
            catch { }
        }
    }
}