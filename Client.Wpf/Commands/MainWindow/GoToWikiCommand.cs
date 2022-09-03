using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Linq;

namespace Client.Wpf.Commands.MainWindow
{
    public class GoToWikiCommand : Command
    {
        #region Constructors

        public GoToWikiCommand()
            : base(ECommandName.GoToWiki)
        {
        }

        #endregion Constructors

        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if (parameter is IMainWindowPresenter presenter && presenter.ReferencedVehicle is IVehicle vehicle)
            {
                var language = WpfSettings.LocalizationLanguage;
                var link = EApplicationData.LinkToOfficialWikiSearch.Format
                (
                    GetDomain(language),
                    GetVehicleName(vehicle, language)
                );

                System.Diagnostics.Process.Start(link);
                presenter.ReferencedVehicle = null;
            }
        }

        private string GetDomain(Language language)
        {
            return language switch
            {
                Language.English => Domain.Com,
                Language.Russian => Domain.Ru,
                _ => Domain.Com,
            };
        }

        private string GetVehicleName(IVehicle vehicle, Language language)
        {
            var nameParts = vehicle.ResearchTreeName.GetLocalisation(language).Split(' ').ToList();
            var firstNamePart = nameParts.First();

            if (!char.IsLetterOrDigit(firstNamePart.First()))
            {
                nameParts[0] = firstNamePart.Substring(1);
                nameParts.Add($"({ApplicationHelpers.LocalisationManager.GetLocalisedString(vehicle.Nation.AsEnumerationItem)})");
            }
            return nameParts.StringJoin('+');
        }
    }
}