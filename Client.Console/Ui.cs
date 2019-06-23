using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using Core.Randomizer.Enumerations;
using Core.Randomizer.Objects.SearchSpecifications;
using System;
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
            System.Console.Write("Specification (nation, game mode, BR - separated by spaces) > ");

            return System.Console.ReadLine();
        }

        /// <summary> Parses a given string into a specification instance. </summary>
        /// <returns></returns>
        private static Specification ParseSpecification(string specificationInput)
        {
            var parameters = specificationInput.Split(" ");

            if (parameters.Count() != 3)
            {
                System.Console.WriteLine("Incorrect Input (need 3 arguments)");
                return ParseSpecification(TakeSpecificationInput());
            }

            var nation = ParseNation(parameters[0]);
            var gamemode = ParseGameMode(parameters[1]);
            var battleRating = Math.Round(decimal.Parse(parameters[2]), 1);

            return new Specification(gamemode, nation, battleRating);
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