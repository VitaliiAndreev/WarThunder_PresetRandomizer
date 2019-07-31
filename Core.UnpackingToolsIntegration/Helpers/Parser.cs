﻿using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Exceptions;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.UnpackingToolsIntegration.Helpers
{
    /// <summary> Provides methods to parse file contents. </summary>
    public class Parser : LoggerFluency, IParser
    {
        #region Constructors

        /// <summary> Creates a new parser. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public Parser(params IConfiguredLogger[] loggers)
            : base(ECoreLogCategory.Parser, loggers)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(ECoreLogCategory.Parser));
        }

        #endregion Constructors

        /// <summary> Reads the client version from .yup file contents. </summary>
        /// <param name="rawFileContents"> A string to read from. </param>
        /// <returns></returns>
        public Version GetClientVersion(string rawFileContents)
        {
            LogDebug(ECoreLogMessage.ReadingClientVersion);

            var versionString = default(string);
            var version = default(Version);

            try
            {
                var regularExpression = new Regex("(version" + ERegularExpression.AtLeastOneNumber + ":" + ERegularExpression.VersionFull + ":yup_version)");
                var match = regularExpression.Match(rawFileContents);

                if (match is null)
                    throw new YupFileParsingException(ECoreLogMessage.VersionNotFoundInSourceString);

                var matchStrings = match.Value.Split(ECharacter.Colon);

                var versionParameterNumberString = matchStrings.First().Where(character => character.IsDigitFluently()).StringJoin();
                var versionParameterNumber = int.Parse(versionParameterNumberString);

                var versionStringRaw = matchStrings[1];

                var yupVersionParameterNumberString = default(string);
                var yupVersionParameterNumberLength = 0;
                var yupVersionParameterNumber = default(int);

                do
                {
                    yupVersionParameterNumberString = versionStringRaw.TakeLast(++yupVersionParameterNumberLength);
                    yupVersionParameterNumber = int.Parse(yupVersionParameterNumberString);

                } while (yupVersionParameterNumber <= versionParameterNumber);

                versionString = versionStringRaw.SkipLast(yupVersionParameterNumberString.Count());
            }
            catch (Exception exception)
            {
                LogError(ECoreLogMessage.ErrorReadingRawInstallData, exception);
                throw;
            }

            try
            {
                version = new Version(versionString);
            }
            catch (Exception exception)
            {
                LogError(ECoreLogMessage.ErrorParsingVersionString, exception);
                throw;
            }

            LogDebug(ECoreLogMessage.ClientVersionIs.FormatFluently(version.ToString(4)));
            return version;
        }
    }
}