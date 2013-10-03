using Soloco.Composition.Construction;
using Soloco.Composition.LinFu.IntegrationTests.CompositionSpecifications;

namespace Soloco.Composition.LinFu.IntegrationTests
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