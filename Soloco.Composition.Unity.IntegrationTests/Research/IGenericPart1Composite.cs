namespace Soloco.Composition.Unity.IntegrationTests.Research
{
    /// Generic Part
    ///////////////////////////////////////

    public interface IGenericPart1Composite<T> : IGenericPart1<T>
        where T : class
    {
    }
}