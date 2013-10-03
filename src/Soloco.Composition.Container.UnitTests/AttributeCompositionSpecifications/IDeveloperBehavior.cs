using System;

namespace Soloco.Composition.Container.IntegrationTests.AttributeCompositionSpecifications
{
    [Implementation(typeof(DeveloperBehavior))]
    public interface IDeveloperBehavior
    {
        void Develop();
    }
}