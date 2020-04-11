using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.UnpackingToolsIntegration.Objects;
using System.IO;

namespace Core.UnpackingToolsIntegration.Helpers
{
    public class Converter : LoggerFluency, IConverter
    {
        #region Constructors

        /// <summary> Creates a new converter. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public Converter(params IConfiguredLogger[] loggers)
            : base(EUnpackingToolsIntegrationLogCategory.Converter, loggers)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(EUnpackingToolsIntegrationLogCategory.Converter));
        }

        #endregion Constructors

        public void ConvertDdsToPng(DirectoryInfo directory, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            foreach (var file in directory.GetFiles(searchOption))
                new DdsImage(file.FullName).Save(Path.Combine(file.DirectoryName, $"{file.GetNameWithoutExtension()}.{EFileExtension.Png}"));
        }
    }
}