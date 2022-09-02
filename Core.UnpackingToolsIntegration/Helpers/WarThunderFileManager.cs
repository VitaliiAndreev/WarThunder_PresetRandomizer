using Core.Enumerations;
using Core.Extensions;
using Core.Helpers;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Extensions;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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

        /// <summary> Removes all directories and files in <see cref="Settings.TempLocation"/>. </summary>
        public void CleanUpTempDirectory()
        {
            if (Settings.TempLocation is null)
                return;

            var tempDirectory = new DirectoryInfo(Settings.TempLocation);

            if (tempDirectory.Exists)
                EmptyDirectory(tempDirectory.FullName);
        }

        /// <summary> Gets all <see cref="FileExtension.SqLite3"/> database files for specific game versions. </summary>
        /// <returns></returns>
        private IEnumerable<FileInfo> GetWarThunderDatabaseFiles() =>
            new DirectoryInfo(Directory.GetCurrentDirectory()).GetWarThunderDatabaseFiles();

        /// <summary> Gets names of all <see cref="FileExtension.SqLite3"/> database files for specific game versions. </summary>
        /// <returns></returns>
        public IEnumerable<string> GetWarThunderDatabaseFileNames() =>
            GetWarThunderDatabaseFiles().Select(file => file.Name);

        /// <summary> Gets all client versions for which a database is found. </summary>
        /// <returns></returns>
        public IEnumerable<Version> GetWarThunderDatabaseVersions() =>
            GetWarThunderDatabaseFiles().Select(file => new Version(file.GetNameWithoutExtension()));

        /// <summary> Checks whether the directory with the specified path has all required files as listed with public constants in the given type (See <see cref="EFile"/>). </summary>
        /// <param name="path"> The path of the directory to validate. </param>
        /// <param name="constantType"> The type that contains public constants whose values correspond with required files. </param>
        /// <returns></returns>
        public bool LocationIsValid(string path, Type constantType)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                LogWarn($"The argument \"{nameof(path)}\" is empty: \"{path}\"");
                return false;
            }
            if (Path.GetInvalidPathChars().ToList().Intersect(path) is IEnumerable<char> invalidCharacters && invalidCharacters.Any())
            {
                LogWarn($"The argument \"{nameof(path)}\" contains invalid characters: \"{invalidCharacters.StringJoin(", ")}\"");
                return false;
            }
            if (!Path.IsPathRooted(path))
            {
                LogWarn($"The argument \"{nameof(path)}\" is not absolute: \"{path}\"");
                return false;
            }
            
            var directory = new DirectoryInfo(path);
            
            if (!directory.Exists)
            {
                LogWarn($"The argument \"{nameof(path)}\" points to a folder that doesn't exist.");
                return false;
            }

            var filePaths = directory.GetFiles().Select(file => file.Name);
            var requiredFilePaths = constantType
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(field => field.IsLiteral && !field.IsInitOnly)
                .Select(constant => constant.GetRawConstantValue().ToString())
            ;

            foreach (var requiredFilePath in requiredFilePaths)
            {
                if (!requiredFilePath.IsIn(filePaths))
                {
                    LogWarn($"The file \"{requiredFilePath}\" is missing from \"{path}\".");
                    return false;
                }
            }
            return true;
        }

        /// <summary> Checks whether the directory with the specified path contains files required from War Thunder. </summary>
        /// <param name="path"> The path of the directory to validate. </param>
        /// <returns></returns>
        public bool WarThunderLocationIsValid(string path) =>
            LocationIsValid(path, typeof(EFile.WarThunder))
            && LocationIsValid(Path.Combine(path, EDirectory.WarThunder.Subdirectory.Ui), typeof(EFile.WarThunderUi));

        /// <summary> Checks whether the directory with the specified path contains files required from Klensy's War Thunder Tools. </summary>
        /// <param name="path"> The path of the directory to validate. </param>
        /// <returns></returns>
        public bool KlensysWarThunderToolLocationIsValid(string path) => LocationIsValid(path, typeof(ETool));
    }
}