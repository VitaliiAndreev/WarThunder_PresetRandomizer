using Core.WarThunderExtractionToolsIntegration;
using System.IO;

namespace Core.UnpackingToolsIntegration.Helpers.Interfaces
{
    /// <summary> Provides methods to unpack War Thunder files. </summary>
    public interface IUnpacker
    {
        /// <summary> Copies the file to unpack into the <see cref="Settings.TempLocation"/> and unpacks it. </summary>
        /// <param name="sourceFile"> The file to unpack. </param>
        /// <returns> The path to the output file / directory. </returns>
        string Unpack(FileInfo sourceFile);
    }
}
