using System.Collections.Generic;

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
            public const int Six = 6;
            public const int Seven = 7;
            public const int Eight = 8;
            public const int Nine = 9;
            public const int Ten = 10;
            public const int Eleven = 11;
            public const int Twelve = 12;
            public const int Thirteen = 13;
            public const int Fourteen = 14;
            public const int Fifteen = 15;
            public const int Sixteen = 16;
            public const int Seventeen = 17;
            public const int Twenty = 20;
            public const int TwentyOne = 21;
            public const int TwentyThree = 23;
            public const int TwentyFive = 25;
            public const int Thirty = 30;
            public const int NinetyNine = 99;
            public const int Hundred = 100;
            public const int HundredOne = 101;
            public const int HundredThree = 103;
            public const int HundredSeven = 107;
            public const int HundredNine = 109;
            public const int HundredThirteen = 113;
            public const int HundredTwentySeven = 127;
            public const int HundredThirtyOne = 131;
            public const int Thousand = 1_000;

            public static IEnumerable<int> PrimesAboveHundred = new List<int> { HundredOne, HundredThree, HundredSeven, HundredNine, HundredThirteen, HundredTwentySeven, HundredThirtyOne };
        }

        /// <summary> Time-related constants. </summary>
        public class Time
        {
            public const int MillisecondsInSecond = Number.Thousand;
        }
    }
}