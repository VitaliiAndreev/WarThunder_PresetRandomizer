using Core.Helpers.Interfaces;
using Core.UnpackingToolsIntegration.Enumerations;

namespace Core.UnpackingToolsIntegration.Helpers.Interfaces
{
    /// <summary> Provides methods to read War Thunder files. </summary>
    public interface IWarThunderFileReader : IFileReader
    {
        /// <summary> Reads .yup files from War Thunder's root directory. </summary>
        /// <param name="version"> Whether to read current or previous client version data. </param>
        /// <returns></returns>
        string ReadInstallData(EClientVersion version);
    }
}
