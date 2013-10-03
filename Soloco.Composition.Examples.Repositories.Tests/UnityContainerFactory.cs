using Microsoft.Practices.Unity;

using NHibernate;

using Soloco.Composition.Unity;

namespace Soloco.Composition.Examples.Repositories.Tests
{
    public static class UnityContainerFactory
    {
        public static IUnityContainer Create()
        {
            var container = new UnityContainer();

            AddExtensions(container);
            RegisterInfrastructure(container);
            RegisterInfrastructurFactory(container);

            return container;
        }

        private static void AddExtensions(IUnityContainer container)
        {
            container.AddNewExtension<CompositionExtension>();
        }

        private static void RegisterInfrastructure(IUnityContainer container)
        {
            container
                .RegisterType<ISessionProvider, SessionProvider>(new ContainerControlledLifetimeManager());
        }

        private static void RegisterInfrastructurFactory(IUnityContainer container)
        {
            container
                .RegisterType<ISession>(new InjectionFactory(c => c.Resolve<ISessionProvider>().Get()));
        }
    }
}