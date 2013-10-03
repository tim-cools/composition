namespace Soloco.Composition.LinFu.IntegrationTests.AttributeCompositionSpecifications
{
    [Composite(typeof(PersonOverride))]
    public interface IDeveloperPartialOverride : IPerson, IDeveloperBehavior, IPersonBehaviorChecker
    {
    }
}