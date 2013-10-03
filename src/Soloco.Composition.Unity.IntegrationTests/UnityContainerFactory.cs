using Microsoft.Practices.Unity;

namespace Soloco.Composition.Unity.IntegrationTests
{
    public static class UnityContainerFactory
    {
        public static IUnityContainer Create()
        {
            var container = new UnityContainer();

            AddExtensions(container);
            RegisterBehavior(container, true);
            RegisterComposits(container);

            return container;
        }

        public static IUnityContainer Create(IPersonBehaviorChecker personBehaviorChecker)
        {
            var container = new UnityContainer();

            AddExtensions(container);
            RegisterInfrastructure(container, personBehaviorChecker);
            RegisterBehavior(container, false);
            RegisterComposits(container);

            return container;
        }

        private static void AddExtensions(IUnityContainer container)
        {
            container.AddNewExtension<CompositionExtension>();
        }

        private static void RegisterInfrastructure(IUnityContainer container, IPersonBehaviorChecker personBehaviorChecker)
        {
            container.RegisterInstance<IPersonBehaviorChecker>(personBehaviorChecker);
        }

        private static void RegisterBehavior(IUnityContainer container, bool debug)
        {
            if (debug)
            {
                container
                    .RegisterType<IPerson, DebugPerson>()
                    .RegisterType<IPartyAnimalBehavior, DebugPartyAnimalBehavior>()
                    .RegisterType<IDeveloperBehavior, DebugDeveloperBehavior>();
            }
            else
            {
                container
                    .RegisterType<IPerson, Person>()
                    .RegisterType<IPartyAnimalBehavior, PartyAnimalBehavior>()
                    .RegisterType<IDeveloperBehavior, DeveloperBehavior>();
            }
        }

        private static void RegisterComposits(IUnityContainer container)
        {
            container
                .RegisterComposite<IPartyAnimal>()
                .RegisterComposite(typeof(IDeveloper))
                .RegisterComposite<IPartyDeveloper>();
        }
    }
}