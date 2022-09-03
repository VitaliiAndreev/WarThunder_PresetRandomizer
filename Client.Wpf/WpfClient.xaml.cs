using Client.Shared.Enumerations;
using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows;
using Client.Wpf.Windows.Interfaces;
using Client.Wpf.Windows.Interfaces.Base;
using Core;
using Core.DataBase.WarThunder.Enumerations;
using Core.Exceptions;
using Core.Extensions;
using Core.Helpers.Logger.Interfaces;
using Core.Localization.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Separator = Core.Separator;

namespace Client.Wpf
{
    /// <summary> Interaction logic for App.xaml. </summary>
    public partial class WpfClient : Application, IWpfClient
    {
        /// <summary> The default <see cref="TextBlock.FontSize"/>.</summary>
        public const int DefaultFontSize = 16;

        #region Fields

        /// <summary> Whether to read data from JSON instead of the database. </summary>
        private bool _readOnlyJson;

        /// <summary> Whether to extract game files. </summary>
        private bool _readPreviouslyUnpackedJson;

        /// <summary> Whether to generate the database. </summary>
        private bool _generateDatabase;

        /// <summary> Startup arguments. </summary>
        internal static string StartupArguments;

        #endregion Fields
        #region Properties

        /// <summary> An instance of an active logger. </summary>
        public IActiveLogger Log { get; private set; }

        #endregion Properties
        #region Constuctors

        /// <summary> Creates a new application. </summary>
        public WpfClient()
        {
            var vectorImageKeys = typeof(EVectorImageKey.Flag)
                .GetFields(BindingFlags.Static | BindingFlags.Public)
                .ToDictionary(property => property.Name, property => property.GetValue(null).ToString())
            ;

            foreach(var item in typeof(ECountry).GetEnumerationItems<ECountry>())
            {
                if (vectorImageKeys.TryGetValue(item.ToString(), out var resourceKey))
                    EReference.CountryIconKeys.Add(item, resourceKey);
            }

            ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(int.MaxValue));
        }

        #endregion Constuctors
        #region Methods: Event Handlers

        private IMainWindow CreateMainWindow()
        {
            #region Starting GuiLoadingWindow to display progress of creating and initialising MainWindow.

            var guiLoadingWindowPresenter = default(IGuiLoadingWindowPresenter);
            var guiLoadingWindow = default(IGuiLoadingWindow);

            void createAndShowGuiLoadingWindow()
            {
                guiLoadingWindow = ApplicationHelpers.WindowFactory.CreateGuiLoadingWindow();
                guiLoadingWindowPresenter = guiLoadingWindow.Presenter;
                guiLoadingWindow.Show();
                Dispatcher.Run();
            }

            var guiLoadingFeedbackThread = new Thread(new ThreadStart(createAndShowGuiLoadingWindow));

            guiLoadingFeedbackThread.SetApartmentState(ApartmentState.STA);
            guiLoadingFeedbackThread.IsBackground = true;
            guiLoadingFeedbackThread.Start();

            #endregion Starting GuiLoadingWindow to display progress of creating and initialising MainWindow.

            while (guiLoadingWindowPresenter is null) Thread.Sleep(1);

            var mainWindow = ApplicationHelpers.WindowFactory.CreateMainWindow(guiLoadingWindowPresenter);

            guiLoadingWindow.CloseSafely();

            return mainWindow;
        }

        /// <summary> Organizes initialization and the overall flow, handles exceptions. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used, launch arguments currently aren't supported. </param>
        [STAThread]
        private void OnStartup(object sender, StartupEventArgs eventArguments)
        {
            try
            {
                ProcessStartupArguments(AdaptStartupArguments(eventArguments.Args));

                Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                ApplicationHelpers.InitialiseLoggers();

                Log = ApplicationHelpers.CreateActiveLogger(nameof(WpfClient));
                Log.Info("Started.");

                try
                {
                    ApplicationHelpers.Initialise(_generateDatabase, _readOnlyJson, _readPreviouslyUnpackedJson);
                }
                catch (SettingsFileRegeneratedException)
                {
                    Log.Warn(EWpfClientLogMessage.SettingsFileRenegenetated_Closing);

                    MessageBox.Show
                    (
                        Current.Windows.OfType<BaseWindow>().LastOrDefault() ?? new Window(),
                        EUnlocalisedMessage.SettingFileRegenerated,
                        EClientApplicationName.WarThunderPresetRandomizer,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
                    Environment.Exit(0);
                }

                ApplicationHelpers.Manager.RemoveOldLogFiles();
                ApplicationHelpers.WindowFactory.CreateLoadingWindow().ShowDialog();

                CreateMainWindow().ShowDialog();
            }
            catch (Exception exception)
            {
                Log?.Fatal("An error has occurred.", exception);
                ShowErrorMessage(exception);
                Environment.Exit(1);
            }
        }

        private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs eventArguments)
        {
            if (eventArguments.Exception is COMException comException && comException.ErrorCode == -2147221040)
                eventArguments.Handled = true;
        }

