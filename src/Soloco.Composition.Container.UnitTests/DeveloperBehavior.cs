using Soloco.Composition.Construction;

namespace Soloco.Composition.Container.IntegrationTests
{
    public class DeveloperBehavior : IDeveloperBehavior
    {
        [This]
        private Person _person;

        private readonly IPersonBehaviorChecker _personBehaviorChecker;

        public DeveloperBehavior(IPersonBehaviorChecker personBehaviorChecker)
        {
            _personBehaviorChecker = personBehaviorChecker;
        }

        public void Develop()
        {
            _personBehaviorChecker.HasDeveloped();
        }
    }
}