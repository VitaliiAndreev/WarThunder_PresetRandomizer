using System.IO;

namespace Core
{
    public static class FileInfoExtensions
    {
        /// <summary> Get the file name without an extension (if any). </summary>
        /// <param name="fileInfo"> A source file information instance. </param>
        /// <returns></returns>
        public static string GetExtensionWithoutPeriod(this FileInfo fileInfo) =>
            fileInfo
                .Extension
                .Substring(1)
            ;

        /// <summary> Get the file name without an extension (if any). </summary>
        /// <param name="fileInfo"> A source file information instance. </param>
        /// <returns></returns>
        public static string GetNameWithoutExtension(this FileInfo fileInfo) =>
            Path.GetFileNameWithoutExtension(fileInfo.Name);
    }
}