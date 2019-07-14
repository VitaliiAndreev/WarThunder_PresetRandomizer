using Client.Console.Enumerations.Logger;
using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System;
using Core.Organization.Objects.SearchSpecifications;
using System.Linq;

namespace Client.Console
{
    class Ui
    {
        /// <summary> The entry point. </summary>
        static void Main()
        {
            using (var manager = new Manager())
                while (true)
                    foreach (var vehicle in manager.GetVehicles(ParseSpecification(TakeSpecificationInput())))
                        System.Console.WriteLine($"\t {vehicle.GaijinId}");
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

            return new Specification(gamemode, nation, branch, Math.Round(battleRating, 1));
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
                    return ENation.Commonwealth;
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