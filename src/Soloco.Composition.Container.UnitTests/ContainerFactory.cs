
using Soloco.Composition.Container;
using Soloco.Composition.Container.IntegrationTests.CompositionSpecifications;

namespace Soloco.Composition.Container.IntegrationTests
{
    public static class ContainerFactory
    {
        public static ICompositionContainer Create()
        {
            var container = new CompositionContainer();

            RegisterBehavior(container, true);
            RegisterComposits(container);

            return container;
        }

        public static ICompositionContainer Create(IPersonBehaviorChecker personBehaviorChecker)
        {
            var container = new CompositionContainer();

            RegisterInfrastructure(container, personBehaviorChecker);
            RegisterBehavior(container, false);
            RegisterComposits(container);

            return container;
        }


        private static void RegisterInfrastructure(CompositionContainer container, IPersonBehaviorChecker personBehaviorChecker)
        {
            container.Register<IPersonBehaviorChecker>(personBehaviorChecker);
        }

        private static void RegisterBehavior(CompositionContainer container, bool debug)
        {
            if (debug)
            {
                container.Register<IPerson, DebugPerson>()
                    .Register<IPartyAnimalBehavior, DebugPartyAnimalBehavior>()
                    .Register<IDeveloperBehavior, DebugDeveloperBehavior>();
            }
            else
            {
                container.Register<IPerson, Person>()
                .Register<IPartyAnimalBehavior, PartyAnimalBehavior>()
                .Register<IDeveloperBehavior, DeveloperBehavior>();
            }
        }

        private static void RegisterComposits(CompositionContainer container)
        {
            container
                .RegisterComposite<IPartyAnimal>()
                .RegisterComposite<IDeveloper>()
                .RegisterComposite<IPartyDeveloper>();
        }
    }
}