using Soloco.Composition.Construction;

namespace Soloco.Composition.LinFu.IntegrationTests
{
    public class PartyAnimalBehavior : IPartyAnimalBehavior
    {
        [This] 
        private IPerson _person;

        private readonly IPersonBehaviorChecker _personBehaviorChecker;

        public PartyAnimalBehavior(IPersonBehaviorChecker personBehaviorChecker)
        {
            _personBehaviorChecker = personBehaviorChecker;
        }

        public void Dance()
        {
            _personBehaviorChecker.HasDanced();
        }
    }
}