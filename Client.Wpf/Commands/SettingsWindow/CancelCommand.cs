using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core;
using System;
using System.Windows;
using System.Windows.Input;

namespace Client.Wpf.Commands.SettingsWindow
{
    /// <summary> A command for cancelling changes. </summary>
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
                if (presenter.PreviousValidWarThunderLocation is string && presenter.PreviousValidKlensysWarThunderToolsLocation is string)
                {
                    presenter.ClosingState = ESettingsWindowClosureState.ClosingCancelled;
                    return;
                }

                var title = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ApplicationName);
                var message = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.RequiredSettingsHaveNotBeenSet);

                if (MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    presenter.Owner.Log.Debug(CoreLogMessage.Closed);
                    Environment.Exit(0);
                }
                else
                {
                    presenter.ClosingState = ESettingsWindowClosureState.ClosingCancelled;
                    return;
                }
            }

            if (presenter.ClosingState != ESettingsWindowClosureState.ClosingExplicitly)
            {
                presenter.ClosingState = ESettingsWindowClosureState.ClosingFromCommand;
                presenter.CloseParentWindow();
            }
        }
    }
}