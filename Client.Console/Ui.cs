using Client.Console.Enumerations;
using Client.Console.Enumerations.Logger;
using Client.Shared.Enumerations;
using Core.Csv.WarThunder.Helpers;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Helpers;
using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.Json.Helpers;
using Core.Objects;
using Core.Organization.Enumerations;
using Core.Organization.Helpers;
using Core.Organization.Objects.SearchSpecifications;
using Core.Randomization.Helpers;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Helpers;
using Core.WarThunderExtractionToolsIntegration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Console
{
    class Ui
    {
        /// <summary> The entry point. </summary>
        static void Main()
        {
            System.Console.Title = EClientApplicationName.WarThunderPresetRandomizer;
            var defaultColor = ConsoleColor.Gray;

            var requiredSettings = new List<string>
            {
                nameof(Settings.WarThunderLocation),
                nameof(Settings.KlensysWarThunderToolsLocation),
            };

            try
            {
                var loggers = new IConfiguredLogger[]
                {
                    new ConfiguredNLogger(ELoggerName.FileLogger, new ExceptionFormatter()),
                    new ConfiguredNLogger(ELoggerName.ConsoleLogger, new ExceptionFormatter()),
                };
                var fileManager = new WarThunderFileManager(loggers);
                var fileReader = new WarThunderFileReader(loggers);
                var settingsManager = new WarThunderSettingsManager(fileManager, EConsoleClientFile.Settings, requiredSettings, loggers);
                var parser = new Parser(loggers);
                var unpacker = new Unpacker(fileManager, loggers);
                var jsonHelper = new WarThunderJsonHelper(loggers);
                var csvDeserializer = new CsvDeserializer(loggers);
                var randomizer = new CustomRandomizer(loggers);
                var vehicleSelector = new VehicleSelector(randomizer, loggers);

                using (var manager = new Manager(fileManager, fileReader, settingsManager, parser, unpacker, jsonHelper, csvDeserializer, randomizer, vehicleSelector, loggers))
                {
                    manager.InitializeGameClientVersion();

                    while (!settingsManager.WarThunderLocationIsValid())
                    {
                        System.Console.Write(EConsoleUiLogMessage.SelectValidLocation.FormatFluently(EApplicationName.WarThunder));
                        settingsManager.Save(nameof(Settings.WarThunderLocation), System.Console.ReadLine());
                    }
                    while (!settingsManager.KlensysWarThunderToolLocationIsValid())
                    {
                        System.Console.Write(EConsoleUiLogMessage.SelectValidLocation.FormatFluently(EApplicationName.KlensysWarThunderTools));
                        settingsManager.Save(nameof(Settings.KlensysWarThunderToolsLocation), System.Console.ReadLine());
                    }
                    System.Console.WriteLine();

                    manager.CacheData();

                    while (true)
                    {
                        var specification = ParseSpecification(TakeSpecificationInput());

                        foreach (var vehicle in manager.GeneratePrimaryAndFallbackPresets(specification)[EPreset.Primary])
                        {
                            System.Console.ForegroundColor = vehicle.NotResearchable
                                ? ConsoleColor.Yellow
                                : ConsoleColor.White;

                            System.Console.WriteLine($"\t {vehicle.BattleRatingFormatted[specification.GameMode]} {vehicle.GaijinId}");
                        }

                        System.Console.ForegroundColor = defaultColor;
                    }
                }
            }
            catch
            {
                System.Console.Write($"\n{ECoreLogMessage.AnErrorHasOccurred} {EConsoleUiLogMessage.PressAnyKeyToExit} ");
                System.Console.ReadKey(true);
                Environment.Exit(0);
            }
        }

        /// <summary> Takes a specification string from keyboard input. </summary>
        /// <returns></returns>
        private static string TakeSpecificationInput()
        {
            System.Console.Write(EConsoleUiLogMessage.InputSpecification);

            return System.Console.ReadLine();
        }

        /// <summary> Parses a given string into a specification instance. </summary>
        /// <returns></returns>
        private static Specification ParseSpecification(string specificationInput)
        {
            var parameters = specificationInput.Split(" ");

            if (parameters.Count() != 4)
            {
                System.Console.WriteLine(EConsoleUiLogMessage.IncorrectInput);
                return ParseSpecification(TakeSpecificationInput());
            }

            var gamemode = ParseGameMode(parameters[0]);
            var nation = ParseNation(parameters[1]);
            var branch = ParseBranch(parameters[2]);

            if (gamemode == EGameMode.None)
            {
                System.Console.WriteLine(EConsoleUiLogMessage.IncorrectGameMode);
                return ParseSpecification(TakeSpecificationInput());
            }
            if (nation == ENation.None)
            {
                System.Console.WriteLine(EConsoleUiLogMessage.IncorrectNation);
                return ParseSpecification(TakeSpecificationInput());
            }
            if (branch == EBranch.None)
            {
                System.Console.WriteLine(EConsoleUiLogMessage.IncorrectBranch);
                return ParseSpecification(TakeSpecificationInput());
            }
            if (!decimal.TryParse(parameters[3], out var battleRating))
            {
                System.Console.WriteLine(EConsoleUiLogMessage.IncorrectBattleRating);
                return ParseSpecification(TakeSpecificationInput());
            }

            var economicRank = Calculator.GetEconomicRank(Calculator.GetRoundedBattleRating(battleRating));

            return new Specification
            (
                gamemode,
                new Dictionary<ENation, NationSpecification> { { nation, new NationSpecification(nation, new List<EBranch> { branch }, EInteger.Number.Ten) } },
                new Dictionary<EBranch, BranchSpecification> { { branch, new BranchSpecification(branch, branch.GetVehicleClasses()) } },
                Enum
                    .GetValues(typeof(ENation))
                    .Cast<ENation>()
                    .Where(enumerationItem => enumerationItem != ENation.None)
                    .ToDictionary(enumerationItem => enumerationItem, enumerationItem => new Interval<int>(true, economicRank, economicRank, true))
            );
        }

        /// <summary> Parses a given string into a corresponding military branch enumeration value. </summary>
        /// <param name="branchInput"> The name of the branch. </param>
        /// <returns></returns>
        private static EBranch ParseBranch(string branchInput)
        {
            switch (branchInput.ToLower())
            {
                case "army":
                case "t":
                case "tank":
                case "tanks":
                case "g":
                case "ground":
                    return EBranch.Army;
                case "a":
                case "aviation":
                case "air":
                case "aircraft":
                case "p":
                case "plane":
                case "planes":
                    return EBranch.Aviation;
                case "f":
                case "fleet":
                case "s":
                case "ship":
                case "ships":
                case "b":
                case "boat":
                case "boats":
                    return EBranch.Fleet;
                case "h":
                case "heli":
                case "helicopter":
                case "helicopters":
                case "helo":
                case "c":
                case "chopper":
                case "choppers":
                    return EBranch.Helicopters;
                default:
                    return EBranch.None;
            }
        }

        /// <summary> Parses a given string into a corresponding nation enumeration value. </summary>
        /// <param name="nationInput"> The name of the nation. </param>
        /// <returns></returns>
        private static ENation ParseNation(string nationInput)
        {
            switch (nationInput.ToLower())
            {
                case "us":
                case "usa":
                case "america":
                case "murica":
                    return ENation.Usa;
                case "germany":
                    return ENation.Germany;
                case "ussr":
                case "soviet":
                case "red":
                case "russia":
                    return ENation.Ussr;
                case "uk":
                case "britain":
                case "commonwealth":
                    return ENation.Britain;
                case "japan":
                    return ENation.Japan;
                case "italy":
                    return ENation.Italy;
                case "france":
                    return ENation.France;
                default:
                    return ENation.None;
            }
        }

        /// <summary> Parses a given string into a corresponding game mode enumeration value. </summary>
        /// <param name="gameModeInput"> The name of the game mode. </param>
        /// <returns></returns>
        private static EGameMode ParseGameMode(string gameModeInput)
        {
            switch (gameModeInput.ToLower())
            {
                case "a":
                case "arcade":
                    return EGameMode.Arcade;
                case "r":
                case "real":
                case "realistic":
                    return EGameMode.Realistic;
                case "s":
                case "sim":
                case "simulator":
                case "simulation":
                    return EGameMode.Simulator;
                case "event":
                    return EGameMode.Event;
                default:
                    return EGameMode.None;
            }
        }
    }
}