using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using Core.Web.Extensions;
using Core.Web.Helpers;
using Core.Web.WarThunder.Enumerations.Logger;
using Core.Web.WarThunder.Helpers.Interfaces;
using Core.Web.WarThunder.Objects;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Core.Web.WarThunder.Helpers
{
    public class ThunderSkillParser : HtmlParser, IThunderSkillParser
    {
        #region Fields

        private string _armyTableXPath;
        private string _helicopterTableXPath;
        private string _aircraftTableXPath;
        private string _fleetTableXPath;

        private HtmlNode _mainHtmlNode;

        #endregion Fields
        #region Properties

        public bool IsLoaded { get; private set; }

        #endregion Properties
        #region Constructors

        public ThunderSkillParser(params IConfiguredLogger[] loggers)
            : base(loggers)
        {
            IsLoaded = false;

            SetCustomCategory(EWebWarThunderLogCategory.ThunderSkillParser);
            LogDebug(ECoreLogMessage.Created.Format(EWebWarThunderLogCategory.ThunderSkillParser));
        }

        #endregion Constructors

        public void Load()
        {
            _armyTableXPath = Settings.ThunderSkillArmyStatisticsXPath;
            _helicopterTableXPath = Settings.ThunderSkillHelicopterStatisticsXPath;
            _aircraftTableXPath = Settings.ThunderSkillAircraftStatisticsXPath;
            _fleetTableXPath = Settings.ThunderSkillFleetStatisticsXPath;

            var url = Settings.ThunderSkillUrl;

            LogDebug(ECoreLogMessage.Reading.Format(url));
            {
                _mainHtmlNode = GetHtmlDocumentNode(url);
            }
            LogDebug(ECoreLogMessage.FinishedReading.Format(url));

            IsLoaded = true;
        }

        public IDictionary<string, VehicleUsage> GetVehicleUsage(EBranch branch)
        {
            if (!IsLoaded)
                return new Dictionary<string, VehicleUsage>();

            switch (branch)
            {
                case EBranch.Army:
                    return GetVehicleUsage(_armyTableXPath);
                case EBranch.Helicopters:
                    return GetVehicleUsage(_helicopterTableXPath);
                case EBranch.Aviation:
                    return GetVehicleUsage(_aircraftTableXPath);
                case EBranch.Fleet:
                    return GetVehicleUsage(_fleetTableXPath);
                default:
                    return new Dictionary<string, VehicleUsage>();
            }
        }

        private IDictionary<string, VehicleUsage> GetVehicleUsage(string tableXPath)
        {
            var vehicleUsageRecords = new Dictionary<string, VehicleUsage>();

            if (string.IsNullOrWhiteSpace(tableXPath))
                return vehicleUsageRecords;

            var table = _mainHtmlNode.SelectSingleNode(tableXPath);

            if (table is HtmlNode)
            {
                var tableRows = table.GetChildNodes("tr").ToList();

                foreach (var row in tableRows)
                {
                    var cells = row.GetChildNodes("td").ToList();
                    var vehicleGaijinId = cells
                        .Second()
                        .GetChildNodes("a")
                        .First()
                        .GetAttributeValue("href", string.Empty)
                        .Split(ECharacter.Slash)
                        .Last()
                    ;

                    if (!int.TryParse(cells.ThirdLast().GetTrimmedInnerText(), out var arcadeUseCount))
                        continue;

                    if (!int.TryParse(cells.SecondLast().GetTrimmedInnerText(), out var realisticUseCount))
                        continue;

                    if (!int.TryParse(cells.Last().GetTrimmedInnerText(), out var simulatorUseCount))
                        continue;

                    vehicleUsageRecords.Add(vehicleGaijinId, new VehicleUsage(arcadeUseCount, realisticUseCount, simulatorUseCount));
                }
            }
            return vehicleUsageRecords;
        }
    }
}