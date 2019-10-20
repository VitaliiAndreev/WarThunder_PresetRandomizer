using System;
using System.Collections.Generic;
using System.IO;

namespace Core.Helpers.Interfaces
{
    /// <summary> Provides methods to manage files. </summary>
    public interface IFileManager
    {
        #region Methods: Copying

        /// <summary> Copies a file into a specified directory. </summary>
        /// <param name="filePath"> The absolute name of the file. </param>
        /// <param name="destination"> The destination path. </param>
        /// <param name="overwrite"> Whether to overwrite colliding files at the destination. </param>
        /// <param name="createDirectories"> Whether to create the destination directory. </param>
        void CopyFile(string filePath, string destination, bool overwrite = false, bool createDirectories = false);

        /// <summary> Copies a file into a specified directory. </summary>
        /// <param name="file"> The file to copy. </param>
        /// <param name="destination"> The destination path. </param>
        /// <param name="overwrite"> Whether to overwrite colliding files at the destination. </param>
        /// <param name="createDirectories"> Whether to create the destination directory. </param>
        void CopyFile(FileInfo file, string destination, bool overwrite = false, bool createDirectories = false);

        /// <summary> Creates a backup copy of the given file, with its name appended with ".bak". </summary>
        /// <param name="file"> The file to back up. </param>
        void BackUpFile(FileInfo file);

        #endregion Methods: Copying
        #region Methods: Deletion

        /// <summary> Deletes the file with the specified name if it exists. </summary>
        /// <param name="fileName"> The name of the file to delete. </param>
        void DeleteFileSafely(string fileName);

        /// <summary> Deletes the specified file if it exists. </summary>
        /// <param name="file"> The file to delete. </param>
        void DeleteFileSafely(FileInfo file);

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

        /// <summary> Deletes all files (non-recursively) older than the given timestamp. </summary>
        /// <param name="path"> The path to files. </param>
        /// <param name="deletionDeadline"> The timestamp before which all files should be deleted. </param>
        void DeleteOldFiles(string path, DateTime deletionDeadline);

        #endregion Methods: Deletion
        #region Methods: Fluency

        /// <summary> Creates a new instance of <see cref="FileInfo"/> with a directory path and a file name. </summary>
        /// <param name="directoryPath"> The directory path. </param>
        /// <param name="fileName"> The file name. </param>
        /// <returns></returns>
        FileInfo GetFileInfo(string directoryPath, string fileName);

        #endregion Methods: Fluency
    }
}
