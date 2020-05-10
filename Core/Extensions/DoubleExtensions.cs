using Core.Enumerations;

namespace Core.Extensions
{
    public static class DoubleExtensions
    {
        public static bool IsPositive(this double value) => value > EDouble.Number.Zero;
    }
}