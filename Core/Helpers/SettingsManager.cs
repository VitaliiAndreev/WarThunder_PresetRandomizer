using Core.Exceptions;
using Core.Extensions;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Core.Helpers
{
    /// <summary> Handles work with settings files. </summary>
    public class SettingsManager : LoggerFluency, ISettingsManager
    {
        #region Constants

        /// <summary> A template for settings expressions. </summary>
        protected const string _settingsExpressionTemplate = CharacterString.Slash + "Settings" + CharacterString.Slash + "{0}";

        #endregion Constants
        #region Fields

        /// <summary> Names of required settings. </summary>
        private readonly IEnumerable<string> _requiredSettingNames;
        /// <summary> Properties that match requirements. </summary>
        private readonly IDictionary<string, string> _settings;

        /// <summary> An instance of a file manager. </summary>
        protected readonly IFileManager _fileManager;

        /// <summary> The settings file. </summary>
        protected FileInfo _settingsFile;

        #endregion Fields
        #region Properties

        /// <summary> The status of the settings file after initialization. </summary>
        public SettingsFileStatus SettingsFileStatus { get; private set; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new settings manager. </summary>
        /// <param name="settingsFileName"> The name of the settings file to attach to this manager. </param>
        /// <param name="fileManager"> An instance of a file manager. </param>
        /// <param name="requiredSettingNames"> Names of required settings. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public SettingsManager(IFileManager fileManager, string settingsFileName, IEnumerable<string> requiredSettingNames, params IConfiguredLogger[] loggers)
            : base(CoreLogCategory.SettingsManager, loggers)
        {
            _fileManager = fileManager;
            _requiredSettingNames = requiredSettingNames;
            _settingsFile = new FileInfo(settingsFileName);
            _settings = new Dictionary<string, string>();

            SettingsFileStatus = SettingsFileStatus.Pending;
            Initialize();

            LogDebug(CoreLogMessage.Created.Format(CoreLogCategory.SettingsManager));
        }

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Initializes the settings manager. </summary>
        private void Initialize()
        {
            ValidateFileExistence();
            ValidateFileSettings();
        }

        /// <summary> Checks that the settings file exists and generates an empty one if not. </summary>
        private void ValidateFileExistence()
        {
            if (!_settingsFile.Exists)
            {
                LogInfo(CoreLogMessage.SettingsFileNotFound_CreatingNewOne.Format(_settingsFile.Name));

                GenerateSettingsFile();

                LogInfo(CoreLogMessage.Created.Format(Word.File));
                SettingsFileStatus = SettingsFileStatus.NotFoundAndGenerated;
            }
            _settings.ReplaceBy(GetSettingsFromFile());
        }

        /// <summary>
        /// Checks the settings file for required settings.
        /// If not all required settings are present, a backup is made, valid settings are moved over, and an automated resolution is attempted.
        /// In an event of only one required setting missing with one setting in the file unrecognized, the two settings are considered related and the situation is resolved.
        /// Otherwise direct intervention is necessary to move unrecognized settings over to the newly generated file.
        /// </summary>
        private void ValidateFileSettings()
        {
            var missingSettingNames = _requiredSettingNames.Except(_settings.Keys);
            var recognizedSettingNames = _requiredSettingNames.Intersect(_settings.Keys);
            var unrecognizedSettingNames = _settings.Keys.Except(recognizedSettingNames);

            if (missingSettingNames.Any())
            {
                _fileManager.BackUpFile(_settingsFile);
                GenerateSettingsFile();

                foreach (var recognizedSettingName in recognizedSettingNames)
                    Save(recognizedSettingName, _settings[recognizedSettingName]);

                if (missingSettingNames.HasSingle() && unrecognizedSettingNames.HasSingle()) // It is assumed that a setting was renamed.
                {
                    var missingSettingName = missingSettingNames.First();
                    var unrecognizedSettingName = unrecognizedSettingNames.First();
                    var settingValue = _settings[unrecognizedSettingName];

                    Save(missingSettingName, settingValue);

                    _settings.Add(missingSettingName, settingValue);
                    _settings.Remove(unrecognizedSettingName);

                    SettingsFileStatus = SettingsFileStatus.FoundAndAutomaticallyUpdated;
                }
                else
                {
                    SettingsFileStatus = SettingsFileStatus.FoundAndNeedsManualUpdate;

                    throw new SettingsFileRegeneratedException();
                }
            }
            else
            {
                SettingsFileStatus = SettingsFileStatus.FoundAndUpToDate;
            }
        }

        /// <summary> Generates an empty settings file. </summary>
        private void GenerateSettingsFile()
        {
            _settingsFile = new FileInfo(_settingsFile.Name);
            _settingsFile.Create().Close();

            using var xmlTextWriter = new XmlTextWriter(_settingsFile.FullName, Encoding.UTF8);
            {
                xmlTextWriter.WriteStartElement(Word.Settings);
                {
                    foreach (var requiredSetting in _requiredSettingNames)
                    {
                        xmlTextWriter.WriteStartElement(requiredSetting);
                        xmlTextWriter.WriteEndElement();
                    }
                }
                xmlTextWriter.WriteEndElement();
            }
        }

        #endregion Methods: Initialization
        #region Methods: Reading

        /// <summary> Reads the <see cref="_settingsFile"/> and extracts a dictionary of settings. </summary>
        /// <returns></returns>
        private IDictionary<string, string> GetSettingsFromFile()
        {
            var settings = new Dictionary<string, string>();
            var xmlDocument = ReadFile();
            var nodes = xmlDocument
                .ChildNodes
                .OfType<XmlNode>()
                .FirstOrDefault(node => node.Name == Word.Settings)
                .ChildNodes
                .OfType<XmlNode>()
            ;

            foreach (var node in nodes)
                settings.Add(node.Name, node.InnerText);

            return settings;
        }

        /// <summary> Reads the <see cref="_settingsFile"/> as an XML document. </summary>
        /// <returns></returns>
        private XmlDocument ReadFile()
        {
            var xmlDocument = new XmlDocument();

            using (var xmlTextReader = new XmlTextReader(_settingsFile.FullName))
                xmlDocument.Load(xmlTextReader);

            return xmlDocument;
        }

        /// <summary> Returns the setting with the specified name. </summary>
        /// <param name="settingName"> The name of the setting to get. </param>
        /// <returns></returns>
        public string GetSetting(string settingName)
        {
            LogErrorAndThrowIfSettingsNotInitialized();
            return _settings[settingName];
        }

        #endregion Methods: Reading
        #region Methods: Writing

        /// <summary> Saves the <paramref name="newValue"/> of the setting with the specified name to the settings file. </summary>
        /// <param name="settingName"> The name of the XML node to write <paramref name="newValue"/> to. </param>
        /// <param name="newValue"> The value to save. </param>
        /// <returns></returns>
        public virtual void Save(string settingName, string newValue)
        {
            LogErrorAndThrowIfSettingsNotInitialized();

            var document = ReadFile();
            var rootElement = document.DocumentElement;

            var oldNode = rootElement.SelectSingleNode(_settingsExpressionTemplate.Format(settingName));

            if (oldNode is null)
                throw new XmlException(CoreLogMessage.XmlNodeNotFound.Format(settingName));

            oldNode.InnerText = newValue;

            document.Save(_settingsFile.FullName);
            _settings[settingName] = newValue;
        }

        #endregion Methods: Writing

        /// <summary> Throws a <see cref="NotInitializedException"/>. </summary>
        private void LogErrorAndThrowIfSettingsNotInitialized()
        {
            if (_settings is null || _settings.IsEmpty())
                LogErrorAndThrow<NotInitializedException>(CoreLogMessage.NotInitialisedProperly.Format(CoreLogCategory.SettingsManager), CoreLogMessage.SettingsCacheIsEmpty);
        }
    }
}