namespace Soloco.Composition.LinFu.IntegrationTests.CompositionSpecifications
{
    public class Open<T> : IOpen<T>
    {
        public T Get()
        {
            return default(T);
        }

        public T this[Tuple<T> index]
        {
            get { return index.Part1; }
        }
    }
}