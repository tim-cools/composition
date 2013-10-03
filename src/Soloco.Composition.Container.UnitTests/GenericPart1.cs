namespace Soloco.Composition.Container.IntegrationTests
{
    public class GenericPart1<T> : IGenericPart1<T>
        where T : class
    {
        public T Method1()
        {
            return null;
        }
    }
}