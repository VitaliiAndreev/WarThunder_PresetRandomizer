using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="DirectoryInfo"/> class. </summary>
    public static class DirectoryInfoExtensions
    {
        public static IEnumerable<DirectoryInfo> GetDirectories(this DirectoryInfo directory, SearchOption searchOption)
        {
            return directory.GetDirectories("*", searchOption);
        }

        public static IEnumerable<FileInfo> GetFiles(this DirectoryInfo directory, SearchOption searchOption)
        {
            return directory.GetFiles("*", searchOption);
        }

        /// <summary> Returns a collection of files fitting the given condition in the specified folder. </summary>
        /// <param name="directory"> The directory to look for files in. </param>
        /// <param name="condition"> The condition to check the files against. </param>
        /// <param name="searchOption"> The search option. </param>
        /// <returns></returns>
        public static IEnumerable<FileInfo> GetFiles(this DirectoryInfo directory, Predicate<FileInfo> condition, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return directory.GetFiles("*", searchOption).Where(file => condition(file));
        }
    }
}