        #endregion Methods: Event Handlers

        /// <summary> Interfaces new startup arguments with old ones (to avoid regress during code adjustments). </summary>
        /// <param name="newStandardStartupArguments"> Startup arguments following the new standard. </param>
        private string[] AdaptStartupArguments(string[] newStandardStartupArguments)
        {
            StartupArguments = newStandardStartupArguments.StringJoin(Separator.Space);

            var tip = "consult BAT files titled \"Mode 1\" through \"Mode 5\" for examples, or just execute those files directly";
            var oldStandardStartupArguments = new List<string>(); // -r and -w, i.e. read from and write to JSON, respectively, are considered on by default.
            var legalArguments = new List<string>
            {
                "-!w",
                "-!r",
                "-dbw",
                "-dbr",
            };

            // Mode 1. New default: reading from JSON without generating a database.
            if (newStandardStartupArguments.IsEmpty())
            {
                oldStandardStartupArguments.Add("-j");
                oldStandardStartupArguments.Add("-!d");
            }
            else if (newStandardStartupArguments.Except(legalArguments).Any())
            {
                throw new ArgumentException($"Illegal arguments found, {tip}.");
            }
            // Mode 4. Not writing or reading from JSON while writing and reading from a database, i.e. old default.
            else if (new string[] { "-!w", "-!r", "-dbr", "-dbw" } is string[] databaseMode && newStandardStartupArguments.Intersect(databaseMode).Distinct().Count() == databaseMode.Count())
            {
                oldStandardStartupArguments.Clear();
            }
            // Mode 5. Not writing or reading from JSON while reading from database.
            else if (new string[] { "-!w", "-!r", "-dbr" } is string[] databaseReadMode && newStandardStartupArguments.Intersect(databaseReadMode).Distinct().Count() == databaseReadMode.Count())
            {
                oldStandardStartupArguments.Add("-!d");
            }
            // Mode 2. Not writing JSON, i.e. reading from already unpacked files.
            else if (new string[] { "-!w" } is string[] readMode && newStandardStartupArguments.Intersect(readMode).Distinct().Count() == readMode.Count())
            {
                oldStandardStartupArguments.Add("-!n");
            }
            // Mode 3. Reading from JSON and creating a database.
            else if (new string[] { "-dbw" } is string[] databaseWriteMode && newStandardStartupArguments.Intersect(databaseWriteMode).Distinct().Count() == databaseWriteMode.Count())
            {
                oldStandardStartupArguments.Add("-j");
            }
            else // Other combinations are considered illegal.
            {
                throw new ArgumentException($"Argument combination is illegal, {tip}.");
            }
            return oldStandardStartupArguments.ToArray();
        }

        /// <summary> Processes startup arguments. </summary>
        /// <param name="startupArguments"> Startup arguments </param>
        private void ProcessStartupArguments(string[] startupArguments)
        {
            _generateDatabase = true;

            if (startupArguments is null || startupArguments.IsEmpty())
                return;

            string getArgument(string targetArgument) => startupArguments.FirstOrDefault(argument => argument.ToLower() == targetArgument.ToLower());

            if (getArgument("-j") is string)
            {
                _readOnlyJson = true;
            }
            if (getArgument("-!n") is string)
            {
                _readPreviouslyUnpackedJson = true;
            }
            if (getArgument("-!d") is string)
            {
                _generateDatabase = false;
            }

            if (_readPreviouslyUnpackedJson)
            {
                _readOnlyJson = true;
                _generateDatabase = false;
            }
        }

        /// <summary>
        /// Shows the error message.
        /// If the <see cref="ApplicationHelpers.Loggers"/> are not yet initialized the call stack is shown within the message instead of the logs.
        /// If the <see cref="ApplicationHelpers.LocalisationManager"/> is not yet initialized, the message is show in English.
        /// </summary>
        /// <param name="exception"> The exception whose stack trace to use if <see cref="ApplicationHelpers.Loggers"/> are not yet initialized. </param>
        private void ShowErrorMessage(Exception exception)
        {
            string title;
            string message;
            string getInstruction(string localizedString) => Log is null ? $"\n{exception}" : localizedString;

            if (ApplicationHelpers.LocalisationManager is ILocalisationManager localizationManager)
            {
                title = localizationManager.GetLocalisedString(ELocalisationKey.Error);
                message = $"{localizationManager.GetLocalisedString(ELocalisationKey.FatalErrorShutdown)}\n{getInstruction(localizationManager.GetLocalisedString(ELocalisationKey.SeeLogsForDetails))}";
            }
            else
            {
                title = Word.Error;
                message = $"An error has occurred. The application will be shut down.\n{getInstruction(localizedString: "See the latest file in the \"Logs\" folder for details.")}";
            }

            MessageBox.Show(Current.Windows.OfType<BaseWindow>().LastOrDefault(), message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}