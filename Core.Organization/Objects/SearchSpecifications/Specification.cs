using Core.DataBase.WarThunder.Enumerations;

namespace Core.Organization.Objects.SearchSpecifications
{
    public class Specification
    {
        public EGameMode GameMode { get; }
        public ENation Nation { get; }
        public EBranch Branch { get; }
        public decimal BattleRating { get; }

        public Specification(EGameMode gameMode, ENation nation, EBranch branch, decimal battleRating)
        {
            GameMode = gameMode;
            Nation = nation;
            Branch = branch;
            BattleRating = battleRating;
        }
    }
}