using LinFu.IoC;
using LinFu.IoC.Interfaces;
using Soloco.Composition.LinFu.IntegrationTests.CompositionSpecifications;

namespace Soloco.Composition.LinFu.IntegrationTests
{
    public static class ContainerFactory
    {
        public static IServiceContainer Create()
        {
            var container = new ServiceContainer();

            RegisterBehavior(container, true);
            RegisterComposits(container);

            return container;
        }

        public static IServiceContainer Create(IPersonBehaviorChecker personBehaviorChecker)
        {
            var container = new ServiceContainer();

            RegisterInfrastructure(container, personBehaviorChecker);
            RegisterBehavior(container, false);
            RegisterComposits(container);

            return container;
        }


        private static void RegisterInfrastructure(ServiceContainer container, IPersonBehaviorChecker personBehaviorChecker)
        {
            container.AddService<IPersonBehaviorChecker>(personBehaviorChecker);
        }

        private static void RegisterBehavior(ServiceContainer container, bool debug)
        {
            if (debug)
            {
                container.Inject<IPerson>().Using<DebugPerson>().OncePerRequest();
                container.Inject<IPartyAnimalBehavior>().Using<DebugPartyAnimalBehavior>().OncePerRequest();
                container.Inject<IDeveloperBehavior>().Using<DebugDeveloperBehavior>().OncePerRequest();
            }
            else
            {
                container.Inject<IPerson>().Using<Person>().OncePerRequest();
                container.Inject<IPartyAnimalBehavior>().Using<PartyAnimalBehavior>().OncePerRequest();
                container.Inject<IDeveloperBehavior>().Using<DeveloperBehavior>().OncePerRequest();
            }
        }

        private static void RegisterComposits(ServiceContainer container)
        {
            container
                .RegisterComposite<IPartyAnimal>()
                .RegisterComposite(typeof(IDeveloper))
                .RegisterComposite<IPartyDeveloper>();
        }
    }
}