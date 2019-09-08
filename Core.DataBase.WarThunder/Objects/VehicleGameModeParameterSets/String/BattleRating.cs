using Core.DataBase.WarThunder.Enumerations;

namespace Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSet.String
{
    /// <summary>
    /// A set of string parameters that vary depending on the game mode.
    /// This is a non-persistent foil for <see cref="Decimal.BattleRating"/>
    /// </summary>
    public class BattleRating
    {
        #region Persistent Properties

        /// <summary> The value in Arcade Battles. </summary>
        public string Arcade { get; }

        /// <summary> The value in Realistic Battles. </summary>
        public string Realistic { get; }

        /// <summary> The value in Simulator Battles. </summary>
        public string Simulator { get; }

        /// <summary> The value in Event Battles. </summary>
        public string Event { get; }

        #endregion Persistent Properties
        #region Constructors

        /// <summary> Creates a new set of values. </summary>
        /// <param name="valueInArcade"> The value in Arcade Battles. </param>
        /// <param name="valueInRealistic"> The value in Realistic Battles. </param>
        /// <param name="valueInSimulator"> The value in Simulator Battles. </param>
        /// <param name="valueInEvent"> The value in Event Battles. </param>
        public BattleRating(string valueInArcade, string valueInRealistic, string valueInSimulator, string valueInEvent)
        {
            Arcade = valueInArcade;
            Realistic = valueInRealistic;
            Simulator = valueInSimulator;
            Event = valueInEvent;
        }

        #endregion Constructors

        /// <summary> Return value of the game mode parameter corresponding to the given enumeration value. </summary>
        /// <param name="gameMode"> The game mode the value for which to get. </param>
        /// <returns></returns>
        public virtual string this[EGameMode gameMode]
        {
            get
            {
                return gameMode switch
                {
                    EGameMode.Arcade => Arcade,
                    EGameMode.Realistic => Realistic,
                    EGameMode.Simulator => Simulator,
                    EGameMode.Event => Event,
                    _ => null,
                };
            }
        }
    }
}