using Core.Enumerations;
using System.IO;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="FileInfo"/> class. </summary>
    public static class FileInfoExtensions
    {
        /// <summary> Get the file name without an extension (if any). </summary>
        /// <param name="fileInfo"> A source file information instance. </param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(this FileInfo fileInfo) =>
            fileInfo.Name.Contains(ECharacter.Period)
                ? fileInfo.Name.Split(ECharacter.Period).SkipLast(1).StringJoin(ECharacter.Period)
                : fileInfo.Name;
    }
}
