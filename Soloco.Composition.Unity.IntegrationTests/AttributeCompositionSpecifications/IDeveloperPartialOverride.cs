namespace Soloco.Composition.Unity.IntegrationTests.AttributeCompositionSpecifications
{
    [Composite(typeof(PersonOverride))]
    public interface IDeveloperPartialOverride : IPerson, IDeveloperBehavior, IPersonBehaviorChecker
    {
    }
}