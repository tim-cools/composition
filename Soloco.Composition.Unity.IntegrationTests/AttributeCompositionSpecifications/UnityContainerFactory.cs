using Microsoft.Practices.Unity;
using Soloco.Composition.Unity;

namespace Soloco.Composition.Unity.IntegrationTests.AttributeCompositionSpecifications
{
    public static class UnityContainerFactory
    {
        public static IUnityContainer Create()
        {
            var container = new UnityContainer();

            AddExtensions(container);

            return container;
        }

        public static IUnityContainer Create(IPersonBehaviorChecker personBehaviorChecker)
        {
            var container = new UnityContainer();

            AddExtensions(container);
            RegisterInfrastructure(container, personBehaviorChecker);

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
    }
}