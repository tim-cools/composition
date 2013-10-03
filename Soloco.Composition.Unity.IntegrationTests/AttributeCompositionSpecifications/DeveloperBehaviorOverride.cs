using Soloco.Composition.Construction;

namespace Soloco.Composition.Unity.IntegrationTests.AttributeCompositionSpecifications
{
    public class DeveloperBehaviorOverride : IDeveloperBehavior
    {
        [This]
        private IPerson _person = null;
        [This]
        private readonly IPersonBehaviorChecker _personBehaviorChecker = null;

        public DeveloperBehaviorOverride()
        {
        }

        public void Develop()
        {
            _personBehaviorChecker.HasDevelopedOverride();
        }
    }
}