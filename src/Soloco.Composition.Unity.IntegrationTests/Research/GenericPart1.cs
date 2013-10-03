namespace Soloco.Composition.Unity.IntegrationTests.Research
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