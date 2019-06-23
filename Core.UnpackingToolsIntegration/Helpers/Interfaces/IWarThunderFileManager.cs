using Core.Helpers.Interfaces;
using System.Collections.Generic;

namespace Core.UnpackingToolsIntegration.Helpers.Interfaces
{
    public interface IWarThunderFileManager: IFileManager
    {
        /// <summary> Gets names of all <see cref="EFileExtension.SqLite3"/> database files for specific game versions. </summary>
        /// <returns></returns>
        IEnumerable<string> GetWarThunderDataBaseFileNames();

        /// <summary> Removes all directories and files in <see cref="Settings.TempLocation"/>. </summary>
        void CleanUpTempDirectory();
    }
}