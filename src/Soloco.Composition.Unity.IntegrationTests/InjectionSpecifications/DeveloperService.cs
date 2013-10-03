
namespace Soloco.Composition.Unity.IntegrationTests.InjectionSpecifications
{
    public class DeveloperService
    {
        private readonly IDeveloper _developer;

        public IDeveloper Developer
        {
            get { return _developer; }
        }

        public DeveloperService(IDeveloper developer)
        {
            _developer = developer;
        }
    }
}