using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.Localization.Enumerations;
using Core.Localization.Enumerations.Logger;
using Core.Localization.Exceptions;
using Core.Localization.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Core.Localization.Helpers
{
    /// <summary> Provide methods to work with localization. </summary>
    public class LocalizationManager : LoggerFluency, ILocalizationManager
    {
        #region Fields

        private readonly IFileReader _fileReader;

        private readonly IDictionary<string, string> _localization;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new localization manager for a specified localization file and loads it into memory. </summary>
        /// <param name="fileReader"> The file reader to use. </param>
        /// <param name="localizationFileName"> The name of the localization file to load into memory. </param>
        /// <param name="loggers"> Instancs of loggers. </param>
        public LocalizationManager(IFileReader fileReader, string localizationFileName, params IConfiguredLogger[] loggers)
            : base(ELocalizationLogCategory.LocalizationManager, loggers)
        {
            _fileReader = fileReader;

            var localizationFile = new FileInfo(Path.Combine(EWord.Localization, $"{localizationFileName}{ECharacter.Period}{EFileExtension.Xml}"));

            if (!localizationFile.Exists)
                throw new FileNotFoundException(ECoreLogMessage.NotFound.FormatFluently(localizationFile.FullName));

            _localization = LoadLocalization(localizationFile);

            LogDebug(ECoreLogMessage.Created.FormatFluently(ELocalizationLogCategory.LocalizationManager));
        }

        #endregion Constructors

        /// <summary> Loads the specified localization file into memory. </summary>
        /// <param name="localizationFile"> The file to load. </param>
        /// <returns></returns>
        private IDictionary<string, string> LoadLocalization(FileInfo localizationFile)
        {
            var fileContents = _fileReader.Read(localizationFile);

            return XElement
                .Parse(fileContents)
                .Elements(EWord.Line)
                .ToDictionary
                (
                    element => (string)element.Attribute(EWord.Key_L),
                    element => (string)element.Attribute(EWord.Value_L)
                )
            ;
        }

        /// <summary> Returns a localized string by its key. </summary>
        /// <param name="key"> The key of the localized string. </param>
        /// <returns></returns>
        public string GetLocalizedString(string key)
        {
            if (_localization.ContainsKey(key))
                return _localization[key];

            return key;
        }
    }
}