namespace Core.Enumerations
{
    /// <summary> Integer constants. </summary>
    public class EInteger
    {
        public class Number
        {
            public const int Zero = 0;
            public const int One = 1;
            public const int Two = 2;
            public const int Three = 3;
            public const int Four = 4;
            public const int Five = 5;
            public const int Ten = 10;
            public const int Hundred = 100;
            public const int Thousand = 1_000;
        }

        public class Time
        {
            public const int MillisecondsInSecond = Number.Thousand;
        }
    }
}