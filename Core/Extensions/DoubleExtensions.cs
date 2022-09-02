namespace Core.Extensions
{
    public static class DoubleExtensions
    {
        public static bool IsPositive(this double value) => value > Double.Number.Zero;
    }
}