using Core.Enumerations;
using Core.Extensions;
using Core.Helpers;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.UnpackingToolsIntegration.Helpers
{
    /// <summary> Provides methods to manage files related to War Thunder. </summary>
    public class WarThunderFileManager : FileManager, IWarThunderFileManager
    {
        #region Constructors

        /// <summary> Creates a new database updater. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public WarThunderFileManager(params IConfiguredLogger[] loggers)
            : base(loggers)
        {
        }

        #endregion Constructors

        /// <summary> Gets names of all <see cref="EFileExtension.SqLite3"/> database files for specific game versions. </summary>
        /// <returns></returns>
        public IEnumerable<string> GetWarThunderDataBaseFileNames() =>
            new DirectoryInfo(Directory.GetCurrentDirectory())
                .GetFiles(file => file.GetExtensionWithoutPeriod() == EFileExtension.SqLite3 && file.GetNameWithoutExtension().Matches(ERegularExpression.VersionFull))
                .Select(file => file.Name)
            ;

        /// <summary> Removes all directories and files in <see cref="Settings.TempLocation"/>. </summary>
        public void CleanUpTempDirectory()
        {
            var tempDirectory = new DirectoryInfo(Settings.TempLocation);

            if (tempDirectory.Exists)
                EmptyDirectory(tempDirectory.FullName);
        }
    }
}