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
        public const string LinkToOfficialWikiSearch = "https://wiki.warthunder.{0}/index.php?search={1}";

        static EApplicationData()
        {
            var productVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            var versionParts = productVersion.Split(' ');

            Version = new Version(versionParts.First());
            DevelopmentStageLocalizationKey = versionParts.Last();
        }
    }
}