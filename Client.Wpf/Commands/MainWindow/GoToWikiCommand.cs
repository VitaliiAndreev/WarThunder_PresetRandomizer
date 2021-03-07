using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using System.Diagnostics;
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

                Process.Start(link);
                presenter.ReferencedVehicle = null;
            }
        }

        private string GetDomain(ELanguage language)
        {
            return language switch
            {
                ELanguage.English => EDomain.Com,
                ELanguage.Russian => EDomain.Ru,
                _ => EDomain.Com,
            };
        }

        private string GetVehicleName(IVehicle vehicle, ELanguage language)
        {
            var nameParts = vehicle.ResearchTreeName.GetLocalisation(language).Split(ECharacter.Space).ToList();
            var firstNamePart = nameParts.First();

            if (!firstNamePart.First().IsLetterOrDigitFluently())
            {
                nameParts[EInteger.Number.Zero] = firstNamePart.Substring(EInteger.Number.One);
                nameParts.Add($"{ECharacter.ParenthesisLeft}{ApplicationHelpers.LocalisationManager.GetLocalisedString(vehicle.Nation.AsEnumerationItem)}{ECharacter.ParenthesisRight}");
            }
            return nameParts.StringJoin(ECharacter.Plus);
        }
    }
}