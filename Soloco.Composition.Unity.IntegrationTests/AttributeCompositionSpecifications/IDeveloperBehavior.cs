using System;

namespace Soloco.Composition.Unity.IntegrationTests.AttributeCompositionSpecifications
{
    [Implementation(typeof(DeveloperBehavior))]
    public interface IDeveloperBehavior
    {
        void Develop();
    }
}