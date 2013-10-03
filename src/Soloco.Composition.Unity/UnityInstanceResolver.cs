
using System;
using Microsoft.Practices.Unity;
using Soloco.Composition.InstanceResolver;

namespace Soloco.Composition.Unity
{
    class UnityInstanceResolver : IInstanceResolver
    {
        private readonly IUnityContainer _container;

        public UnityInstanceResolver(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");

            _container = container;
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}