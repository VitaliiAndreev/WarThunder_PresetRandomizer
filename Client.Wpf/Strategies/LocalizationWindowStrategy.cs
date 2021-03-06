﻿using Client.Wpf.Commands.LocalizationWindow;
using Client.Wpf.Enumerations;
using Client.Wpf.Strategies.Interfaces;
using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Strategies
{
    /// <summary> A strategy that is used to set commands available in the <see cref="ILocalizationWindow"/>. </summary>
    public class LocalizationWindowStrategy : Strategy, ILocalizationWindowStrategy
    {
        /// <summary> Initializes and assigns commands to be used with this strategy. </summary>
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            
            _commands.Add(ECommandName.SelectLocalization, new SelectLocalizationCommand());
        }
    }
}