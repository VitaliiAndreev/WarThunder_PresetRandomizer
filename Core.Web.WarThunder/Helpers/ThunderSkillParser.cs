using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Connectors;
using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger.Interfaces;
using Core.Web.Extensions;
using Core.Web.Helpers;
using Core.Web.WarThunder.Enumerations;
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

        private readonly string _thunderSkillVehicleStatisticsUrl;

        private readonly string _armyTableXPath;
        private readonly string _helicopterTableXPath;
        private readonly string _aircraftTableXPath;
        private readonly string _fleetTableXPath;

        private readonly HtmlNode _mainHtmlNode;

        #endregion Fields
        #region Constructors

        public ThunderSkillParser
        (
            string thunderSkillVehicleStatisticsUrl,
            string armyTableXPath,
            string helicopterTableXPath,
            string aircraftTableXPath,
            string fleetTableXPath,
            params IConfiguredLogger[] loggers
        ) : base(loggers)
        {
            _thunderSkillVehicleStatisticsUrl = thunderSkillVehicleStatisticsUrl;

            _armyTableXPath = armyTableXPath;
            _helicopterTableXPath = helicopterTableXPath;
            _aircraftTableXPath = aircraftTableXPath;
            _fleetTableXPath = fleetTableXPath;

            _mainHtmlNode = GetHtmlDocumentNode(_thunderSkillVehicleStatisticsUrl);

            SetCustomCategory(EWebWarThunderLogCategory.ThunderSkillParser);
            LogDebug(ECoreLogMessage.Created.FormatFluently(EWebWarThunderLogCategory.ThunderSkillParser));
        }

        #endregion Constructors

        public IDictionary<string, VehicleUsage> GetVehicleUsage(EBranch branch)
        {
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