using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;
using Core.Organization.Enumerations;
using Core.Organization.Objects.SearchSpecifications;
using System;
using System.Linq;
using System.Windows.Input;

namespace Client.Wpf.Commands.MainWindow
{
    /// <summary> A command for generating vehicle presets. </summary>
    public class GeneratePresetCommand : Command, ICommand
    {
        #region Constructors

        /// <summary> Creates a new command. </summary>
        public GeneratePresetCommand()
            : base(ECommandName.GeneratePreset)
        {
        }

        #endregion Constructors

        /// <summary> Defines the method that determines whether the command can execute in its current state. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            if (!base.CanExecute(parameter))
                return false;

            if (!(parameter is IMainWindowPresenter presenter))
                return false;

            return presenter.EnabledBranches.Any();
        }

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="parameter"> Data used by the command. An <see cref="IMainWindowPresenter"/> is expected. </param>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is IMainWindowPresenter presenter)
            {
                presenter.ExecuteCommand(ECommandName.DeletePresets);

                var gameMode = presenter.CurrentGameMode;
                var nations = Enum.GetValues(typeof(ENation)).OfType<ENation>().Where(nation => nation != ENation.None).ToDictionary(nation => nation, nation => 10);
                var branches = presenter.EnabledBranches;
                var economicRanks = Enumerable.Range(EInteger.Number.Zero, EReference.TotalEconomicRanks);

                presenter.GeneratedPresets.Clear();
                presenter.GeneratedPresets.AddRange(ApplicationHelpers.Manager.GeneratePrimaryAndFallbackPresets(new Specification(gameMode, nations, branches, economicRanks)));

                var primaryPreset = presenter.GeneratedPresets[EPreset.Primary];
                var firstVehicle = primaryPreset.First();
                var selectedNation = firstVehicle.Nation.AsEnumerationItem;
                var selectedBranches = primaryPreset.Select(vehicle => vehicle.Branch.AsEnumerationItem).Distinct();

                presenter.LoadPresets();
                presenter.DisplayPreset(EPreset.Primary);
                presenter.EnableOnly(selectedNation, selectedBranches);
                presenter.FocusResearchTree(selectedNation, selectedBranches.First());
                presenter.BringIntoView(firstVehicle);
            }
        }
    }
}