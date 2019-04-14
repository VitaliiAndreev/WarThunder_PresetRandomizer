using Core.WarThunderExtractionToolsIntegration;
using System.IO;

namespace Core.UnpackingToolsIntegration.Helpers.Interfaces
{
    /// <summary> Provides methods to unpack War Thunder files. </summary>
    public interface IUnpacker
    {
        /// <summary> Copies both the unpacking tool (because the output folder is relative to it) and the file to unpack into the <see cref="Settings.TempLocation"/> and unpacks the file. </summary>
        /// <param name="sourceFile"> The file to unpack. </param>
        /// <returns></returns>
        DirectoryInfo Unpack(FileInfo sourceFile);
    }
}
