using System.Diagnostics;
using Soloco.Composition.Construction;

namespace Soloco.Composition.Unity.IntegrationTests
{
    public class DebugDeveloperBehavior : IDeveloperBehavior
    {
        [This]
        private IPerson _person = null;

        public void Develop()
        {
            Debug.Write(string.Format("Developer {0} is developing.", _person.Name));
        }
    }
}