namespace Core.Enumerations
{
    /// <summary> Integer constants. </summary>
    public class EInteger
    {
        /// <summary> Pure number constants. </summary>
        public class Number
        {
            public const int Zero = 0;
            public const int One = 1;
            public const int Two = 2;
            public const int Three = 3;
            public const int Four = 4;
            public const int Five = 5;
            public const int Seven = 7;
            public const int Nine = 9;
            public const int Ten = 10;
            public const int Sixteen = 16;
            public const int Hundred = 100;
            public const int Thousand = 1_000;
        }

        /// <summary> Time-related constants. </summary>
        public class Time
        {
            public const int MillisecondsInSecond = Number.Thousand;
        }
    }
}