namespace Soloco.Composition.Container.IntegrationTests.AttributeCompositionSpecifications
{
    [Composite(typeof(PersonOverride))]
    public interface IDeveloperPartialOverride : IPerson, IDeveloperBehavior, IPersonBehaviorChecker
    {
    }
}