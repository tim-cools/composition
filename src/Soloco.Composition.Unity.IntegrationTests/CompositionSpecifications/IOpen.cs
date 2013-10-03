namespace Soloco.Composition.Unity.IntegrationTests.CompositionSpecifications
{
    [Composite(typeof(Open<>))]
    public interface IOpen<T>
    {
        T Get();
        T this[Tuple<T> index] { get;}
    }
}