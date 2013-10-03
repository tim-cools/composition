using System.Diagnostics;
using Soloco.Composition.Construction;

namespace Soloco.Composition.Unity.IntegrationTests
{
    public class DebugPartyAnimalBehavior : IPartyAnimalBehavior
    {
        [This] 
        private IPerson _person = null;

        public void Dance()
        {
            Debug.Write(string.Format("Party animal {0} is dancing.", _person.Name));
        }
    }
}