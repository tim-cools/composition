namespace Soloco.Composition.Container.IntegrationTests.AttributeCompositionSpecifications
{
    [Composite(typeof(PersonOverride), typeof(DeveloperBehaviorOverride))]
    public interface IDeveloperFullOverride : IPerson, IDeveloperBehavior, IPersonBehaviorChecker
    {
    }
}