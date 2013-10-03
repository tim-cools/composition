namespace Soloco.Composition.Unity.IntegrationTests.AttributeCompositionSpecifications
{
    public interface IDeveloper : IPerson, IDeveloperBehavior, IPersonBehaviorChecker
    {
    }
}