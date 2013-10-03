namespace Soloco.Composition.LinFu.IntegrationTests.CompositionSpecifications
{
    [Composite(typeof(Open<>))]
    public interface IOpen<T>
    {
        T Get();
        T this[Tuple<T> index] { get;}
    }
}