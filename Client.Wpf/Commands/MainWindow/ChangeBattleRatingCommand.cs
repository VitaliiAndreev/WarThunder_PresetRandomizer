using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.Extensions;
using Core.WarThunderExtractionToolsIntegration;
using System.Linq;

namespace Client.Wpf.Commands.MainWindow
{
    /// <summary> A command for changing enabled battle rating intervals. </summary>
    public class ChangeBattleRatingCommand : Command
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public ChangeBattleRatingCommand()
            : base(ECommandName.ChangeBattleRating)
        {
        }

        #endregion Constructors

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is IMainWindowPresenter presenter)
                ApplicationHelpers.SettingsManager.Save
                (
                    nameof(WpfSettings.EnabledEconomicRanks),
                    presenter
                        .EnabledEconomicRankIntervals
                        .Values
                        .Select(interval => $"{interval.LeftItem}-{interval.RightItem}")
                        .StringJoin(Settings.Separator)
                );
        }
    }
}
