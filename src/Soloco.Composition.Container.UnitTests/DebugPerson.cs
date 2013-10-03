using System.Diagnostics;

namespace Soloco.Composition.Container.IntegrationTests
{
    public class DebugPerson : IPerson
    {
        public string Name
        {
            get; set;
        }
        
        public void Say(string message)
        {
            Debug.WriteLine(string.Format("Person {0} says: {1}", Name, message));
        }
    }
}