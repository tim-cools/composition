using Soloco.Composition.LinFu.IntegrationTests.CompositionSpecifications;

namespace Soloco.Composition.LinFu.IntegrationTests.InjectionSpecifications
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