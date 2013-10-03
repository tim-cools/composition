
using System;
using System.Security.Permissions;
using LinFu.IoC;
using LinFu.IoC.Interfaces;
using Soloco.Composition.Generation;
using Soloco.Composition.Construction;
using Soloco.Composition.InstanceResolver;

namespace Soloco.Composition.LinFu
{
    /// <summary>
    /// Extension methods for LinFu service container.
    /// </summary>
    public static class ServiceContainerCompositionExtensions 
    {
        /// <summary>
        /// Composes the specified composite type.
        /// </summary>
        /// <typeparam name="T">The composite interface type.</typeparam>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        public static T Compose<T>(this IServiceContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");

            var compositionFactoryBuilder = container.GetCompositionFactoryBuilder();

            return (T)compositionFactoryBuilder.Create(typeof(T)).Create();
        }

        /// <summary>
        /// Composes the specified composite type with an existing object as part of the composit.
        /// </summary>
        /// <typeparam name="T">The composite interface type.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="existing">The existing.</param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand)]
        public static T Compose<T>(this IServiceContainer container, object existing)
        {
            if (container == null) throw new ArgumentNullException("container");

            var compositionFactoryBuilder = container.GetCompositionFactoryBuilder();

            return (T) compositionFactoryBuilder.Create(typeof(T)).Compose(existing);
        }

        /// <summary>
        /// Registers a composite type in the container.
        /// </summary>
        /// <typeparam name="T">The type of the composite interface</typeparam>
        /// <param name="container">The container.</param>
        /// <returns>The container</returns>
        public static IServiceContainer RegisterComposite<T>(this IServiceContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");

            var factory = CreateFactory(container);
            container.AddFactory(typeof(T), factory);

            return container;
        }

        /// <summary>
        /// Registers a composite type in the container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IServiceContainer RegisterComposite(this IServiceContainer container, Type type)
        {
            if (container == null) throw new ArgumentNullException("container");
            
            var factory = CreateFactory(container);
            container.AddFactory(type, factory);

            return container;
        }

        private static IFactory CreateFactory(this IServiceContainer container)
        {
            var compositionFactoryBuilder = container.GetCompositionFactoryBuilder();
            return new LinFuCompositeFactory(compositionFactoryBuilder);
        }

        private static ICompositionFactoryBuilder GetCompositionFactoryBuilder(this IServiceContainer container)
        {
            if (!container.Contains(typeof (ICompositionFactoryBuilder)))
            {
                container.Inject<ICompositionFactoryBuilder>().Using<CompositionFactoryCache>().AsSingleton();
                container.Inject<IThisInjector>().Using<ThisInjector>().AsSingleton();
                container.Inject<ICompositionFactoryBuilder>().Using<CompositionFactoryCache>().AsSingleton();
                container.Inject<ICompositePartsFactory>().Using<CompositePartsFactory>().AsSingleton();
                container.Inject<IKnownPartTypesResolver>().Using<AttributeKnownPartTypesResolver>().AsSingleton();
                container.AddService<IInstanceResolver>(new LinFuInstanceResolver(container));
            }

            return container.GetService<ICompositionFactoryBuilder>();
        }
    }
}