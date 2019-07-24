using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Core.Helpers
{
    public class SettingsManager : LoggerFluency, ISettingsManager
    {
        #region Constants

        /// <summary> A template for settings expressions. </summary>
        protected const string _settingsExpressionTemplate = ECharacterString.Slash + "Settings" + ECharacterString.Slash + "{0}";

        #endregion Constants
        #region Fields

        /// <summary> The settings file. </summary>
        protected readonly FileInfo _settingsFile;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new file manager. </summary>
        /// <param name="settingsFileName"> The name of the settings file to attach to this manager. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public SettingsManager(string settingsFileName, params IConfiguredLogger[] loggers)
            : base(ECoreLogCategory.SettingsManager, loggers)
        {
            _settingsFile = new FileInfo(settingsFileName);
            
            if (!_settingsFile.Exists)
                throw new FileNotFoundException(ECoreLogMessage.NotFound.FormatFluently(settingsFileName));

            LogDebug(ECoreLogMessage.Created.FormatFluently(ECoreLogCategory.SettingsManager));
        }

        #endregion Constructors
        #region Methods: Reading

        /// <summary> Reads the <see cref="_settingsFile"/> as an XML document. </summary>
        /// <returns></returns>
        private XmlDocument Read()
        {
            var xmlDocument = new XmlDocument();

            using (var xmlTextReader = new XmlTextReader(_settingsFile.FullName))
                xmlDocument.Load(xmlTextReader);

            return xmlDocument;
        }

        /// <summary> Loads the setting with the specified name. </summary>
        /// <param name="settingName"> The name of the setting to read. </param>
        /// <returns></returns>
        public string Load(string settingName)
        {
            var navigator = new XPathDocument(_settingsFile.FullName).CreateNavigator();

            var expression = navigator.Compile(_settingsExpressionTemplate.FormatFluently(settingName));
            var iterator = navigator.Select(expression);

            while (iterator.MoveNext())
                return iterator.Current.Value;

            throw new XmlException(ECoreLogMessage.XmlNodeNotFound.FormatFluently(settingName));
        }

        #endregion Methods: Reading
        #region Methods: Writing

        /// <summary> Saves the <paramref name="newValue"/> of the setting with the specified name. </summary>
        /// <param name="settingName">Node of XML to read</param>
        /// <param name="newValue">Value to write to that node</param>
        /// <returns></returns>
        public virtual void Save(string settingName, string newValue)
        {
            var document = Read();
            var rootElement = document.DocumentElement;

            var oldNode = rootElement.SelectSingleNode(_settingsExpressionTemplate.FormatFluently(settingName));

            if (oldNode is null)
                throw new XmlException(ECoreLogMessage.XmlNodeNotFound.FormatFluently(settingName));

            oldNode.InnerText = newValue;

            document.Save(_settingsFile.FullName);
        }

        #endregion Methods: Writing
    }
}