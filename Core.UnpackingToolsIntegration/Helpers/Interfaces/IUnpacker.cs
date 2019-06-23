using Core.WarThunderExtractionToolsIntegration;
using System.IO;

namespace Core.UnpackingToolsIntegration.Helpers.Interfaces
{
    /// <summary> Provides methods to unpack War Thunder files. </summary>
    public interface IUnpacker
    {
        #region Method: Unpack()

        /// <summary> Copies the file to unpack into the <see cref="Settings.TempLocation"/> and unpacks it. </summary>
        /// <param name="sourceFile"> The file to unpack. </param>
        /// <returns> The path to the output file / directory. </returns>
        string Unpack(FileInfo sourceFile, bool overwrite = false);

        /// <summary> Unpacks all valid files in the specified folder with the given tool. </summary>
        /// <param name="sourceDirectory"> The directory to unpack unpack files from. </param>
        /// <param name="toolName"> The name of the unpacking tool to use. </param>
        void Unpack(DirectoryInfo sourceDirectory, string toolName);

        #endregion Method: Unpack()
    }
}
