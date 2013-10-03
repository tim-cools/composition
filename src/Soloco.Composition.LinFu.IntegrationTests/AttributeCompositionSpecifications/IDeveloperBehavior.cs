using System;

namespace Soloco.Composition.LinFu.IntegrationTests.AttributeCompositionSpecifications
{
    [Implementation(typeof(DeveloperBehavior))]
    public interface IDeveloperBehavior
    {
        void Develop();
    }
}