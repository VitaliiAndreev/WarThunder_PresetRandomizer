using Core.Enumerations;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Client.Wpf.Enumerations
{
    /// <summary> Pre-determined reference information. </summary>
    public class EApplicationData
    {
        public static Version Version;
        public static string DevelopmentStageLocalizationKey;

        public const string LinkToYouTubePlaylist = "https://www.youtube.com/playlist?list=PLTkOsj0Uogp4z4Px8IrmZIl_z6M60mmqX";

        static EApplicationData()
        {
            var productVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            var versionParts = productVersion.Split(ECharacter.Space);

            Version = new Version(versionParts.First());
            DevelopmentStageLocalizationKey = versionParts.Last();
        }
    }
}