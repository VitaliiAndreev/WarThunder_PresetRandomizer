using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Enumerations;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Exceptions;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using System;
using System.Linq;

namespace Core.UnpackingToolsIntegration.Helpers
{
    /// <summary> Provides methods to parse file contents. </summary>
    public class Parser : LoggerFluency, IParser
    {
        #region Constructors

        /// <summary> Creates a new parser. </summary>
        /// <param name="logger"> An instance of a logger. </param>
        public Parser(IConfiguredLogger logger)
            : base(logger, ECoreLogCategory.Parser)
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
                versionString = rawFileContents.Split("version9:").LastOrDefault()?.Split("11:yup_version")?.FirstOrDefault();

                if (versionString is null)
                    throw new YupFileParsingException(ECoreLogMessage.ErrorVersionNotFoundInSourceString);
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
