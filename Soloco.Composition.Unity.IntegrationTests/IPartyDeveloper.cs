namespace Soloco.Composition.Unity.IntegrationTests
{
    /// <summary>
    /// Party developer person composition interface.
    /// </summary>
    public interface IPartyDeveloper : IPerson, IPartyAnimalBehavior, IDeveloperBehavior
    {
    }
}