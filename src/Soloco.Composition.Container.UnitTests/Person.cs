namespace Soloco.Composition.Container.IntegrationTests
{
    public class Person : IPerson
    {
        private readonly IPersonBehaviorChecker _personBehaviorChecker;

        public string Name
        {
            get; set;
        }
        
        public Person(IPersonBehaviorChecker personBehaviorChecker)
        {
            _personBehaviorChecker = personBehaviorChecker;
        }

        public void Say(string message)
        {
            _personBehaviorChecker.HasSayed(message);
        }
    }
}