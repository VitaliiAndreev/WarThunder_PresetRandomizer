namespace Core.Randomizer.Objects.SearchSpecifications.Interfaces
{
    public interface ISpecification<T>
    {
        string Name { get; }
        T Value { get; }
    }
}