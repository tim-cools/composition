namespace Soloco.Composition.Container.IntegrationTests
{
    /// Generic Part
    ///////////////////////////////////////

    public interface IGenericPart1Composite<T> : IGenericPart1<T>
        where T : class
    {
    }
}