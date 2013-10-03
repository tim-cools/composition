using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Soloco.Composition.Construction;
using Soloco.Composition.Generation;
using Soloco.Composition.InstanceResolver;

namespace Soloco.Composition.Container
{
    /// <summary>
    /// Basic IoC Container capable of creating compositions.
    /// This container is not optimized and lacks a lot of features and checks.
    /// It is only for Demo and Testing purpose. It should not be used 
    /// in production environments. Use other IoC container integrations
    /// for a full blown IoC experience.
    /// </summary>
    public class CompositionContainer : ICompositionContainer, IInstanceResolver
    {
        private readonly IDictionary<Type, ICompositeFactory> _compositFactories = new Dictionary<Type, ICompositeFactory>();
        private readonly IDictionary<Type, ContainerItem> _items = new Dictionary<Type, ContainerItem>();
        private readonly ICompositionFactoryBuilder _compositionFactoryBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionContainer"/> class.
        /// </summary>
        public CompositionContainer()
        {
            Register<ICompositionFactoryBuilder, CompositionFactoryCache>();
            Register<IThisInjector, ThisInjector>();
            Register<ICompositePartsFactory, CompositePartsFactory>();
            Register<IKnownPartTypesResolver, AttributeKnownPartTypesResolver>();
            Register<IInstanceResolver>(this);

            _compositionFactoryBuilder = Resolve<ICompositionFactoryBuilder>();
        }

        /// <summary>
        /// Registers a service instance in the container.
        /// </summary>
        public ICompositionContainer Register<T>(T instance)
            where T : class
        {
            if (instance == null) throw new ArgumentNullException("instance");

            var item = new ContainerItem(this, instance);

            _items.Add(typeof(T), item);

            return this;
        }

        /// <summary>
        /// Registers service implementation type in the container.
        /// </summary>
        public ICompositionContainer Register<TContract, TImplementation>()
            where TContract : class
            where TImplementation : class
        {
            var item = new ContainerItem(this, typeof(TImplementation));

            _items.Add(typeof(TContract), item);

            return this;
        }

        /// <summary>
        /// Registers service implementation type in the container.
        /// </summary>
        public ICompositionContainer Register(Type contract, Type implementation)
        {
            var item = new ContainerItem(this, implementation);

            _items.Add(contract, item);

            return this;
        }

        /// <summary>
        /// Registers a composite type in the container.
        /// </summary>
        public ICompositionContainer RegisterComposite<T>()
            where T : class
        {
            return RegisterComposite(typeof(T));
        }

        /// <summary>
        /// Registers a composite type in the container.
        /// </summary>
        public ICompositionContainer RegisterComposite(Type compositionType)
        {
            if (compositionType == null) throw new ArgumentNullException("compositionType");

            if (compositionType.IsGenericTypeDefinition)
            {
                _compositFactories.Add(compositionType, null);
            }
            else
            {
                var factory = _compositionFactoryBuilder.Create(compositionType);
                _compositFactories.Add(compositionType, factory);
            }

            return this;
        }

        /// <summary>
        /// Composes the specified composite type.
        /// </summary>
        public T Compose<T>()
            where T : class
        {
            var factory = _compositionFactoryBuilder.Create(typeof(T));
            return (T)factory.Create();
        }

        /// <summary>
        /// Composes the specified composite type with an existing object as part of the composit.
        /// </summary>
        public T Compose<T>(object existing)
            where T : class
        {
            var factory = _compositionFactoryBuilder.Create(typeof(T));
            return (T)factory.Compose(existing);
        }

        /// <summary>
        /// Resolves a service of the specified service type.
        /// </summary>
        public T Resolve<T>() where T : class
        {
            var contract = typeof(T);
            return Resolve(contract) as T;
        }

        /// <summary>
        /// Resolves the specified contract.
        /// </summary>
        public object Resolve(Type contract)
        {
            var service = GetRegisteredService(contract);
            if (service != null) return service;

            service = GetRegisteredGenericService(contract);
            if (service != null) return service;

            service = GetRegisteredComposite(contract);
            if (service != null) return service;

            service = GetRegisteredGenericComposite(contract);
            if (service != null) return service;

            return Create(contract);
        }

        private object GetRegisteredService(Type contract)
        {
            return _items.ContainsKey(contract)
                ? _items[contract].GetInstance()
                : null;
        }

        private object GetRegisteredGenericService(Type contract)
        {
            if (!contract.IsGenericType) return null;

            var definition = contract.GetGenericTypeDefinition();
            if (definition != null && _items.ContainsKey(definition))
            {
                var item = _items[definition];
                return item.GetGenericInstance(contract);
            }
            return null;
        }

        private object GetRegisteredComposite(Type contract)
        {
            return _compositFactories.ContainsKey(contract)
                ? _compositFactories[contract].Create()
                : null;
        }

        private object GetRegisteredGenericComposite(Type contract)
        {
            if (!contract.IsGenericType) return null;

            var genericType = contract.GetGenericTypeDefinition();
            return genericType != null && _compositFactories.ContainsKey(genericType) 
                ? _compositionFactoryBuilder.Create(contract).Create()
                : null;
        }

        internal object Create(Type implementation)
        {
            var constructor = GetParametersWithMostKnownParameters(implementation);
            if (constructor == null)
            {
                return null;
            }
            var constructorParameters = constructor.GetParameters();

            var parameters = new object[constructorParameters.Length];

            var index = 0;
            foreach (var parameterInfo in constructorParameters)
            {
                parameters[index++] = Resolve(parameterInfo.ParameterType);
            }

            return constructor.Invoke(parameters);
        }

        private ConstructorInfo GetParametersWithMostKnownParameters(Type implementation)
        {
            var constructors = implementation.GetConstructors();

            var maxParameters = -1;
            ConstructorInfo constructor = null;

            foreach (var constructorInfo in constructors)
            {
                var parameters = constructorInfo.GetParameters();

                if (parameters.Length < maxParameters) continue;
                if (!CheckWhetherParametersAreKnown(parameters)) continue;

                maxParameters = parameters.Length;
                constructor = constructorInfo;
            }
            return constructor;
        }

        private bool CheckWhetherParametersAreKnown(IEnumerable<ParameterInfo> parameters)
        {
            return parameters
                .All(parameter => _items.ContainsKey(parameter.ParameterType) 
                    || _compositFactories.ContainsKey(parameter.ParameterType));
        }
    }
}