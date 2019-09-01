﻿using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;

namespace Client.Wpf.Commands.LoadingWindow
{
    public class InitializeCommand : Command
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public InitializeCommand()
            : base(ECommandName.Initialize)
        {
        }

        #endregion Constructors

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="ILoadingWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is ILoadingWindowPresenter presenter)
            {
                ApplicationHelpers.Manager.InitializeGameClientVersion();
                ApplicationHelpers.Manager.CacheVehicles();

                presenter.Owner.InitializationComplete = true;
            }
        }
    }
}