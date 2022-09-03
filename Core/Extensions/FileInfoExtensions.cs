using System.IO;

namespace Core
{
    public static class FileInfoExtensions
    {
        public static string GetExtensionWithoutPeriod(this FileInfo fileInfo)
        {
            var extension = fileInfo.Extension;

            return string.IsNullOrEmpty(extension)
                ? string.Empty
                : extension.Substring(1);
        }
    }
}