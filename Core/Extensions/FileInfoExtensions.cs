using System.IO;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="FileInfo"/> class. </summary>
    public static class FileInfoExtensions
    {
        /// <summary> Get the file name without an extension (if any). </summary>
        /// <param name="fileInfo"> A source file information instance. </param>
        /// <returns></returns>
        public static string GetExtensionWithoutPeriod(this FileInfo fileInfo) =>
            fileInfo
                .Extension
                .Substring(Integer.Number.One)
            ;

        /// <summary> Get the file name without an extension (if any). </summary>
        /// <param name="fileInfo"> A source file information instance. </param>
        /// <returns></returns>
        public static string GetNameWithoutExtension(this FileInfo fileInfo) =>
            Path.GetFileNameWithoutExtension(fileInfo.Name);
    }
}