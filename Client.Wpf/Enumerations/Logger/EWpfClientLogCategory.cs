using Core.Enumerations;
using Core.Enumerations.Logger;

namespace Client.Wpf.Enumerations.Logger
{
    /// <summary> Categories of events provided to a logger. </summary>
    public class EWpfClientLogCategory : CoreLogCategory
    {
        /// <summary> See <see cref="Windows.AboutWindow"/>. </summary>
        public static string AboutWindow = $"{Word.About}{Character.Space}{Word.Window}";

        /// <summary> See <see cref="Wpf.ApplicationHelpers"/>. </summary>
        public static string ApplicationHelpers = $"{Word.Application}{Character.Space}{Word.Helpers}";

        /// <summary> See <see cref="Windows.GuiLoadingWindow"/>. </summary>
        public static string GuiLoadingWindow = $"{Word.Gui}{Character.Space}{Word.Loading}{Character.Space}{Word.Window}";

        /// <summary> See <see cref="Windows.LoadingWindow"/>. </summary>
        public static string LoadingWindow = $"{Word.Loading}{Character.Space}{Word.Window}";

        /// <summary> See <see cref="Windows.LocalizationWindow"/>. </summary>
        public static string LocalizationWindow = $"{Word.Localisation}{Character.Space}{Word.Window}";

        /// <summary> See <see cref="Windows.MainWindow"/>. </summary>
        public static string MainWindow = $"{Word.Main}{Character.Space}{Word.Window}";

        /// <summary> See <see cref="Windows.SettingsWindow"/>. </summary>
        public static string SettingsWindow = $"{Word.Settings}{Character.Space}{Word.Window}";

        /// <summary> See <see cref="Helpers.WindowFactory"/>. </summary>
        public static string WindowFactory = $"{Word.Window}{Character.Space}{Word.Factory}";

        /// <summary> See <see cref="Wpf.WpfClient"/>. </summary>
        public static string WpfClient = $"{Word.WPF}{Character.Space}{Word.Client}";
    }
}