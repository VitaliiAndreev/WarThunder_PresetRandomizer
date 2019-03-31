using System.Collections.Generic;

namespace Core.Helpers.Interfaces
{
    /// <summary> Provides methods to manage files. </summary>
    public interface IFileManager
    {
        #region DeleteFiles()

        /// <summary> Deletes all files in a folder. </summary>
        /// <param name="path"> The path to a folder. </param>
        /// <param name="includeNested"> Whether to delete files in all nested directories. </param>
        /// <param name="deleteEmptyDirectories"> Whether to delete directories if they are empty after file deletion regardless of whether they had files prior. </param>
        void DeleteFiles(string path, bool includeNested = false, bool deleteEmptyDirectories = false);

        /// <summary> Deletes all files with the specified extension in a folder. </summary>
        /// <param name="path"> The path to a folder. </param>
        /// <param name="fileExtension"> A collection of file extensions to delete from the folder. </param>
        void DeleteFiles(string path, params string[] fileExtensions);

        /// <summary> Deletes all files with specified extensions in a folder. </summary>
        /// <param name="path"> The path to a folder. </param>
        /// <param name="fileExtension"> A collection of file extensions to delete from the folder. </param>
        /// <param name="includeNested"> Whether to delete files in all nested directories. </param>
        /// <param name="deleteEmptyDirectories"> Whether to delete directories if they are empty after file deletion regardless of whether they had files prior. </param>
        void DeleteFiles(string path, IEnumerable<string> fileExtensions, bool includeNested = false, bool deleteEmptyDirectories = false);

        #endregion DeleteFiles()

        /// <summary> Empties the specified directory. </summary>
        /// <param name="path"> The path to a directory. </param>
        void EmptyDirectory(string path);

        /// <summary> Deletes the specified directory. </summary>
        /// <param name="path"> The path to a directory. </param>
        void DeleteDirectory(string path);
    }
}
