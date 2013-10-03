using Soloco.Composition.Construction;

namespace Soloco.Composition.LinFu.IntegrationTests.AttributeCompositionSpecifications
{
    public class Person : IPerson
    {
        [This]
        private readonly IPersonBehaviorChecker _personBehaviorChecker;

        public string Name
        {
            get; set;
        }
        
        public Person()
        {
        }

        public void Say(string message)
        {
            _personBehaviorChecker.HasSayed(message);
        }
    }
}