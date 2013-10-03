using Soloco.Composition.Construction;

namespace Soloco.Composition.LinFu.IntegrationTests.AttributeCompositionSpecifications
{
    public class PersonOverride : IPerson
    {
        [This]
        private readonly IPersonBehaviorChecker _personBehaviorChecker;

        public string Name
        {
            get;
            set;
        }

        public PersonOverride()
        {
        }

        public void Say(string message)
        {
            _personBehaviorChecker.HasSayedOverride(message);
        }
    }
}