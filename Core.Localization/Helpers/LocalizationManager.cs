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

        public LocalizationManager(IFileReader fileReader, string languageName, params IConfiguredLogger[] loggers)
            : base(ELocalizationLogCategory.LocalizationManager, loggers)
        {
            _fileReader = fileReader;

            if (!Enum.TryParse<ELocalization>(languageName, out var language))
                throw new LanguageNotRecognizedException(ELocalizationLogMessage.LocalizationLanguageNotRecognized.FormatFluently(languageName));

            _localization = LoadLocalization(language);

            LogDebug(ECoreLogMessage.Created.FormatFluently(ELocalizationLogCategory.LocalizationManager));
        }

        #endregion Constructors

        private IDictionary<string, string> LoadLocalization(ELocalization language)
        {
            var fileContents = _fileReader.Read(Path.Combine(EWord.Localization, $"{language}{ECharacter.Period}{EFileExtension.Xml}"));

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