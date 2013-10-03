namespace Soloco.Composition.Container.IntegrationTests.Research
{
    public interface IGenericThing<T>
    {
        T Test(T value);
    }
}