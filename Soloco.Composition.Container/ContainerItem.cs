using System;
using System.Collections.Generic;

namespace Soloco.Composition.Container
{
    internal class ContainerItem
    {
        private readonly CompositionContainer _container;
        private readonly Type _implementation;
        private readonly IDictionary<Type, object> _genericInstances = new Dictionary<Type, object>();
        private object _instance;

        public ContainerItem(CompositionContainer container, Type implementation)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (implementation == null) throw new ArgumentNullException("implementation");

            _container = container;
            _implementation = implementation;
        }

        public ContainerItem(CompositionContainer container, object instance)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (instance == null) throw new ArgumentNullException("instance");

            _container = container;
            _instance = instance;
        }

        public object GetInstance()
        {
            return _instance ?? (_instance = _container.Create(_implementation));
        }

        public object GetGenericInstance(Type genericContract)
        {
            if (_genericInstances.ContainsKey(genericContract))
            {
                return _genericInstances[genericContract];
            }
            var parameters = genericContract.GetGenericArguments();
            var genericImplementation = _implementation.MakeGenericType(parameters);
            var instance = _container.Create(genericImplementation);

            _genericInstances.Add(genericContract, instance);
            return instance;
        }
    }
}