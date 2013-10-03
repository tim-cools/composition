
using System;

namespace Soloco.Composition.Container
{
    /// <summary>
    /// Basic IoC Container capable of creating compositions.
    /// This container is not optimized and lacks a lot of features.
    /// It is only for Demo and Testing purpose. It should not be used 
    /// in production environments. Use other IoC container integrations
    /// for a full blown IoC experience.
    /// </summary>
    public interface ICompositionContainer
    {
        /// <summary>
        /// Registers a service instance in the container.
        /// </summary>
        ICompositionContainer Register<T>(T instance) 
            where T : class;

        /// <summary>
        /// Registers service implementation type in the container.
        /// </summary>
        ICompositionContainer Register<TContract, TImplementation>()
            where TContract : class
            where TImplementation : class;

        /// <summary>
        /// Registers service implementation type in the container.
        /// </summary>
        ICompositionContainer Register(Type contract, Type implementation);

        /// <summary>
        /// Registers a composite type in the container.
        /// </summary>
        ICompositionContainer RegisterComposite<T>() where T : class;

        /// <summary>
        /// Registers a composite type in the container.
        /// </summary>
        ICompositionContainer RegisterComposite(Type compositionType);

        /// <summary>
        /// Resolves a service of the specified service type.
        /// </summary>
        T Resolve<T>() where T : class;

        /// <summary>
        /// Resolves a service of the specified service type.
        /// </summary>
        object Resolve(Type serviceType);

        /// <summary>
        /// Composes the specified composite type.
        /// </summary>
        T Compose<T>() where T : class;

        /// <summary>
        /// Composes the specified composite type with an existing object as part of the composit.
        /// </summary>
        T Compose<T>(object existing) where T : class;
    }
}