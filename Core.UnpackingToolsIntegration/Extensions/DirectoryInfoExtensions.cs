using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.UnpackingToolsIntegration.Extensions
{
    public static class DirectoryInfoExtensions
    {
        /// <summary> Gets all <see cref="FileExtension.SqLite3"/> database files for specific game versions. </summary>
        /// <param name="directory"> The directory to search in. </param>
        /// <returns></returns>
        public static IEnumerable<FileInfo> GetWarThunderDatabaseFiles(this DirectoryInfo directory) =>
            directory
                .GetFiles("*", SearchOption.TopDirectoryOnly)
                .Where(file => 
                    file.GetExtensionWithoutPeriod() == FileExtension.SqLite3 &&
                    Path.GetFileNameWithoutExtension(file.Name).Matches(RegularExpressionPattern.VersionFull));
    }
}