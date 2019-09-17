using Core.Enumerations;
using Core.Enumerations.Logger;

namespace Client.Wpf.Enumerations.Logger
{
    /// <summary> Categories of events provided to a logger. </summary>
    public class EWpfClientLogCategory : ECoreLogCategory
    {
        /// <summary> See <see cref="Windows.AboutWindow"/>. </summary>
        public static string AboutWindow = $"{EWord.About} {EWord.Window}";

        /// <summary> See <see cref="Wpf.ApplicationHelpers"/>. </summary>
        public static string ApplicationHelpers = $"{EWord.Application} {EWord.Helpers}";

        /// <summary> See <see cref="Windows.LoadingWindow"/>. </summary>
        public static string LoadingWindow = $"{EWord.Loading} {EWord.Window}";

        /// <summary> See <see cref="Windows.LocalizationWindow"/>. </summary>
        public static string LocalizationWindow = $"{EWord.Localization} {EWord.Window}";

        /// <summary> See <see cref="Windows.MainWindow"/>. </summary>
        public static string MainWindow = $"{EWord.Main} {EWord.Window}";

        /// <summary> See <see cref="Windows.SettingsWindow"/>. </summary>
        public static string SettingsWindow = $"{EWord.Settings} {EWord.Window}";

        /// <summary> See <see cref="WindowFactory"/>. </summary>
        public static string WindowFactory = $"{EWord.Window} {EWord.Factory}";

        /// <summary> See <see cref="Wpf.WpfClient"/>. </summary>
        public static string WpfClient = $"{EWord.WPF} {EWord.Client}";
    }
}