using Core.DataBase.WarThunder.Enumerations;

namespace Core.Organization.Objects.SearchSpecifications
{
    /// <summary> A specification used for filtering preferred items from collections before randomizing the former. </summary>
    public class Specification
    {
        /// <summary> The game mode. </summary>
        public EGameMode GameMode { get; }
        /// <summary> The nation. </summary>
        public ENation Nation { get; }
        /// <summary> The branch. </summary>
        public EBranch Branch { get; }
        /// <summary> The battle rating. </summary>
        public decimal BattleRating { get; }

        /// <summary> Creates a new filter specification with the given parameters. </summary>
        /// <param name="gameMode"> The game mode. </param>
        /// <param name="nation"> The nation. </param>
        /// <param name="branch"> The branch. </param>
        /// <param name="battleRating"> The battle rating. </param>
        public Specification(EGameMode gameMode, ENation nation, EBranch branch, decimal battleRating)
        {
            GameMode = gameMode;
            Nation = nation;
            Branch = branch;
            BattleRating = battleRating;
        }
    }
}