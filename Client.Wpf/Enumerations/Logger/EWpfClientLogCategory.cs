using Core.Enumerations;
using Core.Enumerations.Logger;

namespace Client.Wpf.Enumerations.Logger
{
    /// <summary> Categories of events provided to a logger. </summary>
    public class EWpfClientLogCategory : ECoreLogCategory
    {
        /// <summary> See <see cref="Windows.AboutWindow"/>. </summary>
        public static string AboutWindow = $"{EWord.About}{ECharacter.Space}{EWord.Window}";

        /// <summary> See <see cref="Wpf.ApplicationHelpers"/>. </summary>
        public static string ApplicationHelpers = $"{EWord.Application}{ECharacter.Space}{EWord.Helpers}";

        /// <summary> See <see cref="Windows.GuiLoadingWindow"/>. </summary>
        public static string GuiLoadingWindow = $"{EWord.Gui}{ECharacter.Space}{EWord.Loading}{ECharacter.Space}{EWord.Window}";

        /// <summary> See <see cref="Windows.LoadingWindow"/>. </summary>
        public static string LoadingWindow = $"{EWord.Loading}{ECharacter.Space}{EWord.Window}";

        /// <summary> See <see cref="Windows.LocalizationWindow"/>. </summary>
        public static string LocalizationWindow = $"{EWord.Localisation}{ECharacter.Space}{EWord.Window}";

        /// <summary> See <see cref="Windows.MainWindow"/>. </summary>
        public static string MainWindow = $"{EWord.Main}{ECharacter.Space}{EWord.Window}";

        /// <summary> See <see cref="Windows.SettingsWindow"/>. </summary>
        public static string SettingsWindow = $"{EWord.Settings}{ECharacter.Space}{EWord.Window}";

        /// <summary> See <see cref="Helpers.WindowFactory"/>. </summary>
        public static string WindowFactory = $"{EWord.Window}{ECharacter.Space}{EWord.Factory}";

        /// <summary> See <see cref="Wpf.WpfClient"/>. </summary>
        public static string WpfClient = $"{EWord.WPF}{ECharacter.Space}{EWord.Client}";
    }
}