using Core.Enumerations;

namespace Core.UnpackingToolsIntegration.Enumerations
{
    /// <summary> Enumerates names of unpacking tools. </summary>
    public class ETool
    {
        private const string _unpacker = "_unpack";
        private const string _exe = ECharacterString.Period + EFileExtension.Exe;

        public const string BlkUnpacker = "blk" + _unpacker + _exe;
        public const string ClogUnpacker = "clog" + _unpacker + _exe;
        public const string DdsxUnpacker = "ddsx" + _unpacker + _exe;
        public const string DxpUnpacker = "dxp" + _unpacker + _exe;
        public const string VromfsUnpacker = EFile.Vromfs + _unpacker + _exe;
        public const string WrplUnpacker = "wrpl" + _unpacker + _exe;
    }
}
