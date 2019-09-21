using System;

namespace Client.Wpf.Enumerations
{
    /// <summary> Pre-determined reference information. </summary>
    public class EApplicationData
    {
        public static Version Version = new Version(0, 1, 0, 0);
        public const string DevelopmentStageLocalizationKey = ELocalizationKey.PreAlpha;

        public const string ContributionsByVitalyAndreyev = "600+";
    }
}