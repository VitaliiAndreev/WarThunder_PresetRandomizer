using Core.Helpers;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;

namespace Core.UnpackingToolsIntegration.Helpers
{
    /// <summary> Provides methods to read War Thunder files. </summary>
    public class WarThunderFileReader : FileReader, IWarThunderFileReader
    {
        #region Constructors

        /// <summary> Creates a new file reader. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public WarThunderFileReader(params IConfiguredLogger[] loggers)
            : base(loggers)
        {
        }

        #endregion Constructors

        /// <summary>
        /// Reads .yup files from War Thunder's root directory.
        /// <para> Note that while some data (like the client version) is readable in text form, accessing full data requires the files to be processed first. </para>
        /// </summary>
        /// <param name="version"> Whether to read current or previous client version data. </param>
        /// <returns></returns>
        public string ReadInstallData(EClientVersion version)
        {
            var filePath = default(string);

            switch (version)
            {
                case EClientVersion.Current:
                    filePath = $"{Settings.WarThunderLocation}\\{EFile.CurrentIntallData}";
                    break;
                case EClientVersion.Previous:
                    filePath = $"{Settings.WarThunderLocation}\\{EFile.PreviousVersionInstallData}";
                    break;
            }
            return Read(filePath);
        }
    }
}