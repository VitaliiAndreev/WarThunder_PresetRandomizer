using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="DirectoryInfo"/> class. </summary>
    public static class DirectoryInfoExtensions
    {
        /// <summary> Returns a collection of files fitting the given condition in the specified folder. </summary>
        /// <param name="directory"> The directory to look for files in. </param>
        /// <param name="condition"> The condition to check the files against. </param>
        /// <param name="searchOption"> The search option. </param>
        /// <returns></returns>
        public static IEnumerable<FileInfo> GetFiles(this DirectoryInfo directory, Predicate<FileInfo> condition, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var files = directory.GetFiles().Where(file => condition(file)).ToList();

            if (searchOption == SearchOption.AllDirectories)
                foreach (var subdirectory in directory.GetDirectories())
                {
                    files.AddRange(GetFiles(subdirectory, condition, searchOption));
                }

            return files;
        }
    }
}
