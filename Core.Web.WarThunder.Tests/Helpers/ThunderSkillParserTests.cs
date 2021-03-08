using Core.DataBase.WarThunder.Enumerations;
using Core.Tests;
using Core.WarThunderExtractionToolsIntegration;
using Core.Web.WarThunder.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Web.WarThunder.Tests.Helpers
{
    [TestClass]
    public class ThunderSkillParserTests
    {
        #region Internal Methods

        [TestInitialize]
        public void Initialise()
        {
            Settings.ThunderSkillUrl = "http://thunderskill.com/en/vehicles";
            Settings.ThunderSkillArmyStatisticsXPath = "/html/body/div[3]/main/div[3]/table/tbody";
            Settings.ThunderSkillAircraftStatisticsXPath = "/html/body/div[3]/main/div[2]/table/tbody";
            Settings.ThunderSkillFleetStatisticsXPath = "/html/body/div[3]/main/div[4]/table/tbody";
        }

        #endregion Internal Methods

        [TestMethod]
        public void GetVehicleUsage()
        {
            // arrange
            var parser = new ThunderSkillParser(Presets.Logger);

            parser.Load();

            // act
            var armyUsageRecords = parser.GetVehicleUsage(EBranch.Army);
            var helicopterUsageRecords = parser.GetVehicleUsage(EBranch.Helicopters);
            var aircraftUsageRecords = parser.GetVehicleUsage(EBranch.Aviation);
            var fleetUsageRecords = parser.GetVehicleUsage(EBranch.AllFleet);

            // assert
            armyUsageRecords.Should().NotBeEmpty();
            helicopterUsageRecords.Should().BeEmpty();
            aircraftUsageRecords.Should().NotBeEmpty();
            fleetUsageRecords.Should().NotBeEmpty();
        }
    }
}
