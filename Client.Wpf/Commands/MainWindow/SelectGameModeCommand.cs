﻿using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;

namespace Client.Wpf.Commands.MainWindow
{
    /// <summary> A command for changing the game mode. </summary>
    public class SelectGameModeCommand : Command
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public SelectGameModeCommand()
            : base(ECommandName.SelectGameMode)
        {
        }

        #endregion Constructors

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is IMainWindowPresenter presenter)
                if (WpfSettings.CurrentGameModeAsEnumerationItem != presenter.CurrentGameMode)
                    ApplicationHelpers.SettingsManager.Save(nameof(WpfSettings.CurrentGameMode), presenter.CurrentGameMode.ToString());
        }
    }
}