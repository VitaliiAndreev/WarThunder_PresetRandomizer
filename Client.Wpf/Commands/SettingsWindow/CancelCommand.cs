﻿using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.Enumerations.Logger;
using System;
using System.Windows;
using System.Windows.Input;

namespace Client.Wpf.Commands.SettingsWindow
{
    public class CancelCommand : Command, ICommand
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public CancelCommand()
            : base(ECommandName.Cancel)
        {
        }

        #endregion Constructors

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="ILoadingWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (!(parameter is ISettingsWindowPresenter presenter))
                return;

            if ((!ApplicationHelpers.SettingsManager.WarThunderLocationIsValid() || !ApplicationHelpers.SettingsManager.KlensysWarThunderToolLocationIsValid()))
            {
                var title = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.ApplicationName);
                var message = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.RequiredSettingsHaveNotBeenSet);

                if (MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    presenter.Owner.Log.Debug(ECoreLogMessage.Closed);
                    Environment.Exit(0);
                }
                else
                {
                    presenter.ClosureCancelled = true;
                }
            }
        }
    }
}