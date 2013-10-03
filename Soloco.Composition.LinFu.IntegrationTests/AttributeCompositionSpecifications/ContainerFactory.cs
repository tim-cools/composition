using LinFu.IoC;
using LinFu.IoC.Interfaces;

namespace Soloco.Composition.LinFu.IntegrationTests.AttributeCompositionSpecifications
{
    public static class ContainerFactory
    {
        public static ServiceContainer Create()
        {
            return new ServiceContainer();
        }

        public static ServiceContainer Create(IPersonBehaviorChecker personBehaviorChecker)
        {
            var container = new ServiceContainer();

            RegisterInfrastructure(container, personBehaviorChecker);

            return container;
        }

        private static void RegisterInfrastructure(IServiceContainer container, IPersonBehaviorChecker personBehaviorChecker)
        {
            container.AddService<IPersonBehaviorChecker>(personBehaviorChecker);
        }
    }
}