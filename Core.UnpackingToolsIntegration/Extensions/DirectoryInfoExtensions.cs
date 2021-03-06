﻿using Core.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.IO;

namespace Core.UnpackingToolsIntegration.Extensions
{
    /// <summary> Methods extending the <see cref="DirectoryInfo"/> class. </summary>
    public static class DirectoryInfoExtensions
    {
        /// <summary> Gets all <see cref="EFileExtension.SqLite3"/> database files for specific game versions. </summary>
        /// <param name="directory"> The directory to search in. </param>
        /// <returns></returns>
        public static IEnumerable<FileInfo> GetWarThunderDatabaseFiles(this DirectoryInfo directory) =>
            directory.GetFiles(file => file.GetExtensionWithoutPeriod() == EFileExtension.SqLite3 && file.GetNameWithoutExtension().Matches(ERegularExpression.VersionFull));
    }
}