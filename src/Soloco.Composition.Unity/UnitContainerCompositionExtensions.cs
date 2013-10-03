
using System;
using System.Security.Permissions;
using Microsoft.Practices.Unity;

namespace Soloco.Composition.Unity
{
    /// <summary>
    /// UnitContainer Extension methods for Composition Unity Extension.
    /// </summary>
    public static class UnitContainerCompositionExtensions 
    {
        /// <summary>
        /// Composes the specified composite type.
        /// </summary>
        /// <typeparam name="T">The composite interface type.</typeparam>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand)]
        public static T Compose<T>(this IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            return container.Configure<CompositionExtension>().Compose<T>();
        }

        /// <summary>
        /// Composes the specified composite type with an existing object as part of the composit.
        /// </summary>
        /// <typeparam name="T">The composite interface type.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="existing">The existing.</param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand)]
        public static T Compose<T>(this IUnityContainer container, object existing)
        {
            if (container == null) throw new ArgumentNullException("container");
            return container.Configure<CompositionExtension>().Compose<T>(existing);
        }

        /// <summary>
        /// Registers a composite type in the container.
        /// </summary>
        /// <typeparam name="T">The type of the composite interface</typeparam>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        public static IUnityContainer RegisterComposite<T>(this IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            container.Configure<CompositionExtension>().RegisterComposite<T>();
            return container;
        }

        /// <summary>
        /// Registers a composite type in the container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IUnityContainer RegisterComposite(this IUnityContainer container, Type type)
        {
            if (container == null) throw new ArgumentNullException("container");
            container.Configure<CompositionExtension>().RegisterComposite(type);
            return container;
        }
    }
}