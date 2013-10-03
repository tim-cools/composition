using System;

namespace Soloco.Composition.Container.IntegrationTests.CompositionSpecifications
{
    public class Open<T> : IOpen<T>
    {
        public T Get()
        {
            return default(T);
        }

        public T this[Tuple<T, T> index]
        {
            get { return index.Item1; }
        }
    }
}