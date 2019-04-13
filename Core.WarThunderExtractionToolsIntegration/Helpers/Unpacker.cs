using Core.Enumerations;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Enumerations;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Exceptions;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.UnpackingToolsIntegration.Helpers
{
    /// <summary> Provides methods to unpack War Thunder files. </summary>
    public class Unpacker : LoggerFluency, IUnpacker
    {
        private readonly Dictionary<string, string> _toolFileNames;

        #region Constructors

        /// <summary> Creates a new unpacker. </summary>
        /// <param name="logger"> An instance of a logger. </param>
        public Unpacker(IConfiguredLogger logger)
            : base(logger, ECoreLogCategory.Unpacker)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(ECoreLogCategory.Unpacker));

            _toolFileNames = new Dictionary<string, string>
            {
                { EFileExtension.Blk, ETool.BlkUnpacker },
                { EFileExtension.Clog, ETool.ClogUnpacker },
                { EFileExtension.Ddsx, ETool.DdsxUnpacker },
                { EFileExtension.Dxp, ETool.DxpUnpacker },
                { EFileExtension.VromfsBin, ETool.VromfsUnpacker },
                { EFileExtension.Wrpl, ETool.WrplUnpacker },
            };
        }

        #endregion Constructors

        /// <summary> Selects an appropriate unpacking tool file name (see <see cref="ETool"/>) for a given file path/name. </summary>
        /// <param name="fileName"> An absolute or a relative name of the file. </param>
        /// <returns></returns>
        private string GetToolFileNameByTargetFile(string fileName)
        {
            var fileExtension = fileName.Split(ECharacter.Period).Last().ToLower();

            if (_toolFileNames.TryGetValue(fileExtension, out var toolFileName))
            {
                LogDebug(ECoreLogMessage.UnpackingToolSelected);
                return toolFileName;
            }
            else
            {
                var message = ECoreLogMessage.FileExtensionNotSupportedByUnpackingTools.FormatFluently(fileExtension);
                var exception = new FileExtensionNotSupportedException(message);

                LogError(message, exception);
                throw exception;
            }
        }
    }
}
