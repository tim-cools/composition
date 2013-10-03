using Soloco.Composition.Container.IntegrationTests.CompositionSpecifications;

namespace Soloco.Composition.Container.IntegrationTests.InjectionSpecifications
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