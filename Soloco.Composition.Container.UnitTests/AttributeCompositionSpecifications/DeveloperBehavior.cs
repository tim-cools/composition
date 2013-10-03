using Soloco.Composition.Construction;

namespace Soloco.Composition.Container.IntegrationTests.AttributeCompositionSpecifications
{
    public class DeveloperBehavior : IDeveloperBehavior
    {
        [This]
        private IPerson _person;
        [This]
        private readonly IPersonBehaviorChecker _personBehaviorChecker;

        public DeveloperBehavior()
        {
        }

        public void Develop()
        {
            _personBehaviorChecker.HasDeveloped();
        }
    }
}