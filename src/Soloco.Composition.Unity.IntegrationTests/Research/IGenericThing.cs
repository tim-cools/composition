namespace Soloco.Composition.Unity.IntegrationTests.Research
{
    public interface IGenericThing<T>
    {
        T Test(T value);
    }
}