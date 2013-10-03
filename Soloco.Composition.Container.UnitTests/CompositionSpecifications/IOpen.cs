using System;

namespace Soloco.Composition.Container.IntegrationTests.CompositionSpecifications
{
    [Composite(typeof(Open<>))]
    public interface IOpen<T>
    {
        T Get();
        T this[Tuple<T, T> index] { get;}
    }
}