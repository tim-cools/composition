
using Soloco.Composition.Container;

namespace Soloco.Composition.Container.IntegrationTests.AttributeCompositionSpecifications
{
    public static class ContainerFactory
    {
        public static CompositionContainer Create()
        {
            return new CompositionContainer();
        }

        public static CompositionContainer Create(IPersonBehaviorChecker personBehaviorChecker)
        {
            var container = new CompositionContainer();

            RegisterInfrastructure(container, personBehaviorChecker);

            return container;
        }

        private static void RegisterInfrastructure(ICompositionContainer container, IPersonBehaviorChecker personBehaviorChecker)
        {
            container.Register<IPersonBehaviorChecker>(personBehaviorChecker);
        }
    }
}