namespace Core.Web.WarThunder.Objects
{
    public class VehicleUsage
    {
        #region Properties

        public int ArcadeCount { get; }
        public int RealisticCount { get; }
        public int SimulatorCount { get; }

        #endregion Properties
        #region Constructors

        public VehicleUsage(int arcadeCount, int realisticCount, int simulatorCount)
        {
            ArcadeCount = arcadeCount;
            RealisticCount = realisticCount;
            SimulatorCount = simulatorCount;
        }

        #endregion Constructors
    }
}